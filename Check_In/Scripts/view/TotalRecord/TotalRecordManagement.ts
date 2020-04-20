import { Vue, Component, Watch } from 'vue-property-decorator'
import { UserViewModel, SearchModel, Check_AllViewModel } from './model'
import service from './service'
import moment = require('moment')
import { dateToWeek, dateToDateString } from "../Share/FilterFunction";

@Component({
    template: '#TotalRecordManagement',
    filters: {
        dateToWeek,
        dateToDateString
    }
})


export default class TotalRecordManagement extends Vue {
    Check_AllList: Check_AllViewModel[] = [];
    StartDateTime: string = moment().startOf('day').format('YYYY-MM-01');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-' + moment().daysInMonth());
    Query: string = '';

    MaxDate: string = '';
    MinDate: string = '';

    searchmodel: SearchModel | null = null

    Check_All_Item: Check_AllViewModel | null = null;

    Selectd: number = 0;
    Options: object[] = [{
        value: 0,
        text: '帳號'
    }];

    Year: string = moment().format("YYYY");
    Years: object[] = [];
    Month: string = moment().format("M");
    Months: object[] = [];

    SaveForm: string = '';

    created() {
        const _this = this;
        _this.GetOptions();
        _this.SetCheckRecord();
    }

    GetOptions() {
        const _this = this;
        service.GetOptions().then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.Years = res.Data.YearOptions;
                _this.Months = res.Data.MonthOptions;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    SetCheckRecord() {
        const _this = this;

        const serarhModel: SearchModel = {
            SearchEnum: 1,
            StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
            EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
            Query: _this.Query
        };

        _this.searchmodel = serarhModel;
        _this.GetCheckRecord(_this.searchmodel);
    }

    GetCheckRecord(serarhModel: SearchModel) {
        const _this = this;

        service.GetTotalRecord(serarhModel).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.Check_AllList = res.Data;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    @Watch("Year")
    OnYearChange() {
        const _this = this;
        _this.StartDateTime = _this.Year + "-" + _this.Month + "-01";
        _this.EndDateTime = _this.Year + "-" + _this.Month + "-" + moment(_this.StartDateTime).daysInMonth();
        _this.SetCheckRecord();
    }

    @Watch("Month")
    OnMonthChange() {
        const _this = this;
        _this.StartDateTime = _this.Year + "-" + _this.Month + "-01";
        _this.EndDateTime = _this.Year + "-" + _this.Month + "-" + moment(_this.StartDateTime).daysInMonth();
        _this.SetCheckRecord();
    }

    //EditTotalRecord(ur_id: string) {
    //    const _this = this;
    //    _this.SaveForm = 'Form';
    //    service.GetCheck_All(ur_id).then(res => {
    //        if (!res.Success) {
    //            console.log(res);
    //        }
    //        if (res.Data) {
    //            _this.Check_All_Item = res.Data;
    //            if (_this.Check_All_Item) {
    //            }
    //        }
    //    }).catch(err => {
    //        console.log(err);
    //    })
    //    _this.$bvModal.show('TotalRecordModal');
    //}

    //SubmitCheck_All() {
    //    const _this = this;
    //    _this.SaveForm = 'Loading';

    //    if (_this.Check_All_Item) {

    //        service.SubmitCheck_All(_this.Check_All_Item).then(res => {
    //            if (!res.Success) {
    //                _this.SaveForm = 'Error';
    //                console.log(res);
    //            }
    //            if (res.Success) {
    //                _this.SaveForm = 'Success';

    //                _this.SetCheckRecord();
    //            }
    //        }).catch(err => {
    //            console.log(err);
    //            _this.SaveForm = 'Error';
    //        })
    //    }
    //}

    //CloseModal() {
    //    const _this = this;
    //    _this.$bvModal.hide('TotalRecordModal');
    //}
}
