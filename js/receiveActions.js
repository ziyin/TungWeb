; $(document).ready(function () {
    getModelList();
    getSaveData();

    $('#ContentPlaceHolder1_btnSave').on('click', function (e) {
        // e.preventDefault();
        onSaveClick();
    });

    $('#ContentPlaceHolder1_btnQuery').on('click', function () {
        // 清除先前session
        sessionStorage.clear();
    });

    // 頁面刷新清除 session
    window.onbeforeunload = function () {
        sessionStorage.setItem("origin", window.location.href);
    }

    window.onload = function () {
        if (window.location.href == sessionStorage.getItem("origin")) {
            sessionStorage.clear();
        }
    }

    // keypress Enter for 重工單號 190108
    $('#ContentPlaceHolder1_txtReworkNo').on('keypress', function (e) {
        // var key = String.fromCharCode(e.charCode || e.which);
        var code = e.keyCode ? e.keyCode : e.which;
        if (code == 13) {
            e.preventDefault();
            var ele = $('#receiveModelItems input[type=text]:enabled')[0];
            if (ele) {
                ele.focus();
            }
        }
        $(this).val($(this).val());
        //$('#ContentPlaceHolder1_txtReworkNo').val($('#ContentPlaceHolder1_txtReworkNo').val() + key);
    });
});

var RECEIVE_IN_URL = 'https://assyapi.bestyield.com/api/AssyChk/';
var modelListItems = [],
    partsListItems = [];

function getModelList() {
    removeModelList();
    var pathname = window.location.pathname,
        orderNo = $('#ContentPlaceHolder1_txtOrderNo'),
        isOrderNo = orderNo.length > 0 && orderNo.val() !== '';
    if (pathname === '/ReceiveIn.aspx' && isOrderNo) {
        $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
        var GET_MODEL_LIST_URL = RECEIVE_IN_URL + orderNo.val();

        $.ajax({
            url: GET_MODEL_LIST_URL,
            type: 'get',
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                if (response && response.length > 0) {
                    // console.log(response);
                    renderModelList(response);
                } else {
                    removeModelList();
                }
            },
            error: function (xhr) {
                console.log(xhr.status + " " + xhr.statusText);
            }
        });
    }
}

function renderModelList(items) {
    // console.log(modelListItems);

    $('.receive-model-list-container').append(
        '<table class="gridview-table receive-model-list-table">' +
        '<tr>' +
        '<th></th>' +
        '<th>項次</th>' +
        '<th>料號</th>' +
        '<th>機種</th>' +
        '<th>應收數量</th>' +
        '<th>實際點收數量</th>' +
        '</tr>' +
        '</table>'
    );
    for (var i = 0; i < items.length; i++) {
        var orderNo = items[i].orderNo;
        $('.receive-model-list-table').append(
            '<tr class="mlrow mlrow-' + i + '">' +
            '<td>' +
            '<input type="button" class="select-btn" value="選取" onclick="onSelectClick(' + i + ',\'' + orderNo + '\');" />' +
            '</td>' +
            '<td>' + items[i].item + '</td>' +
            '<td>' + items[i].partId + '</td>' +
            '<td>' + items[i].description + '</td>' +
            '<td>' + items[i].orderQty + '</td>' +
            '<td>' + items[i].deliveryQty + '</td>' +
            '</tr>'
        );
    }

    // 被選的對象+顏色提示 181227 - Ivy
    if (modelListItems[0]) {
        var indexNo = modelListItems[0].item.split('-')[0];
        //console.log(indexNo);

        var index = 0;
        for (var i = 0; i < $('.mlrow').length; i++) {
            // console.log($('.mlrow-' + i + ' td:nth-child(2)').text());
            if ($('.mlrow-' + i + ' td:nth-child(2)').text() == indexNo) {
                // console.log(123);
                index = i;
            }
        }
        var selectedRow = $('.receive-model-list-table .mlrow-' + index);
        selectedRow.addClass('selected');
    } else {
        $('.mlrow').removeClass('selected');
    }
}

function removeModelList() {
    $('.receive-model-list-table').remove();
}

function onSelectClick(index, orderNo) {
    // 清空資料
    removeModelItems();
    removePartsListItems();

    // 清除先前session
    sessionStorage.clear();

    if (orderNo && orderNo.length > 0) {
        var selectedRow = $('.receive-model-list-table .mlrow-' + index);
        $('.mlrow').removeClass('selected');
        selectedRow.addClass('selected');
        getModelByItem(index, orderNo);
    } else {
        $('.mlrow').removeClass('selected');
    }
}

