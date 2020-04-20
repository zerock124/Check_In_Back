define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    function AjaxReturn(_setting, _datatype) {
        if (_datatype === void 0) { _datatype = 'JSON'; }
        var BasePath = window.BasePath;
        var headers = {};
        var token = $("input[name='__RequestVerificationToken']").val();
        headers["__RequestVerificationToken"] = token;
        var defSetting = {
            contentType: "application/json; charset=utf-8",
            cache: false,
            headers: headers
        };
        if (_datatype == 'JSON')
            _setting.data = JSON.stringify(_setting.data);
        if (_datatype == 'FormData') {
            defSetting.contentType = false;
            defSetting.processData = false;
        }
        if (!CheckURLHasOrigin(_setting.url))
            _setting.url = RemoveUrlPathDoubleSlash(BasePath + _setting.url);
        return jQuery.ajax(Object.assign(defSetting, _setting));
    }
    exports.AjaxReturn = AjaxReturn;
    function AddAntiForgeryToken(data) {
        if ($.isEmptyObject(data)) {
            data = {};
        }
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    }
    exports.AddAntiForgeryToken = AddAntiForgeryToken;
    function RemoveUrlPathDoubleSlash(urlPath) {
        return urlPath.replace(/\/\//gm, "/");
    }
    exports.RemoveUrlPathDoubleSlash = RemoveUrlPathDoubleSlash;
    function CheckURLHasOrigin(url) {
        var pattern = new RegExp('^(https?:\\/\\/)?' +
            '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' +
            '((\\d{1,3}\\.){3}\\d{1,3}))');
        return !!pattern.test(url);
    }
    exports.CheckURLHasOrigin = CheckURLHasOrigin;
    function NewId() {
        var d = Date.now();
        if (typeof performance !== 'undefined' && typeof performance.now === 'function') {
            d += performance.now();
        }
        var id = 'xxxxxxxx-xxxx-xxx4-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return id;
    }
    exports.NewId = NewId;
    function GetExportExcelUrl(url) {
        var BasePath = window.BasePath;
        if (!CheckURLHasOrigin(url))
            url = RemoveUrlPathDoubleSlash(BasePath + url);
        return url;
    }
    exports.GetExportExcelUrl = GetExportExcelUrl;
});
//# sourceMappingURL=PublicFunction.js.map