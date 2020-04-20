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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums", "../service"], function (require, exports, vue_property_decorator_1, Enums_1, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var UserEditManagement = (function (_super) {
        __extends(UserEditManagement, _super);
        function UserEditManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.image = '';
            _this_1.PhotoFile = null;
            _this_1.DefaultImage = '';
            _this_1.ImageName = '';
            _this_1.ur_id = '';
            _this_1.ur_ac = '';
            _this_1.ur_pw = '';
            _this_1.ConfirmPassword = '';
            _this_1.PasswordTypes = "password";
            _this_1.ShowPassword = false;
            _this_1.Select = '';
            _this_1.ListItem = null;
            _this_1.SaveForm = 'Loading';
            _this_1.Message = '';
            return _this_1;
        }
        UserEditManagement.prototype.created = function () {
            var _this = this;
            _this.ur_id = _this.httpURL.split("?ur_id=")[1];
            _this.GetUserItem(_this.ur_id);
        };
        UserEditManagement.prototype.GetUserItem = function (ur_id) {
            var _this = this;
            service_1.default.GetEditUserItem(ur_id).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ListItem = res.Data;
                    _this.ur_ac = res.Data.ur_ac;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        UserEditManagement.prototype.ChangePasswordType = function () {
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
        UserEditManagement.prototype.SetEditUser = function () {
            var _this = this;
            _this.$bvModal.show('UserModel');
            _this.SaveForm = 'Loading';
            var _a = this, PhotoFile = _a.PhotoFile, ur_id = _a.ur_id, ur_ac = _a.ur_ac, ur_pw = _a.ur_pw;
            var _formdata = new FormData();
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '');
            _formdata.append('ur_id', ur_id);
            _formdata.append('ur_ac', ur_ac);
            _formdata.append('ur_pw', ur_pw);
            _this.EditUserItem(_formdata);
        };
        UserEditManagement.prototype.EditUserItem = function (model) {
            var _this = this;
            if (_this.ListItem) {
                service_1.default.EditUserItem(model).then(function (res) {
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
            }
        };
        Object.defineProperty(UserEditManagement.prototype, "VisibleStopAuthority", {
            get: function () {
                var ur_ac = this.ur_ac, ur_pw = this.ur_pw, ConfirmPassword = this.ConfirmPassword;
                return Boolean(ur_ac && ur_pw && ConfirmPassword && ur_pw == ConfirmPassword);
            },
            enumerable: true,
            configurable: true
        });
        UserEditManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('UserModel');
        };
        UserEditManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('UserModel');
            var locationURL = this.httpURL.split("/Edit?")[0];
            document.location.href = locationURL;
        };
        UserEditManagement.prototype.GetDefaultImgUrl = function () {
            var _this = this;
            var BasePath = window.BasePath;
            if (_this.ListItem) {
                var photo = _this.ListItem.ur_im;
                _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.UserPhoto + '?filename=' + photo;
            }
        };
        ;
        UserEditManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        UserEditManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        __decorate([
            vue_property_decorator_1.Watch('ListItem')
        ], UserEditManagement.prototype, "GetDefaultImgUrl", null);
        UserEditManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#UserEditManagement'
            })
        ], UserEditManagement);
        return UserEditManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = UserEditManagement;
});
//# sourceMappingURL=UserEditManagement.js.map