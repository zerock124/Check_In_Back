var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var CheckRecordService = (function () {
        function CheckRecordService() {
        }
        CheckRecordService.prototype.GetCheckRecord = function (searchModel, pagination) {
            var setting = {
                url: '/CheckRecord/GetCheckRecord',
                type: 'POST',
                data: __assign(__assign({}, searchModel), pagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CheckRecordService.prototype.GetUserList = function () {
            var setting = {
                url: '/CheckRecord/GetUserList',
                type: 'GET'
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CheckRecordService.prototype.GetClick_In = function (ci_sn) {
            var setting = {
                url: "/CheckRecord/GetClick_In?ci_sn=" + ci_sn,
                type: 'GET'
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CheckRecordService.prototype.SubmitClick_In = function (model, time) {
            var setting = {
                url: '/CheckRecord/SubmitClick_In',
                type: 'POST',
                data: {
                    model: model,
                    time: time
                }
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CheckRecordService.prototype.GetExportCheckRecord = function (model) {
            var setting = {
                url: '/CheckRecord/GetExportCheckRecord',
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return CheckRecordService;
    }());
    var check_in_service = new CheckRecordService();
    exports.default = check_in_service;
});
//# sourceMappingURL=service.js.map