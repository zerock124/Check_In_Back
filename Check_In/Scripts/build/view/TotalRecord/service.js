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
    var TotalRecordService = (function () {
        function TotalRecordService() {
        }
        TotalRecordService.prototype.GetTotalRecord = function (searchModel) {
            var setting = {
                url: '/TotalRecord/GetTotalRecord',
                type: 'POST',
                data: __assign({}, searchModel)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        TotalRecordService.prototype.GetOptions = function () {
            var setting = {
                url: '/TotalRecord/GetOptions',
                type: 'GET'
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        TotalRecordService.prototype.GetCheck_All = function (ur_id) {
            var setting = {
                url: "/TotalRecord/GetCheck_All?ur_id=" + ur_id,
                type: 'GET'
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        TotalRecordService.prototype.SubmitCheck_All = function (model) {
            var setting = {
                url: "/TotalRecord/SubmitCheck_All",
                type: 'POST',
                data: __assign({}, model)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return TotalRecordService;
    }());
    var check_all_service = new TotalRecordService();
    exports.default = check_all_service;
});
//# sourceMappingURL=service.js.map