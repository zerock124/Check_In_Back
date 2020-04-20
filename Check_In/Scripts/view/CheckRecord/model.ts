export interface ICheckRecordService {
    /**
     * 取得打卡紀錄
     * @param SearchModel
     */
    GetCheckRecord(searchModel: SearchModel, pagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<Check_InViewModel[]>>;

    GetUserList(): JQuery.jqXHR<ResponseViewModel<UserViewModel[]>>;

    GetClick_In(ci_sn: number): JQuery.jqXHR<ResponseViewModel<Check_InViewModel>>;

    SubmitClick_In(model: Check_InViewModel, time: UpdateCheckTimeViewModel): JQuery.jqXHR<VerityResult>;

    GetExportCheckRecord(model: SearchTimeModel): JQuery.jqXHR<ResponseViewModel>;
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
    /**是否點擊 */
    ur_hover: boolean;
}

export interface Check_InViewModel {
    /**打卡記錄編號 */
    ci_sn: number;
    /**使用者ID */
    ur_id: string;
    /**打卡日期 */
    ci_da: Date;
    /**上班時間 */
    ci_ut: string;
    /**下班時間 */
    ci_dt: string;
    /**帳號 */
    ur_ac: string;
    /**建立日期 */
    ci_ct: Date;
}

export interface SearchModel {
    ur_id: string;
    StartDateTime: Date ;
    EndDateTime: Date ;
    Query: string;
}

export interface SearchTimeModel {
    StartDateTime: Date;
    EndDateTime: Date;
}

export interface RegisterViewModel {
    ur_ac: string;
    ur_pw: string;
}

export interface UpdateCheckTimeViewModel {
    ci_ut: string;
    ci_dt: string;
}