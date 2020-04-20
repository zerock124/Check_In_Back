import { ICheckRecordService, SearchModel, UserViewModel, Check_InViewModel, SearchTimeModel, UpdateCheckTimeViewModel} from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class CheckRecordService implements ICheckRecordService {

    GetCheckRecord(searchModel: SearchModel, pagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<Check_InViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/CheckRecord/GetCheckRecord',
            type: 'POST',
            data: {
                ...searchModel,
                ...pagination
            }
        }
        return AjaxReturn(setting);
    }

    GetUserList(): JQuery.jqXHR<ResponseViewModel<UserViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/CheckRecord/GetUserList',
            type: 'GET'
        }
        return AjaxReturn(setting);
    }

    GetClick_In(ci_sn: number): JQuery.jqXHR<ResponseViewModel<Check_InViewModel>> {
        const setting: JQueryAjaxSettings = {
            url: `/CheckRecord/GetClick_In?ci_sn=${ci_sn}`,
            type: 'GET'
        }
        return AjaxReturn(setting);
    }

    SubmitClick_In(model: Check_InViewModel, time: UpdateCheckTimeViewModel): JQuery.jqXHR<VerityResult> {
        const setting: JQueryAjaxSettings = {
            url: '/CheckRecord/SubmitClick_In',
            type: 'POST',
            data: {
                model,
                time
            }
        }
        return AjaxReturn(setting);
    }

    GetExportCheckRecord(model: SearchTimeModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: '/CheckRecord/GetExportCheckRecord',
            type: 'POST',
            data: model
        }
        return AjaxReturn(setting);
    }
}

const check_in_service: ICheckRecordService = new CheckRecordService();
export default check_in_service;