; $(document).ready(function () {
    currentPageShow();
    onWheelScrollY();
    inputEnterNext();
    // gridViewButtonStyled();
});

// 指向目前頁面
function currentPageShow() {
    var path = window.location.pathname.split("/").pop();
    var target;

    if (path == 'IEError.aspx') return;

    if (path == '') {
        path = 'Receive.aspx';
    }

    target = $('li a[href="' + path + '"]');
    target.addClass('active');
}

// table-container  有 scrollbar 時可滾輪移動x軸
function onWheelScrollY() {
    let gridViewTable = document.getElementsByClassName("gridview-table");
    if (gridViewTable.length > 0) {
        $(".gridview-table").parent().addClass("table-container");
    }
    var scrollTable = document.getElementsByClassName('table-container');
    function hasScrollBar(el) {
        return el.scrollWidth > el.clientWidth;
    }
    for (var i = 0; i < scrollTable.length; i++) {
        var index = i;
        scrollTable[i].addEventListener('mousewheel', function (e) {
            if (hasScrollBar(this)) {
                e.preventDefault();
                var step = 50;
                if (e.deltaY < 0) {
                    this.scrollLeft -= step;
                } else {
                    this.scrollLeft += step;
                }
            } else {
                return;
            }
        }, false);
    }
}

var myFile = [];

// apply 檔案ID
$("#ContentPlaceHolder1_fileUpload").change(function () {
    var files = $(this)[0].files;
    var maxsize = 1048576 * 2; // 2MB
    //var reg = /\.(pdf|xlsx)$/i;
    var reg = /\.(pdf|jpg)$/i;

    if (files && files.length > 0) {
        // console.log(files);
        var filename = files[0].name;
        var filesize = files[0].size;
        if (!reg.test(filename)) {
            alert(filename + " 檔案格式不正確，請重新上傳");
            $("#ContentPlaceHolder1_fileUpload").val('');
        } else if (filesize > maxsize) {
            alert(filename + " 檔案超過2MB");
            $("#ContentPlaceHolder1_fileUpload").val('');
        } else {
            var file = files[0];
            readFileSourceToURL(file);
            renderFile(file);
        }
    }
});

// Burn-in 檔案ID
$("#ContentPlaceHolder1_chooseFile").change(function () {
    var files = $(this)[0].files;
    var maxsize = 1048576 * 2; // 2MB
    var reg = /\.(jpg|png)$/i;

    if (files && files.length > 0) {
        // console.log(files);
        var filename = files[0].name;
        var filesize = files[0].size;
        if (!reg.test(filename)) {
            alert(filename + " 檔案格式不正確，請重新上傳");
            $("#ContentPlaceHolder1_chooseFile").val('');
        } else if (filesize > maxsize) {
            alert(filename + " 檔案超過2MB");
            $("#ContentPlaceHolder1_chooseFile").val('');
        } else {
            var file = files[0];
            readFileSourceToURL(file);
            renderFile(file);
        }
    }
})

function renderFile(file) {
    var filename = file.name,
        filesize = (file.size / 1024).toFixed(2);

    if (myFile.length !== 0) {
        myFile.splice(0, 1);
        myFile.push(file);
        // console.log(111);
        // console.log(myFile);
        $("#fileName").html(filename);
        $("#fileSize").html(filesize + "KB");
        $(".file-info").addClass("active");
    } else {
        myFile.push(file);
        // console.log(222);
        $("#fileName").html(filename);
        $("#fileSize").html(filesize + "KB");
        // console.log(myFile);
        $(".file-info").addClass("active");
    }

    var deleteBtn = document.getElementById("deleteFile");

    if (deleteBtn) {
        deleteBtn.addEventListener('click', function (e) {
            removeFile(e);
        }, false);
    }
}

// Read files
function readFileSourceToURL(file) {
    var fileReader = new FileReader();
    fileReader.readAsDataURL(file);
    fileReader.onload = function () {
        var source = dataURLtoBlob(this.result);
        // console.log(source);
        var imgurl = window.URL.createObjectURL(source);
        // console.log(imgurl);
    }
}

function dataURLtoBlob(dataurl) {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], { type: mime });
}

function removeFile(e) {
    $("#ContentPlaceHolder1_fileUpload").val('');
    $("#ContentPlaceHolder1_chooseFile").val('');
    // console.log(111);
    myFile.splice(0, 1);
    if (myFile.length < 1) {
        $(".file-info").removeClass("active");
    }
}

function gridViewButtonStyled() {
    var deleteBtn = document.getElementsByClassName("delete-btn"),
        selectBtn = document.getElementsByClassName("select-btn");
    if (deleteBtn && deleteBtn.length > 0) {
        $(".delete-btn").parent().css({ "padding": "0 0 0 10px", "text-align": "center" });
    }
    if (selectBtn && selectBtn.length > 0) {
        $(".select-btn").parent().css({ "padding": "0 0 0 10px", "text-align": "center" });
    }
}

function inputEnterNext() {
    var input1 = $('#ContentPlaceHolder1_txtSn1'),
        input2 = $('#ContentPlaceHolder1_txtSn2');
    input1.on('keypress', function (e) {
        if (e.keyCode == 13) {
            input2.focus();
            return false
        }
    });
}