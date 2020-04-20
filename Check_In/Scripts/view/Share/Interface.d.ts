interface Window {
	IsDebugging: boolean | undefined;
	BaseWebUrl: string;
	BasePath: string;
}

interface HtmlEvent extends Event {
	target: HTMLElement;
}

/**傳輸方法*/
type ajaxType = 'POST' | 'GET' | 'DELETE' | 'UPDATE';
/**資料型態*/
type ajaxDataType = 'xml' | 'json' | 'script' | 'html';
/**Ajax資料送出方式 */
type AjaxConvertDataType = 'JSON' | 'FormData';
/**擴充原Ajax設定內容 */
interface JQueryAjaxSettings {
	type?: ajaxType,
	dataType?: ajaxDataType
}

/**HttpStatusCode狀態碼定義*/
declare enum HttpStatusCode {
      /**請求已成功*/OK = 200,
      /**請求已經被實現，而且有一個新的資源已經依據請求的需要而建立*/CREATED = 201,
      /**伺服器已接受請求，但尚未處理。最終該請求可能會也可能不會被執行，並且可能在處理發生時被禁止。*/ACCEPTED = 202,
      /**伺服器成功處理了請求，沒有返回任何內容*/NO_CONTENT = 204,
      /**伺服器成功處理了請求，但沒有返回任何內容。與204回應不同，此回應要求請求者重設文件視圖。*/RESET_CONTENT = 205,
      /**用戶端錯誤，伺服器不會處理該請求*/BAD_REQUEST = 400,
      /**當前請求需要用戶驗證*/UNAUTHORIZED = 401,
      /**請求失敗，請求所希望得到的資源未被在伺服器上發現*/NOT_FOUND = 404,
      /**伺服器錯誤*/INTERNAL_SERVER_ERROR = 500,
      /**帳號停權中*/AccountStopAuthority = 459,
      /**帳號或密碼錯誤*/AccountOrPasswordError = 461
}

/**共用的後端返回Modal(自訂資料格式)*/
interface ResponseViewModel<T = any> {
	/**HttpStatusCode狀態碼*/
	HttpStatusCode: HttpStatusCode;
	/**是否成功*/
	Success: boolean;
	/**訊息，可為空值*/
	Message: string;
	/**資料,不限格式，可為空值*/
	Data?: T | null;
	/**例外狀況，可為空值*/
	Exception?: string,
	/**反應時間*/
	ResponseTime: string
}

interface DataVerityResult<T = any> {
	/**是否成功*/
	Success: boolean;
	/**訊息，可為空值*/
	Message: string;
	/**資料,不限格式，可為空值*/
	Data?: T | null;
}

interface VerityResult {
	/**是否成功*/
	Success: boolean;
	/**訊息，可為空值*/
	Message: string;
}

/**伺服器回應內容增加包含分頁資訊 */
interface ResWithPaginationViewModel<T = any> extends ResponseViewModel<T> {
	Pagination: PaginationViewModel;
}


/**分頁選項內容 */
interface PageStationOption {
	/**總頁數 */
	total: number;
	/**當前頁數 */
	now: number;
	/**總項目數量 */
	itemlength: number;
	/**每頁顯示數量 */
	perpage: number;
}

/**新製分頁回應內容，
 * 可使用 ResWithPaginationViewModel 取得完整的伺服器回應資訊 
 */
interface PaginationViewModel {
	/**每頁顯示數量 */
	PerPage: number;
	/**當前頁數 */
	CurrentPage: number;
	/**總頁數 */
	TotalPage: number;
	/**總計數量 */
	TotalCounts: number;
}
interface SendPaginationModel {
	/**每頁顯示數量 */
	PerPage: number;
	/**當前頁數 */
	CurrentPage: number;
}
