using Check_InDB.Interface;
using Check_InDB.Models;
using Check_InDB.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using ViewModels.User;
using ViewModels.Verity;

namespace Check_InDB.Service
{
    public class UserService : IUserService
    {
        Check_testEntities _db;
        protected IGenericRepository<user> _user;
        protected IGenericRepository<AspNetRoles> _aspnetRole;
        protected IGenericRepository<AspNetUserRoles> _aspnetUserRole;
        PasswordHasher hasher;

        public UserService()
        {
            _db = new Check_testEntities();
            _user = new GenericRepository<user>();
            _aspnetRole = new GenericRepository<AspNetRoles>();
            _aspnetUserRole = new GenericRepository<AspNetUserRoles>();

            hasher = new PasswordHasher();
        }

        public async Task<ResWithPaginationViewModel> GetUserList(UserSearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<UserViewModel> userlist = new List<UserViewModel>();

            var query = from a in _db.user
                        select new UserViewModel
                        {
                            ur_sn = a.ur_sn,
                            ur_id = a.ur_id,
                            ur_ac = a.ur_ac,
                            ur_pw = a.ur_pw,
                            ur_ct = a.ur_ct,
                            ur_is = a.ur_is,
                            ur_im = a.ur_im
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.ur_ct).FirstOrDefault().ur_ct;
            pageData.MinDateTime = query.OrderBy(x => x.ur_ct).FirstOrDefault().ur_ct;

            if (searchModel.StartDateTime != null)
            {
                query = query.Where(x => x.ur_ct >= searchModel.StartDateTime);
            }
            if (searchModel.EndDateTime != null)
            {
                query = query.Where(x => x.ur_ct <= searchModel.EndDateTime);
            }
            if (!string.IsNullOrEmpty(searchModel.Query))
            {
                switch (searchModel.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.ur_ac.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    default:
                        break;
                }
            }


            var _TotalCount = query.Count();
            pageData.Pagination = new PaginationViewModel
            {
                PerPage = pagination.PerPage,
                CurrentPage = pagination.CurrentPage,
                TotalCounts = _TotalCount
            };

            query = query
                .OrderByDescending(x => x.ur_ct)
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any())
            {
                var list = query.OrderByDescending(x => x.ur_ct).ToList();
                userlist = list;
            }

            pageData.Data = userlist;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }

        public async Task<VerityResult> CreateUser(CreateUserViewModel model)
        {
            VerityResult result = new VerityResult();

            user AddUser = new user()
            {
                ur_id = Guid.NewGuid().ToString(),
                ur_ac = model.ur_ac,
                ur_pw = MD5Str.MD5(model.ur_pw),
                ur_ct = DateTime.Now,
                ur_is = true,
                ur_im = model.ur_im
            };
            try
            {
                _user.Create(AddUser);
                result.Success = true;
                result.Message = "新增帳號成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增帳號失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<UserViewModel> GetEditUserItem(string Id)
        {
            UserViewModel item = new UserViewModel();

            var query = from a in _db.user.Where(x => x.ur_id == Id)
                        select new UserViewModel
                        {
                            ur_sn = a.ur_sn,
                            ur_id = a.ur_id,
                            ur_ac = a.ur_ac,
                            ur_pw = a.ur_pw,
                            ur_ct = a.ur_ct,
                            ur_is = a.ur_is,
                            ur_im = a.ur_im
                        };

            if (query.Any())
            {
                var list = query.FirstOrDefault();
                item = list;
            }
            return await Task.Run(() => item);

        }

        public async Task<VerityResult> EditUserItem(UserViewModel model)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = _user.FindBy(x => x.ur_id == model.ur_id);

                if (query.Any())
                {
                    var list = query.FirstOrDefault();
                    user item = list;
                    item.ur_ac = model.ur_ac;
                    string Md5Str = MD5Str.MD5(model.ur_pw);
                    item.ur_pw = Md5Str;
                    if (!string.IsNullOrEmpty(model.ur_im))
                    {
                        item.ur_im = model.ur_im;
                    }
                    _user.Update(item);
                }

                result.Success = true;
                result.Message = "編輯帳號資料成功";
            }

            catch
            {
                result.Success = false;
                result.Message = "編輯帳號資料失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<VerityResult> DeleteUserItem(string ur_id)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = _user.FindBy(x => x.ur_id == ur_id);
                if (query.Any())
                {
                    user item = query.FirstOrDefault();
                    _user.Delete(item);

                    result.Success = true;
                    result.Message = "刪除最新消息成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "刪除最新消息失敗";
            }

            return await Task.Run(() => result);
        }

    }
}