function getModelByItem(index, orderNo) {
    var itemNo = $('.mlrow-' + index + ' td:nth-child(2)').text(); // 181227 Ivy
    var GET_MODEL_BY_ITEM_URL = RECEIVE_IN_URL + '/' + orderNo + '/' + itemNo + '/Machine';
    var GET_PARTS_LIST_BY_ITEM_URL = RECEIVE_IN_URL + '/' + orderNo + '/' + itemNo + '/parts';

    $('#ContentPlaceHolder1_msgStatus').val("SUCCESS"); // J 181224

    $.ajax({
        url: GET_MODEL_BY_ITEM_URL,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json',
        async: false, // J 181224
        success: function (response) {
            if (response && response.length > 0) {
                // console.log(response);
                renderModelItems(response);
                modelListItems = response;
            } else {
                removeModelItems();
            }
        },
        error: function (xhr) {
            console.log(xhr.status + " " + xhr.statusText);
        }
    });

    $.ajax({
        url: GET_PARTS_LIST_BY_ITEM_URL,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json',
        async: false, // J 181224
        success: function (response) {
            if (response && response.length > 0) {
                // console.log(response);
                renderPartsListItems(response);
                partsListItems = response;
            } else {
                removePartsListItems();
            }
        },
        error: function (xhr) {
            console.log(xhr.status + " " + xhr.statusText);
        }
    });
}

function renderModelItems(items) {
    $('#receiveModelItems').append(
        '<table class="form-table mt-3 mb-3 receive-model-items-table"></table>'
    );
    for (var i = 0; i < items.length; i++) {
        $('.receive-model-items-table').append(
            '<tr>' +
            '<td>' +
            '<input type="text" value="' + items[i].partId + '" disabled />' +
            '</td>' +
            '<td>' +
            '<input type="text" value="' + items[i].description + '" disabled />' +
            '</td>' +
            '<td>' +
            // onkeypress
            '<input type="text" onkeypress="return setupEnterToNext(this, event)" onchange="onItemSnChange(this, ' + i + ')" style="width: 360px" value="' + items[i].sn + '" placeholder="請輸入序號" />' +
            '</td>' +
            '</tr>'
        );
    }
}

function removeModelItems() {
    $('.receive-model-items-table').remove();
}

function onItemSnChange(el, index) {
    modelListItems[index].sn = el.value;

    // console.log(modelListItems);
}

// 190107 阻擋送出表單 + Enter 跳下一個 (ivy)
function setupEnterToNext(obj, e) {
    //var key = String.fromCharCode(e.charCode || e.which);
    var code = e.keyCode ? e.keyCode : e.which;
    var ele = $('input:enabled');

    if (code == 13) {
        e.preventDefault();
        for (var i = 0; i < ele.length; i++) {
            var q = (i == ele.length - 1) ? 0 : i + 1;
            if (obj == ele[i]) {
                ele[q].focus();
                if (i == ele.length - 1) {
                    $('#ContentPlaceHolder1_btnSave').focus();
                }
                break
            }
        }
    }
}

function renderPartsListItems(items) {
    $('#receivePartsListItems').append(
        '<table class="fill-in-table receive-parts-list-items-table">' +
        '<tr>' +
        '<th>類別</th>' +
        '<th>料號</th>' +
        '<th>品名規格</th>' +
        '<th>應收數量</th>' +
        '<th width="100px">實際點收數量</th>' +
        '<th width="200px">到貨日期</th>' +
        '<th>備註</th>' +
        '</tr>' +
        '</table>'
    );
    for (var i = 0; i < items.length; i++) {
        var deliveryDate = moment(items[i].deliveryDate).format('YYYY-MM-DD').toString();
        if (deliveryDate == '1900-01-01') {
            deliveryDate = '';
        }
        var remark = items[i].remark == null ? "" : items[i].remark;

        // 應收數量 = 0，實際點收數量 > 0 → 實際點收數量、到貨日期、備註欄位不可再修改
        if (items[i].orderQty == 0 && items[i].deliveryQty != 0) {
            $('.receive-parts-list-items-table').append(
                '<tr>' +
                '<td>' + items[i].category + '</td>' +
                '<td>' + items[i].partId + '</td>' +
                '<td>' + items[i].description + '</td>' +
                '<td>' + items[i].orderQty + '</td>' +
                '<td>' +
                '<input type="number" disabled value="' + items[i].deliveryQty + '" />' +
                '</td>' +
                '<td>' +
                '<input type="date" disabled value="' + deliveryDate + '" />' +
                '</td>' +
                '<td>' +
                '<input type="text" disabled value="' + remark + '" class="w-100 min-360" />' +
                '</td>' +
                '</tr>'
            );
        } else {
            $('.receive-parts-list-items-table').append(
                '<tr>' +
                '<td>' + items[i].category + '</td>' +
                '<td>' + items[i].partId + '</td>' +
                '<td>' + items[i].description + '</td>' +
                '<td>' + items[i].orderQty + '</td>' +
                '<td>' +
                '<input type="number" min="0" onkeypress="return setupEnterToNext(this, event)" onchange="onDelQtyChange(this, ' + i + ')" value="' + items[i].deliveryQty + '" />' +
                '</td>' +
                '<td>' +
                '<input type="date" onkeypress="return setupEnterToNext(this, event)" onchange="onDelDateChange(this, ' + i + ')" value="' + deliveryDate + '" />' +
                '</td>' +
                '<td>' +
                '<input type="text" onkeypress="return setupEnterToNext(this, event)" onchange="onRemarkChange(this, ' + i + ')" value="' + remark + '" class="w-100 min-360" />' +
                '</td>' +
                '</tr>'
            );
        }
    }
}

