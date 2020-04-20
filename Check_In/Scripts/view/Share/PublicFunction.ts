/**
 * 設置ajax
 * @param _setting 設定內容
 * @param _datatype 送出時的資料型態(postbody時的header修改)
 */
export function AjaxReturn(_setting: JQueryAjaxSettings, _datatype: AjaxConvertDataType = 'JSON'): JQuery.jqXHR {
	const BasePath = window.BasePath; // _Layout.cshtml

	var headers = {};
	var token = $("input[name='__RequestVerificationToken']").val();
	headers["__RequestVerificationToken"] = token;

	const defSetting: JQueryAjaxSettings = {
		contentType: "application/json; charset=utf-8",
		cache: false,
		headers: headers
	}
	if (_datatype == 'JSON')
		_setting.data = JSON.stringify(_setting.data);
	if (_datatype == 'FormData') {
		defSetting.contentType = false;
		defSetting.processData = false;
	}

	// 如果網址未包含主機位置 (代表使用相對路徑或當前路徑)，加上網站基底位置
	if (!CheckURLHasOrigin(_setting.url))
		_setting.url = RemoveUrlPathDoubleSlash(BasePath + _setting.url);

	return jQuery.ajax(Object.assign(defSetting, _setting));
}

export function AddAntiForgeryToken(data) {
	if ($.isEmptyObject(data)) {
		data = {};
	}
	//回傳防偽基元得值
	data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
	return data;
}

/**
 * 替換網址路徑的雙斜線為單斜線
 * @param urlPath 網址路徑
 */
export function RemoveUrlPathDoubleSlash(urlPath: string) {
	return urlPath.replace(/\/\//gm, `/`);
}

/**
 * 確認網址是否包含主機位置
 * 出處：https://stackoverflow.com/questions/5717093/check-if-a-javascript-string-is-a-url/43467144
 * @param url 網址
 */
export function CheckURLHasOrigin(url) {
	var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
		'((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
		'((\\d{1,3}\\.){3}\\d{1,3}))'); // fragment locator
	return !!pattern.test(url);
}

/** 取得新的Guid */
export function NewId() {
	let d = Date.now();
	if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
		d += performance.now(); //use high-precision timer if available
	}

	const id = 'xxxxxxxx-xxxx-xxx4-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = (d + Math.random() * 16) % 16 | 0;
		d = Math.floor(d / 16);
		return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
	});

	return id;
}

export function GetExportExcelUrl(url: string) {
	const BasePath = window.BasePath; // _Layout.cshtml

	// 如果網址未包含主機位置 (代表使用相對路徑或當前路徑)，加上網站基底位置
	if (!CheckURLHasOrigin(url))
		url = RemoveUrlPathDoubleSlash(BasePath + url);

	return url;
}