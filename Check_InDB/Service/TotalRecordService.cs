using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using Check_InDB.Models;
using Check_InDB.Repositories;
using Check_InDB.Interface;
using ViewModels.TotalRecord;
using ViewModels.Verity;

namespace Check_InDB.Service
{
    public class TotalRecordService : ITotalRecordService
    {
        protected Check_testEntities _db;
        protected IGenericRepository<user> _user;
        protected IGenericRepository<check_all> _check_all;
        protected IGenericRepository<check_in> _check_in;

        public TotalRecordService()
        {
            _db = new Check_testEntities();
            _user = new GenericRepository<user>(_db);
            _check_all = new GenericRepository<check_all>(_db);
            _check_in = new GenericRepository<check_in>(_db);
        }
        /// <summary>
        /// 取得彙總紀錄列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<ResponseViewModel> GetTotalRecordList(SearchModel searchModel)
        {
            ResponseViewModel res = new ResWithPaginationViewModel();
            List<Check_AllViewModel> check_All = new List<Check_AllViewModel>();
            try
            {
                var year = searchModel.StartDateTime.Year;
                var month = searchModel.StartDateTime.Month;

                var query = from a in _check_all.GetAll()
                            join b in _user.GetAll() on a.ur_id equals b.ur_id into user
                            from b in user.DefaultIfEmpty()
                            where a.ca_ya == year && a.ca_mo == month && !string.IsNullOrEmpty(b.ur_id)
                            select new Check_AllViewModel
                            {
                                ca_sn = a.ca_sn,
                                ur_id = a.ur_id,
                                ca_ya = a.ca_ya,
                                ca_mo = a.ca_mo,
                                ca_dy = a.ca_dy,
                                ca_on = a.ca_on,
                                ca_hr = a.ca_hr,
                                ur_ac = b.ur_ac,
                                ca_hr_avg = Math.Round((a.ca_hr * 1.00) / (a.ca_on * 1.00), 1)
                            };

                if (query.Any())
                {

                    if (!string.IsNullOrEmpty(searchModel.Query))
                    {
                        var QueryString = searchModel.Query.Trim().ToLower();
                        query = query.Where(x => x.ur_ac.Trim().ToLower().Contains(QueryString));
                    }

                    check_All = query.ToList();
                    res.Data = check_All;
                    res.Success = true;
                    res.Message = "取得彙總紀錄成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "取得彙總紀錄失敗";
            }

            return await Task.Run(() => res);
        }
        /// <summary>
        /// 更新彙整記錄
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<VerityResult> CheckTotalRecord(SearchTime searchModel)
        {
            VerityResult res = new VerityResult();
            List<check_all> check_All = new List<check_all>();
            try
            {
                DateTime nowDate = DateTime.Now;

                var year = searchModel.StartDateTime.Year;
                var month = searchModel.StartDateTime.Month;
                var days = 0;
                if (nowDate.Year == year && nowDate.Month == month)
                {
                    days = nowDate.Day;
                }
                else
                {
                    days = DateTime.DaysInMonth(year, month);
                }
                int weekDays = 0;

                for (int i = 0; i < days; i++)
                {
                    // 判断是否為周六、周日
                    switch (searchModel.StartDateTime.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            weekDays++;
                            break;
                        case DayOfWeek.Sunday:
                            weekDays++;
                            break;
                    }
                    searchModel.StartDateTime = searchModel.StartDateTime.AddDays(1);
                }
                // 工作日
                int workDays = days - weekDays;

                var checkIn = from a in _check_in.GetAll()
                              join c in _user.GetAll() on a.ur_id equals c.ur_id
                              where a.ca_mo == month && a.ci_ut != TimeSpan.Zero && a.ci_dt != TimeSpan.Zero
                              group a by new
                              {
                                  a.ur_id,
                                  a.ca_mo,
                                  ca_ya = a.ci_da.Year,
                              } into ga
                              select new Check_AllViewModel
                              {
                                  ur_id = ga.Key.ur_id,
                                  ca_ya = ga.Key.ca_ya,
                                  ca_mo = ga.Key.ca_mo,
                                  ca_dy = workDays,
                                  ca_on = ga.Count(),
                                  ca_hr = ga.Sum(x => x.ci_hr)
                              };

                if (checkIn.Any())
                {
                    foreach (var item in checkIn.ToList())
                    {
                        check_all check_All_Item = new check_all();

                        var Find_check_all = _check_all.FindBy(x => x.ur_id == item.ur_id && x.ca_mo == item.ca_mo).ToList();

                        if (Find_check_all.Any())
                        {
                            check_All_Item = Find_check_all.FirstOrDefault();
                            check_All_Item.ca_dy = item.ca_dy;
                            check_All_Item.ca_on = item.ca_on;
                            check_All_Item.ca_hr = item.ca_hr;
                            check_All.Add(check_All_Item);

                            _check_all.Update(check_All_Item);
                        }
                        else
                        {
                            check_All_Item.ur_id = item.ur_id;
                            check_All_Item.ca_ya = item.ca_ya;
                            check_All_Item.ca_mo = item.ca_mo;
                            check_All_Item.ca_dy = item.ca_dy;
                            check_All_Item.ca_on = item.ca_on;
                            check_All_Item.ca_hr = item.ca_hr;

                            check_All.Add(check_All_Item);

                            _check_all.Create(check_All_Item);
                        }
                    }
                    res.Success = true;
                    res.Message = "更新打卡紀錄成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "更新打卡紀錄失敗";
            }

            return await Task.Run(() => res);
        }
        /// <summary>
        /// 取得日期選項
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseViewModel> GetOptions()
        {
            ResponseViewModel res = new ResponseViewModel();
            OptionsViewModel opts = new OptionsViewModel();
            try
            {
                var yearOpts = (from a in _check_all.GetAll()
                                group a by a.ca_ya into ay
                                select new Options
                                {
                                    value = ay.Key,
                                    text = ay.Key.ToString(),
                                }).ToList();

                var monthOpts = (from a in _check_all.GetAll()
                                 group a by a.ca_mo into ay
                                 select new Options
                                 {
                                     value = ay.Key,
                                     text = ay.Key.ToString(),
                                 }).ToList();

                if (yearOpts.Any())
                {
                    opts.YearOptions = yearOpts.ToArray();
                }
                else
                {
                    List<Options> yearoptS = new List<Options>();

                    Options years = new Options
                    {
                        value = DateTime.Now.Year,
                        text = DateTime.Now.Year.ToString(),
                    };

                    yearoptS.Add(years);
                    opts.YearOptions = yearoptS.ToArray();
                }

                if (monthOpts.Any())
                {
                    opts.MonthOptions = monthOpts.ToArray();
                }
                else
                {
                    List<Options> monthoptS = new List<Options>();

                    Options months = new Options
                    {
                        value = DateTime.Now.Month,
                        text = DateTime.Now.Month.ToString(),
                    };

                    monthoptS.Add(months);
                    opts.MonthOptions = monthoptS.ToArray();
                }

                res.Data = opts;
                res.Success = true;
                res.Message = "取得下拉列表成功";
            }
            catch
            {
                res.Success = false;
                res.Message = "取得下拉列表失敗";
            }
            return await Task.Run(() => res);
        }

        //public async Task<ResponseViewModel> GetCheck_All_Item(string ur_id)
        //{
        //    ResponseViewModel res = new ResponseViewModel();
        //    Check_AllViewModel item = new Check_AllViewModel();

        //    try
        //    {
        //        var query = from a in _check_all.FindBy(x => x.ur_id == ur_id)
        //                    join b in _user.GetAll() on a.ur_id equals b.ur_id
        //                    select new Check_AllViewModel
        //                    {
        //                        ca_sn = a.ca_sn,
        //                        ur_id = a.ur_id,
        //                        ca_ya = a.ca_ya,
        //                        ca_mo = a.ca_mo,
        //                        ca_on = a.ca_on,
        //                        ca_of = a.ca_of,
        //                        ca_la = a.ca_la,
        //                        ca_ad = a.ca_ad,
        //                        ca_ev = a.ca_ev,
        //                        ca_sk = a.ca_sk,
        //                        ca_dy = a.ca_dy,
        //                        ur_ac = b.ur_ac
        //                    };
        //        if (query.Any())
        //        {
        //            res.Data = query.FirstOrDefault();
        //            res.Success = true;
        //            res.Message = "取得下拉列表成功";
        //        };
        //    }
        //    catch
        //    {
        //        res.Success = false;
        //        res.Message = "取得下拉列表失敗";
        //    }
        //    return await Task.Run(() => res);
        //}

        //public async Task<VerityResult> SubmitCheck_All(Check_AllViewModel model) 
        //{
        //    VerityResult res = new VerityResult();
        //    check_all item = new check_all();

        //    try
        //    {
        //        var query = _check_all.FindBy(x => x.ca_sn == model.ca_sn);

        //        if (query.Any())
        //        {
        //            item = query.FirstOrDefault();
        //            item.ca_ev = model.ca_ev;
        //            item.ca_sk = model.ca_sk;
        //            item.ca_of = item.ca_ev + item.ca_sk;
        //            _check_all.Update(item);
        //            res.Success = true;
        //            res.Message = "變更打卡資料成功";
        //        }
        //    }
        //    catch
        //    {
        //        res.Success = false;
        //        res.Message = "變更打卡資料失敗";
        //    }

        //    return await Task.Run(() => res);
        //}
    }
}
