using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using ViewModels.User;
using ViewModels.Verity;

namespace Check_InDB.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 取得帳號權限管理列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetUserList(UserSearchModel searchModel, PaginationViewModel pagination);
        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> CreateUser(CreateUserViewModel model);
        /// <summary>
        /// 取得編輯帳號資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserViewModel> GetEditUserItem(string Id);
        /// <summary>
        /// 編輯帳號資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> EditUserItem(UserViewModel model);
        /// <summary>
        /// 刪除帳號資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteUserItem(string ur_id);
    }
}