function removePartsListItems() {
    $('.receive-parts-list-items-table').remove();
}

function onDelQtyChange(el, index) {
    partsListItems[index].deliveryQty = el.value;
}

//console.log(partsListItems);

function onDelDateChange(el, index) {
    partsListItems[index].deliveryDate = el.value;
}

function onRemarkChange(el, index) {
    partsListItems[index].remark = el.value;
}

function getSaveData() {
    //console.log(partsListItems);
    var message = JSON.parse(sessionStorage.getItem('message'));

    if (sessionStorage.getItem('modelListItems') !== null && sessionStorage.getItem('partsListItems') !== null) {
        modelListItems = JSON.parse(sessionStorage.getItem('modelListItems'));
        // console.log(modelListItems);
        partsListItems = JSON.parse(sessionStorage.getItem('partsListItems'));
        renderModelItems(modelListItems);
        renderPartsListItems(partsListItems);
    }
    if (message !== null && message.msg == 'ERROR') {
        $('#ContentPlaceHolder1_lblSaveFail').text('應收數量與實際點收數量不一致，請確實填寫備註欄').css('color', 'red');
    } else {
        $('#ContentPlaceHolder1_lblSaveFail').css('color', '#00B400');

        $('#ContentPlaceHolder1_msgStatus').val("SUCCESS"); //20190111 Yiling
    }
}

function onSaveClick() {
    // 清除先前session
    sessionStorage.clear();

    var SAVE_MODEL_ITEMS_URL = RECEIVE_IN_URL + 'Machine',
        SAVE_PARTS_LIST_URL = RECEIVE_IN_URL + 'Parts';
    var message = {};

    for (var j = 0; j < modelListItems.length; j++) {
        modelListItems[j].modiUser = $('#ContentPlaceHolder1_lblUserId').text();
    }

    for (var i = 0; i < partsListItems.length; i++) {
        // 未點收的可以直接存不判斷備註
        var deliveryDate = moment(partsListItems[i].deliveryDate).format('YYYY-MM-DD').toString();
        if (partsListItems[i].deliveryQty == 0 && deliveryDate == '1900-01-01') {
            continue;

            // 判斷備註：應收數量與實際點收數量不一致
        } else if (partsListItems[i].orderQty != partsListItems[i].deliveryQty && partsListItems[i].remark == '') {
            alert('應收數量與實際點收數量不一致，請確實填寫備註欄');
            // $('#ContentPlaceHolder1_msgStatus').val('應收數量與實際點收數量不一致，請確實填寫備註欄');
            message = JSON.stringify({ msg: 'ERROR' });
            modelItemsData = JSON.stringify(modelListItems);
            partsListData = JSON.stringify(partsListItems);

            console.log(partsListItems);

            sessionStorage.setItem('modelListItems', modelItemsData);
            sessionStorage.setItem('partsListItems', partsListData);
            sessionStorage.setItem('message', message);

            return;
        }
        partsListItems[i].modiUser = $('#ContentPlaceHolder1_lblUserId').text();
    }

    modelItemsData = JSON.stringify(modelListItems);
    partsListData = JSON.stringify(partsListItems);
    message = JSON.stringify({ msg: 'SUCCESS' });

    // console.log(partsListData);

    sessionStorage.setItem('modelListItems', modelItemsData);
    sessionStorage.setItem('partsListItems', partsListData);
    sessionStorage.setItem('message', message);

    $.ajax({
        url: SAVE_MODEL_ITEMS_URL,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: modelItemsData,
        success: function (response) {
        },
        error: function (xhr) {
            console.log(xhr.status + " " + xhr.statusText);
        }
    });

    $.ajax({
        url: SAVE_PARTS_LIST_URL,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: partsListData,
        success: function (response) {
        },
        error: function (xhr) {
            console.log(xhr.status + " " + xhr.statusText);
        }
    });
}