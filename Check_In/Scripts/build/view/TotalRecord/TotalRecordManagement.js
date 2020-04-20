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
define(["require", "exports", "vue-property-decorator", "./service", "moment", "../Share/FilterFunction"], function (require, exports, vue_property_decorator_1, service_1, moment, FilterFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var TotalRecordManagement = (function (_super) {
        __extends(TotalRecordManagement, _super);
        function TotalRecordManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.Check_AllList = [];
            _this_1.StartDateTime = moment().startOf('day').format('YYYY-MM-01');
            _this_1.EndDateTime = moment().endOf('day').format('YYYY-MM-' + moment().daysInMonth());
            _this_1.Query = '';
            _this_1.MaxDate = '';
            _this_1.MinDate = '';
            _this_1.searchmodel = null;
            _this_1.Check_All_Item = null;
            _this_1.Selectd = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '帳號'
                }];
            _this_1.Year = moment().format("YYYY");
            _this_1.Years = [];
            _this_1.Month = moment().format("M");
            _this_1.Months = [];
            _this_1.SaveForm = '';
            return _this_1;
        }
        TotalRecordManagement.prototype.created = function () {
            var _this = this;
            _this.GetOptions();
            _this.SetCheckRecord();
        };
        TotalRecordManagement.prototype.GetOptions = function () {
            var _this = this;
            service_1.default.GetOptions().then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.Years = res.Data.YearOptions;
                    _this.Months = res.Data.MonthOptions;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        TotalRecordManagement.prototype.SetCheckRecord = function () {
            var _this = this;
            var serarhModel = {
                SearchEnum: 1,
                StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
                EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
                Query: _this.Query
            };
            _this.searchmodel = serarhModel;
            _this.GetCheckRecord(_this.searchmodel);
        };
        TotalRecordManagement.prototype.GetCheckRecord = function (serarhModel) {
            var _this = this;
            service_1.default.GetTotalRecord(serarhModel).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.Check_AllList = res.Data;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        TotalRecordManagement.prototype.OnYearChange = function () {
            var _this = this;
            _this.StartDateTime = _this.Year + "-" + _this.Month + "-01";
            _this.EndDateTime = _this.Year + "-" + _this.Month + "-" + moment(_this.StartDateTime).daysInMonth();
            _this.SetCheckRecord();
        };
        TotalRecordManagement.prototype.OnMonthChange = function () {
            var _this = this;
            _this.StartDateTime = _this.Year + "-" + _this.Month + "-01";
            _this.EndDateTime = _this.Year + "-" + _this.Month + "-" + moment(_this.StartDateTime).daysInMonth();
            _this.SetCheckRecord();
        };
        __decorate([
            vue_property_decorator_1.Watch("Year")
        ], TotalRecordManagement.prototype, "OnYearChange", null);
        __decorate([
            vue_property_decorator_1.Watch("Month")
        ], TotalRecordManagement.prototype, "OnMonthChange", null);
        TotalRecordManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#TotalRecordManagement',
                filters: {
                    dateToWeek: FilterFunction_1.dateToWeek,
                    dateToDateString: FilterFunction_1.dateToDateString
                }
            })
        ], TotalRecordManagement);
        return TotalRecordManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = TotalRecordManagement;
});
//# sourceMappingURL=TotalRecordManagement.js.map