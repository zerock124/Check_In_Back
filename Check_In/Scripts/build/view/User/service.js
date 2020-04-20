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
    var UserService = (function () {
        function UserService() {
        }
        UserService.prototype.GetAuthorityList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/User/GetUserList',
                type: 'POST',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        UserService.prototype.CreateUser = function (model) {
            var setting = {
                url: "/User/CreateUser",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, 'FormData');
        };
        UserService.prototype.CheckAuthority = function (Id) {
            var setting = {
                url: "/Authority/CheckAuthority?Id=" + Id,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        UserService.prototype.GetEditUserItem = function (ur_id) {
            var setting = {
                url: "/User/GetEditUserItem?ur_id=" + ur_id,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        UserService.prototype.EditUserItem = function (model) {
            var setting = {
                url: "/User/EditUserItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, 'FormData');
        };
        UserService.prototype.DeleteUserItem = function (ur_id, ur_im) {
            var setting = {
                url: "/User/DeleteUserItem",
                type: 'POST',
                data: {
                    ur_id: ur_id,
                    ur_im: ur_im
                }
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        UserService.prototype.GetRoleOptions = function () {
            var setting = {
                url: 'Authority/GetRoleOptions',
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return UserService;
    }());
    var user_service = new UserService();
    exports.default = user_service;
});
//# sourceMappingURL=service.js.map