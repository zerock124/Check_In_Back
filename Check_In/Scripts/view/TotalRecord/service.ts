import { ITotalRecordService, SearchModel, OptionsViewModel, Check_AllViewModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class TotalRecordService implements ITotalRecordService {

    GetTotalRecord(searchModel: SearchModel): JQuery.jqXHR<ResponseViewModel<Check_AllViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/TotalRecord/GetTotalRecord',
            type: 'POST',
            data: {
                ...searchModel,
            }
        }
        return AjaxReturn(setting);
    }

    GetOptions(): JQuery.jqXHR<ResponseViewModel<OptionsViewModel>> {
        const setting: JQueryAjaxSettings = {
            url: '/TotalRecord/GetOptions',
            type: 'GET'
        }
        return AjaxReturn(setting);
    }

    GetCheck_All(ur_id: string): JQuery.jqXHR<ResponseViewModel<Check_AllViewModel>> {
        const setting: JQueryAjaxSettings = {
            url: `/TotalRecord/GetCheck_All?ur_id=${ur_id}`,
            type: 'GET'
        }
        return AjaxReturn(setting);
    }

    SubmitCheck_All(model: Check_AllViewModel): JQuery.jqXHR<VerityResult> {
        const setting: JQueryAjaxSettings = {
            url: `/TotalRecord/SubmitCheck_All`,
            type: 'POST',
            data: {
                ...model
            }
        }
        return AjaxReturn(setting);
    }
}

const check_all_service: ITotalRecordService = new TotalRecordService();
export default check_all_service;