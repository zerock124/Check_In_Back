import { Vue, Component, Watch } from 'vue-property-decorator'
import { UserViewModel, SearchModel, Check_InViewModel, SearchTimeModel, UpdateCheckTimeViewModel } from './model'
import service from './service'
import moment = require('moment')
import { dateToWeek, dateToDateString, TimeSpanToString } from "../Share/FilterFunction";
import { UrlPathEnum } from '../Share/Enums';
import { GetExportExcelUrl } from '../Share/PublicFunction';

@Component({
    template: '#CheckRecordManagement',
    filters: {
        dateToWeek,
        dateToDateString,
        TimeSpanToString
    }
})


export default class CheckRecordManagement extends Vue {
    UserList: UserViewModel[] = [];
    Check_InList: Check_InViewModel[] = [];
    StartDateTime: string = moment().startOf('day').format('YYYY-MM-DD');
    EndDateTime: string = moment().endOf('day').format('YYYY-MM-DD');

    ur_ac: string = '';
    workDays: number = 0;
    attendantDays: number = 0;

    searchmodel: SearchModel | null = null

    SaveForm: string = 'Form';
    Check_In_Item: Check_InViewModel | null = null;

    ci_ut: string = '';
    ci_dt: string = '';
    Checkci_ut: boolean = false;
    Checkci_dt: boolean = false;

    Selectd: number = 0;
    Options: object[] = [{
        value: 0,
        text: '帳號'
    }];

    Query: string = '';

    Pagination: PaginationViewModel = { PerPage: 20, CurrentPage: 1, TotalCounts: 0, TotalPage: 1 };
    PerPage: number = 20;
    CurrentPage: number = 1;
    TotalPage: number = 1;
    TotalCounts: number = 0;

    created() {
        const _this = this;
        _this.SetDefaultDate();
        _this.GetUserList();
    }

    SetDefaultDate() {
        const _this = this;
        const nowday = moment().format('DD');
        _this.StartDateTime = moment(_this.StartDateTime).startOf('day').format('YYYY-MM-01');
        _this.EndDateTime = moment(_this.EndDateTime).endOf('day').format('YYYY-MM' + '-' + nowday);

        const searchModel: SearchModel = {
            ur_id: '',
            StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
            EndDateTime: moment(_this.EndDateTime).endOf('day').toDate(),
            Query: ''
        };
        _this.searchmodel = searchModel;

        _this.SetWorkDays();
        _this.GetCheckRecord(searchModel);
    }

