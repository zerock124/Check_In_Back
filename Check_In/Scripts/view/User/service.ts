import { UserViewModel, SearchModel, RegisterViewModel, IUserService } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class UserService implements IUserService {

    GetAuthorityList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<UserViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/User/GetUserList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    CreateUser(model: RegisterViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/User/CreateUser`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, 'FormData');
    }

    CheckAuthority(Id: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Authority/CheckAuthority?Id=${Id}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    GetEditUserItem(ur_id: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/User/GetEditUserItem?ur_id=${ur_id}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditUserItem(model: UserViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/User/EditUserItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, 'FormData');
    }

    DeleteUserItem(ur_id: string, ur_im: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/User/DeleteUserItem`,
            type: 'POST',
            data: {
                ur_id,
                ur_im
            }
        }
        return AjaxReturn(setting);
    }

    GetRoleOptions(): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: 'Authority/GetRoleOptions',
            type: 'GET',
        }
        return AjaxReturn(setting);
    }

}

const user_service: IUserService = new UserService();
export default user_service;