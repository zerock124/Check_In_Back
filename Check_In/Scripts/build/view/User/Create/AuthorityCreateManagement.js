var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "vue-property-decorator", "../service"], function (require, exports, vue_property_decorator_1, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var AuthorityCreateManagement = (function (_super) {
        __extends(AuthorityCreateManagement, _super);
        function AuthorityCreateManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.UserName = '';
            _this_1.Password = '';
            _this_1.ConfirmPassword = '';
            _this_1.PasswordTypes = "password";
            _this_1.ShowPassword = false;
            _this_1.RoleId = '';
            _this_1.RoleList = [];
            _this_1.Options = [];
            _this_1.SaveForm = 'Loading';
            _this_1.Message = '';
            return _this_1;
        }
        AuthorityCreateManagement.prototype.created = function () {
            var _this = this;
            _this.GetRoleOptions();
        };
        AuthorityCreateManagement.prototype.GetRoleOptions = function () {
            var _this = this;
            service_1.default.GetRoleOptions().then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.RoleList = res.Data;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        AuthorityCreateManagement.prototype.ChangePasswordType = function () {
            var _this = this;
            if (_this.PasswordTypes == "password") {
                _this.PasswordTypes = "text";
                _this.ShowPassword = true;
            }
            else {
                _this.PasswordTypes = "password";
                _this.ShowPassword = false;
            }
        };
        AuthorityCreateManagement.prototype.OnOptionsChange = function () {
            var _this = this;
            if (_this.RoleList) {
                var length_1 = _this.RoleList.length;
                for (var i = 0; i < length_1; i++) {
                    var role = {
                        value: _this.RoleList[i].Id,
                        text: _this.RoleList[i].Name
                    };
                    _this.Options.push(role);
                }
                _this.RoleId = _this.Options[0].value;
            }
        };
        Object.defineProperty(AuthorityCreateManagement.prototype, "VisibleStopAuthority", {
            get: function () {
                return Boolean(this.UserName && this.Password == this.ConfirmPassword);
            },
            enumerable: true,
            configurable: true
        });
        AuthorityCreateManagement.prototype.SetCreateUser = function () {
            var _this = this;
            _this.$bvModal.show('AuthorityModal');
            _this.SaveForm = 'Loading';
            var _a = this, UserName = _a.UserName, Password = _a.Password, ConfirmPassword = _a.ConfirmPassword, RoleId = _a.RoleId;
            var model = {
                UserName: UserName,
                Password: Password,
                ConfirmPassword: ConfirmPassword,
                RoleId: RoleId
            };
            _this.CreateUser(model);
        };
        AuthorityCreateManagement.prototype.CreateUser = function (data) {
            var _this = this;
            console.log(data);
            service_1.default.CreateUser(data).then(function (res) {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    _this.Message = res.Message;
                    console.log(res);
                }
                if (res.Success) {
                    _this.Message = res.Message;
                    _this.SaveForm = 'Success';
                }
            }).catch(function (err) {
                _this.SaveForm = 'Error';
                console.log(err);
            });
        };
        AuthorityCreateManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('AuthorityModal');
        };
        AuthorityCreateManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('AuthorityModal');
            var locationURL = this.httpURL.split("/Account")[0];
            document.location.href = locationURL;
        };
        __decorate([
            vue_property_decorator_1.Watch('RoleList')
        ], AuthorityCreateManagement.prototype, "OnOptionsChange", null);
        AuthorityCreateManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#LatestNewsCreateManagement'
            })
        ], AuthorityCreateManagement);
        return AuthorityCreateManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = AuthorityCreateManagement;
});
//# sourceMappingURL=AuthorityCreateManagement.js.map