    SetWorkDays() {
        const _this = this;
        let diffdays = moment(_this.EndDateTime).diff(moment(_this.StartDateTime), 'day') + 1;
        let weekdays = 0;
        for (var i = 0; i < diffdays; i++) {
            const weekday = moment(_this.StartDateTime).add(i, 'day').weekday();
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
    }

    GetUserList() {
        const _this = this;
        service.GetUserList().then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                console.log(res);
                _this.UserList = res.Data;
                const length = _this.UserList.length;
                for (var i = 0; i < length; i++) {
                    _this.UserList[i].ur_hover = false;
                }
                _this.SetDefaultUser();
            }
        }).catch(err => {
            console.log(err);
        })
    }

    SetDefaultUser() {
        const _this = this;
        const url: string = location.href;
        const ur_id: string = url.split('?ur_id=')[1];
        if (ur_id) {
            if (ur_id.length > 0)
                _this.SetUrID(ur_id);
        }
    }

    @Watch('UserList')
    GetImageUrl() {
        const _this = this;
        if (_this.UserList) {
            const BasePath = window.BasePath; // _Layout.cshtml
            var length = _this.UserList.length;
            for (var i = 0; i < length; i++) {
                _this.UserList[i].ur_imurl = BasePath + UrlPathEnum.UserPhoto + '?filename=' + _this.UserList[i].ur_im;
            }
        }
    }

    SetCheckRecord() {
        const _this = this;
        if (_this.searchmodel) {

            _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
            _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();

            _this.SetWorkDays();

            if (_this.searchmodel)
                _this.GetCheckRecord(_this.searchmodel);
        }
    }

    SearchCheckRecord() {
        const _this = this;
        if (_this.searchmodel) {
            const length = _this.UserList.length;
            for (var i = 0; i < length; i++) {
                _this.UserList[i].ur_hover = false;
            }
            _this.searchmodel.ur_id = '';
            _this.searchmodel.Query = _this.Query;
            _this.CurrentPage = 1;
            _this.GetCheckRecord(_this.searchmodel);
        }
    }

    SetUrID(ur_id: string) {
        const _this = this;
        if (_this.searchmodel) {
            _this.searchmodel.ur_id = ur_id;
            _this.searchmodel.Query = '';
            const length = _this.UserList.length;
            for (var i = 0; i < length; i++) {
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
    }

    GetCheckRecord(serarhModel: SearchModel) {
        const _this = this;

        _this.SetWorkDays();

        const sendPagination: SendPaginationModel = {
            PerPage: _this.Pagination.PerPage,
            CurrentPage: _this.Pagination.CurrentPage
        }

        service.GetCheckRecord(serarhModel, sendPagination).then(res => {
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
        }).catch(err => {
            console.log(err);
        })
    }

    EditCheckRecord(ur_id: number) {
        const _this = this;
        _this.SaveForm = 'Form';
        service.GetClick_In(ur_id).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.Check_In_Item = res.Data;
                if (_this.Check_In_Item) {
                    _this.Check_In_Item.ci_da = moment(res.Data.ci_da).toDate();
                    _this.Check_In_Item.ci_ct = moment(res.Data.ci_ct).toDate();
                    _this.ci_ut = TimeSpanToString(_this.Check_In_Item.ci_ut);
                    _this.ci_dt = TimeSpanToString(_this.Check_In_Item.ci_dt);
                }
            }
        }).catch(err => {
            console.log(err);
        })
        _this.$bvModal.show('CheckRecordModal');
    }

    SubmitCheck_In() {
        const _this = this;
        _this.SaveForm = 'Loading';

        if (_this.Check_In_Item) {
            const time: UpdateCheckTimeViewModel = {
                ci_ut: _this.ci_ut,
                ci_dt: _this.ci_dt
            }

            service.SubmitClick_In(_this.Check_In_Item, time).then(res => {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    console.log(res);
                }
                if (res.Success) {
                    if (_this.searchmodel)
                        _this.SetUrID(_this.searchmodel.ur_id);
                    _this.CloseModal();
                }
            }).catch(err => {
                console.log(err);
                _this.SaveForm = 'Error';
            })
        }
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('CheckRecordModal');
    }

    ExportExcel() {
        const _this = this;
        const searchTime: SearchTimeModel = {
            StartDateTime: moment(_this.StartDateTime).startOf('day').toDate(),
            EndDateTime: moment(_this.EndDateTime).endOf('day').toDate()
        }
        service.GetExportCheckRecord(searchTime).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                const StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM-DD 00:mm:ss");
                const EndDateTime = moment(res.Data.EndDateTime).format("YYYY-MM-DD hh:mm:ss");
                const url: string = '/CheckRecord/ExportCheckRecord?StartDateTime=' + StartDateTime + '&EndDateTime=' + EndDateTime;
                const exporturl = GetExportExcelUrl(url);
                window.location.href = exporturl;
            }
        }).catch(err => {
            console.log(err);
        })
    }

    @Watch('Pagination')
    OnPaginationChange() {
        const _this = this;
        if (_this.Pagination) {
            _this.PerPage = _this.Pagination.PerPage;
            _this.CurrentPage = _this.Pagination.CurrentPage;
            _this.TotalPage = _this.Pagination.TotalPage;
            _this.TotalCounts = _this.Pagination.TotalCounts;
        }
    }

    //----------------使用此方法會因為頁數不斷變更導致呼叫多次-----------------
    //----------------不再使用此方法去判斷是否換頁---------------------------
    //@Watch('PerPage')
    //OnPerPageChange() {
    //    this.SetSendPagination();
    //}

    //@Watch('CurrentPage')
    //OnCurrentPageChange() {
    //    this.SetSendPagination();
    //}

    //------------------------------------------------------------------



    SetSendPagination() {
        const _this = this;
        _this.Pagination.PerPage = _this.PerPage;
        _this.Pagination.CurrentPage = _this.CurrentPage;
        if (!_this.searchmodel) { return; }
        else {
            if (_this.searchmodel.Query) {
                _this.searchmodel.Query = _this.Query;
            }
            _this.searchmodel.StartDateTime = moment(_this.StartDateTime).startOf('day').toDate();
            _this.searchmodel.EndDateTime = moment(_this.EndDateTime).endOf('day').toDate();
            _this.GetCheckRecord(_this.searchmodel);
        }
    }

    @Watch('ci_ut')
    OnCi_UtChange() {
        const _this = this;
        if (_this.Check_In_Item) {
            const ci_da = moment(_this.Check_In_Item.ci_da).format("YYYY-MM-DD");
            const ci_ut = ci_da + " " + _this.ci_ut;
            const nowDate = moment().format("YYYY-MM-DD hh:mm:ss");
            _this.Checkci_ut = moment(ci_ut).isBefore(nowDate, "second");
            if (_this.ci_ut == '-') {
                _this.Checkci_ut = true;
            }
        }
    }

    @Watch('ci_dt')
    OnCi_DtChange() {
        const _this = this;
        if (_this.Check_In_Item) {
            const ci_da = moment(_this.Check_In_Item.ci_da).format("YYYY-MM-DD");
            const ci_dt = ci_da + " " + _this.ci_dt;
            const nowDate = moment().format("YYYY-MM-DD hh:mm:ss");
            _this.Checkci_dt = moment(ci_dt).isBefore(nowDate, "second");
            if (_this.ci_dt == '-') {
                _this.Checkci_dt = true;
            }
        }
    }

    get VisibleStopAuthority() {
        return Boolean(this.Checkci_ut && this.Checkci_dt);
    }
}
