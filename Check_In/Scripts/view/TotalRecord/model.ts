export interface ITotalRecordService {
    /**
     * 取得打卡彙總紀錄
     * @param SearchModel
     */
    GetTotalRecord(searchModel: SearchModel): JQuery.jqXHR<ResponseViewModel<Check_AllViewModel[]>>;

    GetOptions(): JQuery.jqXHR<ResponseViewModel<OptionsViewModel>>

    GetCheck_All(ur_id: string): JQuery.jqXHR<ResponseViewModel<Check_AllViewModel>>;

    SubmitCheck_All(model: Check_AllViewModel): JQuery.jqXHR<VerityResult>;
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
    /**建立時間 */
    ci_ct: Date;
    /**備註 */
    Remark: string;
    /**帳號 */
    ur_ac: string;
}

export interface Check_AllViewModel {
    /**彙總記錄編號 */
    ca_sn: number;
    /**使用者ID */
    ur_id: string;
    /**彙總月份 */
    ca_mo: number;
    /**工作天數 */
    ca_dy: number;
    /**出勤天數 */
    ca_on: number;
    /**總工時 */
    ca_hr: number;
    /**平均工時 */
    ca_hr_avg: number;
    /**帳號 */
    ur_ac: string;

}

export interface OptionsViewModel {
    YearOptions: Options[];
    MonthOptions: Options[];
}

export interface Options {
    value: number;
    text: string;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date;
    EndDateTime: Date;
    Query: string;
}

export interface RegisterViewModel {
    ur_ac: string;
    ur_pw: string;
}