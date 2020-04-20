export interface IUserService {
    /**
     * 取得最新消息列表
     * @param SearchModel
     */
    GetAuthorityList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<UserViewModel[]>>
    /**
     * 新增帳號
     * @param model
     */
    CreateUser(model: RegisterViewModel): JQuery.jqXHR<ResponseViewModel>;
    /**
     * 確認是否有權限
     * @param CaseId
     */
    CheckAuthority(Id: string): JQuery.jqXHR<ResponseViewModel>
    /**
     * 取得變更帳號
     * @param LatestNewsId
     */
    GetEditUserItem(Id: string): JQuery.jqXHR<ResponseViewModel>
    /**
     * 變更帳號
     * @param model
     */
    EditUserItem(model: UserViewModel): JQuery.jqXHR<ResponseViewModel>
    /**
     * 刪除帳號
     * @param LatestNewsId
     */
    DeleteUserItem(ur_id: string, ur_im: string): JQuery.jqXHR<ResponseViewModel>

    GetRoleOptions(): JQuery.jqXHR<ResponseViewModel>
}

export interface UserViewModel {
    /**帳號編號 */
    ur_sn: number;
    /**帳號ID */
    ur_id: string;
    /**帳號 */
    ur_ac: string;
    /**密碼 */
    ur_pw: string;
    /**建立時間 */
    ur_ct: Date;
    /**是否停用 */
    ur_is: boolean;
    /**圖片 */
    ur_im: string;
    /**圖片路徑 */
    ur_imurl: string;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    Query: string;
}

export interface RegisterViewModel {
    ur_ac: string;
    ur_pw: string;
}