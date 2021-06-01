; $(document).ready(function () {
	getAssemblyItems();

	// 點按儲存
	$('#ContentPlaceHolder1_btnSave').on('click', function (e) {
		// e.preventDefault();
		onSaveClick();
	});

	// keypress Enter for 備註
	$('#ContentPlaceHolder1_txtRemark').on('keypress', function (e) {
		// var key = String.fromCharCode(e.charCode || e.which);
		var code = e.keyCode ? e.keyCode : e.which;
		if (code == 13) {
			e.preventDefault();
			var ele = document.getElementsByClassName('tabinput')[0];
			if (ele) {
				ele.focus();
			}
		}
		$(this).val($(this).val());

		// $('#ContentPlaceHolder1_txtRemark').val($('#ContentPlaceHolder1_txtRemark').val() + key);
	});
});

// 組裝ACTIONS URL
var ASSYITEM_URL = 'https://assyapi.bestyield.com/api/AssyItem/';

function getAssemblyItems() {
	var pathname = window.location.pathname,
		snNum = $('#ContentPlaceHolder1_txtSn'),
		isSnNum = snNum.length > 0;
	var GET_ASSYITEM_URL = ASSYITEM_URL + snNum.val();

	if (pathname === '/Assembly.aspx' && isSnNum) {
		$(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

		$.ajax({
			url: GET_ASSYITEM_URL,
			type: 'get',
			dataType: 'json',
			success: function (response) {
				if (response && response.length > 0 && isSnNum) {
					renderAssemblyTable(response);
				} else {
					removeAssemblyTable();
				}
			},
			error: function (xhr) {
				console.log(xhr.status + " " + xhr.statusText);
			}
		});
	}
}

function renderAssemblyTable(result) {
	$(".table-container").addClass('mt-3').append(
		'<table class="fill-in-table">' +
		'<tr>' +
		'<th>Location</th>' +
		'<th>料號</th>' +
		'<th>品名規格</th>' +
		'<th>行為</th>' +
		'<th>新序號</th>' +
		'<th>新供應商序號</th>' +
		'<th>原序號</th>' +
		'<th>原供應商序號</th>' +
		'</tr>' +
		'</table>'
	);
	for (var i = 0; i < result.length; i++) {
		if (result[i].action === '新增') {
			$(".fill-in-table").append(
				'<tr>' +
				'<td>' + result[i].location + '</td>' +
				'<td>' + result[i].partId + '</td>' +
				'<td>' + result[i].description + '</td>' +
				'<td>' + result[i].action + '</td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput new-part-sn" value=' + result[i].newPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput new-part-vender-sn" value=' + result[i].newPartVenderSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" disabled class="old-part-sn" value=' + result[i].oldPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" disabled class="old-part-vender-sn" value=' + result[i].oldPartVenderSn + '></td>' +
				'</tr>'
			);
		} else if (result[i].action === '更換') {
			$(".fill-in-table").append(
				'<tr>' +
				'<td>' + result[i].location + '</td>' +
				'<td>' + result[i].partId + '</td>' +
				'<td>' + result[i].description + '</td>' +
				'<td>' + result[i].action + '</td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput new-part-sn" value=' + result[i].newPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput new-part-vender-sn" value=' + result[i].newPartVenderSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput old-part-sn" value=' + result[i].oldPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput old-part-vender-sn" value=' + result[i].oldPartVenderSn + '></td>' +
				'</tr>'
			);
		} else {
			$(".fill-in-table").append(
				'<tr>' +
				'<td>' + result[i].location + '</td>' +
				'<td>' + result[i].partId + '</td>' +
				'<td>' + result[i].description + '</td>' +
				'<td>' + result[i].action + '</td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" disabled class="new-part-sn" value=' + result[i].newPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" disabled class="new-part-vender-sn" value=' + result[i].newPartVenderSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput old-part-sn" value=' + result[i].oldPartSn + '></td>' +
				'<td><input type="text" onKeypress="return setupEnterToNext(this, event)" class="tabinput old-part-vender-sn" value=' + result[i].oldPartVenderSn + '></td>' +
				'</tr>'
			);
		}
	}
}

function removeAssemblyTable() {
	$(".table-container").remove();
}

function onSaveClick() {
	var data = [];
	var pathname = window.location.pathname,
		snNum = $('#ContentPlaceHolder1_txtSn'),
		isSnNum = snNum.length > 0;
	var GET_ASSYITEM_URL = ASSYITEM_URL + snNum.val();

	if (pathname === '/Assembly.aspx') {
		$.ajax({
			url: GET_ASSYITEM_URL,
			type: 'GET',
			dataType: 'json',
			contentType: 'application/json',
			async: false, // J 181220
			success: function (response) {
				if (response && response.length > 0 && isSnNum) {
					data = response;
					var newPartSnArr = document.getElementsByClassName('new-part-sn'),
						newPartVenderSnArr = document.getElementsByClassName('new-part-vender-sn'),
						oldPartSnArr = document.getElementsByClassName('old-part-sn'),
						oldPartVenderSnArr = document.getElementsByClassName('old-part-vender-sn');
					var SAVE_ASSYITEM_URL = ASSYITEM_URL;

					for (var i = 0; i < data.length; i++) {
						var newPartSn = newPartSnArr[i].value,
							newPartVenderSn = newPartVenderSnArr[i].value,
							oldPartSn = oldPartSnArr[i].value,
							oldPartVenderSn = oldPartVenderSnArr[i].value;

						data[i].newPartSn = newPartSn;
						data[i].newPartVenderSn = newPartVenderSn;
						data[i].oldPartSn = oldPartSn;
						data[i].oldPartVenderSn = oldPartVenderSn;
						// User Id
						data[i].modiUser = $('#ContentPlaceHolder1_lblUserId').text();
					}
					data = JSON.stringify(data);
					$('#ContentPlaceHolder1_msgStatus').val("SUCCESS"); // J 181220

					onSaveAssemblyData(data);
				}
			},
			error: function (xhr) {
				console.log(xhr.status + " " + xhr.statusText);
			}
		});
	}
}

function onSaveAssemblyData(data) {
	$.ajax({
		url: ASSYITEM_URL,
		type: 'POST',
		contentType: 'application/json',
		dataType: 'json',
		data: data,
		success: function (response) {
			// console.log(response);
		},
		error: function (xhr) {
			// console.log(xhr.status + " " + xhr.statusText);
		}
	});
}

function isEmptyOrSpace(string) {
	return string.replace(/(^\s*)|(\s*$)/g, "").length === 0;
}

function setupEnterToNext(obj, e) {
	var code = e.keyCode ? e.keyCode : e.which;
	var key = String.fromCharCode(e.charCode || e.which);
	var ele = document.getElementsByClassName('tabinput');

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
	/*else {
		for (var i = 0; i < ele.length; i++) {
			if (obj == ele[i]) {
				ele[i].value = ele[i].value + key;
			}
		}
	}

	return false;
	*/
}