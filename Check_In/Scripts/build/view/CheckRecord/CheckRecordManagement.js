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
define(["require", "exports", "vue-property-decorator", "./service", "moment", "../Share/FilterFunction", "../Share/Enums", "../Share/PublicFunction"], function (require, exports, vue_property_decorator_1, service_1, moment, FilterFunction_1, Enums_1, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var CheckRecordManagement = (function (_super) {
        __extends(CheckRecordManagement, _super);
        function CheckRecordManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.UserList = [];
            _this_1.Check_InList = [];
            _this_1.StartDateTime = moment().startOf('day').format('YYYY-MM-DD');
            _this_1.EndDateTime = moment().endOf('day').format('YYYY-MM-DD');
            _this_1.ur_ac = '';
            _this_1.workDays = 0;
            _this_1.attendantDays = 0;
            _this_1.searchmodel = null;
            _this_1.SaveForm = 'Form';
            _this_1.Check_In_Item = null;
            _this_1.ci_ut = '';
            _this_1.ci_dt = '';
            _this_1.Checkci_ut = false;
            _this_1.Checkci_dt = false;
            _this_1.Selectd = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '帳號'
                }];
            _this_1.Query = '';
            _this_1.Pagination = { PerPage: 20, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };
            _this_1.PerPage = 20;
            _this_1.CurrentPage = 1;
            _this_1.TotalPage = 1;
            _this_1.TotalCounts = 0;
            return _this_1;
        }
        CheckRecordManagement.prototype.created = function () {
            var _this = this;
            _this.SetDefaultDate();
            _this.GetUserList();
        };
        CheckRecordManagement.prototype.SetDefaultDate = function () {
            var _this = this;
            var nowday = moment().format('DD');
            _this.StartDateTime = moment(_this.StartDateTime).startOf('day').format('YYYY-MM-01');
            _this.EndDateTime = moment(_this.EndDateTime).endOf('day').format('YYYY-MM' + '-' + nowday);
            var searchModel = {
                ur_id: '',
                StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
                EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
                Query: ''
            };
            _this.searchmodel = searchModel;
            _this.SetWorkDays();
            _this.GetCheckRecord(searchModel);
        };
        CheckRecordManagement.prototype.SetWorkDays = function () {
            var _this = this;
            var diffdays = moment(_this.EndDateTime).diff(moment(_this.StartDateTime), 'day') + 1;
            var weekdays = 0;
            for (var i = 0; i < diffdays; i++) {
                var weekday = moment(_this.StartDateTime).add(i, 'day').weekday();
                switch (weekday) {
                    case 0:
                        weekdays++;
                        break;
                    case 6:
                        weekdays++;
                        break;
                    default:
                        break;
                }
            }
            _this.workDays = diffdays - weekdays;
        };
        CheckRecordManagement.prototype.GetUserList = function () {
            var _this = this;
            service_1.default.GetUserList().then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    console.log(res);
                    _this.UserList = res.Data;
                    var length_1 = _this.UserList.length;
                    for (var i = 0; i < length_1; i++) {
                        _this.UserList[i].ur_hover = false;
                    }
                    _this.SetDefaultUser();
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        CheckRecordManagement.prototype.SetDefaultUser = function () {
            var _this = this;
            var url = location.href;
            var ur_id = url.split('?ur_id=')[1];
            if (ur_id) {
                if (ur_id.length > 0)
                    _this.SetUrID(ur_id);
            }
        };
        CheckRecordManagement.prototype.GetImageUrl = function () {
            var _this = this;
            if (_this.UserList) {
                var BasePath = window.BasePath;
                var length = _this.UserList.length;
                for (var i = 0; i < length; i++) {
                    _this.UserList[i].ur_imurl = BasePath + Enums_1.UrlPathEnum.UserPhoto + '?filename=' + _this.UserList[i].ur_im;
                }
            }
        };
        CheckRecordManagement.prototype.SetCheckRecord = function () {
            var _this = this;
            if (_this.searchmodel) {
                _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
                _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
                _this.SetWorkDays();
                if (_this.searchmodel)
                    _this.GetCheckRecord(_this.searchmodel);
            }
        };
        CheckRecordManagement.prototype.SearchCheckRecord = function () {
            var _this = this;
            if (_this.searchmodel) {
                var length_2 = _this.UserList.length;
                for (var i = 0; i < length_2; i++) {
                    _this.UserList[i].ur_hover = false;
                }
                _this.searchmodel.ur_id = '';
                _this.searchmodel.Query = _this.Query;
                _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
                _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
                _this.CurrentPage = 1;
                _this.GetCheckRecord(_this.searchmodel);
            }
        };
        CheckRecordManagement.prototype.SetUrID = function (ur_id) {
            var _this = this;
            if (_this.searchmodel) {
                _this.searchmodel.ur_id = ur_id;
                _this.searchmodel.Query = '';
                var length_3 = _this.UserList.length;
                for (var i = 0; i < length_3; i++) {
                    if (_this.UserList[i].ur_id == ur_id) {
                        _this.UserList[i].ur_hover = true;
                        _this.Query = _this.UserList[i].ur_ac;
                    }
                    else
                        _this.UserList[i].ur_hover = false;
                }
                _this.Pagination.PerPage = 20;
                _this.Pagination.CurrentPage = 1;
                _this.GetCheckRecord(_this.searchmodel);
            }
        };
        CheckRecordManagement.prototype.GetCheckRecord = function (serarhModel) {
            var _this = this;
            _this.SetWorkDays();
            var sendPagination = {
                PerPage: _this.Pagination.PerPage,
                CurrentPage: _this.Pagination.CurrentPage
            };
            service_1.default.GetCheckRecord(serarhModel, sendPagination).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Success) {
                    _this.Pagination = res.Pagination;
                    if (res.Data) {
                        _this.Check_InList = res.Data;
                        _this.attendantDays = res.Data.length;
                    }
                    else {
                        _this.Check_InList = [];
                        _this.attendantDays = 0;
                    }
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        CheckRecordManagement.prototype.EditCheckRecord = function (ur_id) {
            var _this = this;
            _this.SaveForm = 'Form';
            service_1.default.GetClick_In(ur_id).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.Check_In_Item = res.Data;
                    if (_this.Check_In_Item) {
                        _this.Check_In_Item.ci_da = moment(res.Data.ci_da).toDate();
                        _this.Check_In_Item.ci_ct = moment(res.Data.ci_ct).toDate();
                        _this.ci_ut = FilterFunction_1.TimeSpanToString(_this.Check_In_Item.ci_ut);
                        _this.ci_dt = FilterFunction_1.TimeSpanToString(_this.Check_In_Item.ci_dt);
                    }
                }
            }).catch(function (err) {
                console.log(err);
            });
            _this.$bvModal.show('CheckRecordModal');
        };
        CheckRecordManagement.prototype.SubmitCheck_In = function () {
            var _this = this;
            _this.SaveForm = 'Loading';
            if (_this.Check_In_Item) {
                var time = {
                    ci_ut: _this.ci_ut,
                    ci_dt: _this.ci_dt
                };
                service_1.default.SubmitClick_In(_this.Check_In_Item, time).then(function (res) {
                    if (!res.Success) {
                        _this.SaveForm = 'Error';
                        console.log(res);
                    }
                    if (res.Success) {
                        if (_this.searchmodel)
                            _this.SetUrID(_this.searchmodel.ur_id);
                        _this.CloseModal();
                    }
                }).catch(function (err) {
                    console.log(err);
                    _this.SaveForm = 'Error';
                });
            }
        };
        CheckRecordManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('CheckRecordModal');
        };
        CheckRecordManagement.prototype.ExportExcel = function () {
            var _this = this;
            var searchTime = {
                StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
                EndDateTime: moment(_this.EndDateTime).endOf('day').toDate()
            };
            service_1.default.GetExportCheckRecord(searchTime).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    var StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM-DD 00:mm:ss");
                    var EndDateTime = moment(res.Data.EndDateTime).format("YYYY-MM-DD hh:mm:ss");
                    var url = '/CheckRecord/ExportCheckRecord?StartDateTime=' + StartDateTime + '&EndDateTime=' + EndDateTime;
                    var exporturl = PublicFunction_1.GetExportExcelUrl(url);
                    window.location.href = exporturl;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        CheckRecordManagement.prototype.OnPaginationChange = function () {
            var _this = this;
            if (_this.Pagination) {
                _this.PerPage = _this.Pagination.PerPage;
                _this.CurrentPage = _this.Pagination.CurrentPage;
                _this.TotalPage = _this.Pagination.TotalPage;
                _this.TotalCounts = _this.Pagination.TotalCounts;
            }
        };
        CheckRecordManagement.prototype.SetSendPagination = function () {
            var _this = this;
            _this.Pagination.PerPage = _this.PerPage;
            _this.Pagination.CurrentPage = _this.CurrentPage;
            if (!_this.searchmodel) {
                return;
            }
            else {
                if (_this.searchmodel.Query) {
                    _this.searchmodel.Query = _this.Query;
                }
                _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
                _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
                _this.GetCheckRecord(_this.searchmodel);
            }
        };
        CheckRecordManagement.prototype.OnCi_UtChange = function () {
            var _this = this;
            if (_this.Check_In_Item) {
                var ci_da = moment(_this.Check_In_Item.ci_da).format("YYYY-MM-DD");
                var ci_ut = ci_da + " " + _this.ci_ut;
                var nowDate = moment().format("YYYY-MM-DD hh:mm:ss");
                _this.Checkci_ut = moment(ci_ut).isBefore(nowDate, "second");
                if (_this.ci_ut == '-') {
                    _this.Checkci_ut = true;
                }
            }
        };
        CheckRecordManagement.prototype.OnCi_DtChange = function () {
            var _this = this;
            if (_this.Check_In_Item) {
                var ci_da = moment(_this.Check_In_Item.ci_da).format("YYYY-MM-DD");
                var ci_dt = ci_da + " " + _this.ci_dt;
                var nowDate = moment().format("YYYY-MM-DD hh:mm:ss");
                _this.Checkci_dt = moment(ci_dt).isBefore(nowDate, "second");
                if (_this.ci_dt == '-') {
                    _this.Checkci_dt = true;
                }
            }
        };
        Object.defineProperty(CheckRecordManagement.prototype, "VisibleStopAuthority", {
            get: function () {
                return Boolean(this.Checkci_ut && this.Checkci_dt);
            },
            enumerable: true,
            configurable: true
        });
        __decorate([
            vue_property_decorator_1.Watch('UserList')
        ], CheckRecordManagement.prototype, "GetImageUrl", null);
        __decorate([
            vue_property_decorator_1.Watch('Pagination')
        ], CheckRecordManagement.prototype, "OnPaginationChange", null);
        __decorate([
            vue_property_decorator_1.Watch('ci_ut')
        ], CheckRecordManagement.prototype, "OnCi_UtChange", null);
        __decorate([
            vue_property_decorator_1.Watch('ci_dt')
        ], CheckRecordManagement.prototype, "OnCi_DtChange", null);
        CheckRecordManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#CheckRecordManagement',
                filters: {
                    dateToWeek: FilterFunction_1.dateToWeek,
                    dateToDateString: FilterFunction_1.dateToDateString,
                    TimeSpanToString: FilterFunction_1.TimeSpanToString
                }
            })
        ], CheckRecordManagement);
        return CheckRecordManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = CheckRecordManagement;
});
//# sourceMappingURL=CheckRecordManagement.js.map