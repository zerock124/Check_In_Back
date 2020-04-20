using Check_InDB.Interface;
using Check_InDB.Models;
using Check_InDB.Repositories;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.CheckRecord;
using ViewModels.Share;
using ViewModels.User;
using ViewModels.Verity;

namespace Check_InDB.Service
{
    public class CheckRecordService : ICheckRecordService
    {
        protected Check_testEntities _db;
        protected IGenericRepository<user> _user;
        protected IGenericRepository<check_in> _check_in;

        public CheckRecordService()
        {
            _db = new Check_testEntities();
            _user = new GenericRepository<user>(_db);
            _check_in = new GenericRepository<check_in>(_db);
        }

        public async Task<ResWithPaginationViewModel> GetCheckRecord(SearchCheckModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<CheckRecordViewModel> check_inList = new List<CheckRecordViewModel>();

            try
            {
                var query = from a in _check_in.GetAll()
                            join b in _user.GetAll() on a.ur_id equals b.ur_id
                            where a.ci_da >= model.StartDateTime && a.ci_da <= model.EndDateTime
                            select new CheckRecordViewModel
                            {
                                ci_sn = a.ci_sn,
                                ur_id = a.ur_id,
                                ci_da = a.ci_da,
                                ci_ut = a.ci_ut,
                                ci_dt = a.ci_dt,
                                ur_ac = b.ur_ac,
                                ci_ct = a.ci_ct,
                                Remark = a.Remark,
                                ci_hr = a.ci_hr
                            };

                if (!string.IsNullOrEmpty(model.Query))
                {
                    var QueryString = model.Query.ToLower().Trim();
                    query = query.Where(x => x.ur_ac.ToLower().Trim().Contains(QueryString));
                }

                if (!string.IsNullOrEmpty(model.ur_id))
                {
                    query = query.Where(x => x.ur_id == model.ur_id);
                }

                var _TotalCount = query.Count();
                pageData.Pagination = new PaginationViewModel
                {
                    PerPage = pagination.PerPage,
                    CurrentPage = pagination.CurrentPage,
                    TotalCounts = _TotalCount
                };

                query = query
                    .OrderByDescending(x => x.ci_da)
                    .Skip(pagination.GetSkipLength())
                    .Take(pagination.PerPage);

                if (query.Any())
                {
                    check_inList = query.OrderByDescending(x => x.ci_da).ToList();
                    pageData.Data = check_inList;
                    pageData.Success = true;
                    pageData.Message = "取得打卡紀錄成功";
                }
                else
                {
                    pageData.Success = true;
                    pageData.Message = "尚未有任何打卡紀錄";
                }
            }
            catch
            {
                pageData.Success = false;
                pageData.Message = "取得打卡紀錄失敗";
            }
            return await Task.Run(() => pageData);
        }

        public async Task<ResponseViewModel> GetUserList()
        {
            ResponseViewModel res = new ResponseViewModel();
            List<UserViewModel> userlist = new List<UserViewModel>();

            try
            {
                var query = from a in _user.GetAll()
                            select new UserViewModel
                            {
                                ur_sn = a.ur_sn,
                                ur_id = a.ur_id,
                                ur_ac = a.ur_ac,
                                ur_im = a.ur_im,
                                ur_is = a.ur_is,
                                ur_ct = a.ur_ct,
                            };

                if (query.Any())
                {
                    userlist = query.ToList();
                    res.Data = userlist;
                    res.Success = true;
                    res.Message = "取得帳號資料成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "取得帳號資料失敗";
            }

            return await Task.Run(() => res);
        }

        public async Task<ResponseViewModel> GetClick_In(int ci_sn)
        {
            ResponseViewModel res = new ResponseViewModel();
            CheckRecordViewModel click_In_Item = new CheckRecordViewModel();

            try
            {
                var query = from a in _check_in.FindBy(x => x.ci_sn == ci_sn)
                            join b in _user.GetAll() on a.ur_id equals b.ur_id
                            select new CheckRecordViewModel
                            {
                                ci_sn = a.ci_sn,
                                ur_id = a.ur_id,
                                ci_da = a.ci_da,
                                ci_ut = a.ci_ut,
                                ci_dt = a.ci_dt,
                                ur_ac = b.ur_ac,
                                ci_ct = a.ci_ct,
                                Remark = a.Remark
                            };

                if (query.Any())
                {
                    click_In_Item = query.FirstOrDefault();
                    res.Data = click_In_Item;
                    res.Success = true;
                    res.Message = "取得打卡資料成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "取得打卡資料失敗";
            }

            return await Task.Run(() => res);
        }

        public async Task<VerityResult> SubmitClick_In(CheckRecordViewModel model)
        {
            VerityResult res = new VerityResult();
            try
            {
                check_in item = new check_in();

                var query = _check_in.FindBy(x => x.ci_sn == model.ci_sn);

                if (query.Any())
                {
                    item = query.FirstOrDefault();
                    item.ci_ut = model.ci_ut;
                    item.ci_dt = model.ci_dt;

                    var nowDate = item.ci_da.ToString("yyyy-MM-dd");

                    if (item.ci_ut != TimeSpan.Zero && item.ci_dt != TimeSpan.Zero)
                    {
                        TimeSpan dt1 = item.ci_ut;
                        TimeSpan dt2 = item.ci_dt;
                        TimeSpan ts = dt2.Subtract(dt1);

                        double hours = Math.Round(ts.TotalHours, 1) - 1;
                        if (hours > 0)
                        {
                            item.ci_hr = hours;
                        }
                    };

                    item.Remark = "變更打卡時間";
                    _check_in.Update(item);
                    res.Success = true;
                    res.Message = "變更打卡資料成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "變更打卡資料失敗";
            }

            return await Task.Run(() => res);
        }
        /// <summary>
        /// 取得匯出Excel資料
        /// </summary>
        /// <param name="SearchTime"></param>
        /// <returns></returns>
        public async Task<ResponseViewModel> GetCheckRecordExcel(SearchTimeModel SearchTime)
        {
            ResponseViewModel res = new ResponseViewModel();
            List<CheckRecordExportViewModel> list = new List<CheckRecordExportViewModel>();

            try
            {
                var query = from a in _user.GetAll()
                            select new CheckRecordExportViewModel
                            {
                                ur_id = a.ur_id,
                                ur_ac = a.ur_ac,
                                checkRecordTestViewModel = (from b in _db.check_in
                                                            join c in _db.user on b.ur_id equals c.ur_id
                                                            where b.ur_id == a.ur_id && b.ci_da >= SearchTime.StartDateTime && b.ci_da <= SearchTime.EndDateTime
                                                            select new CheckRecordViewModel
                                                            {
                                                                ci_sn = b.ci_sn,
                                                                ur_id = b.ur_id,
                                                                ci_da = b.ci_da,
                                                                ci_ut = b.ci_ut,
                                                                ci_dt = b.ci_dt,
                                                                ci_ct = b.ci_ct,
                                                                Remark = b.Remark,
                                                                ur_ac = c.ur_ac
                                                            }).OrderBy(x => x.ci_da).ToList()
                            };

                if (query.Any())
                {
                    list = query.ToList();
                    res.Data = list;
                    res.Success = true;
                    res.Message = "取得匯出資料成功";
                }
            }
            catch
            {
                res.Success = false;
                res.Message = "取得匯出資料失敗";
            }

            return await Task.Run(() => res);
        }

        /// <summary>
        /// check_in匯出Excel
        /// </summary>
        /// <param name="xlsList"></param>
        /// <param name="wk"></param>
        /// <param name="sht"></param>
        /// <param name="Title"></param>
        public void ExportExcel(List<CheckRecordViewModel> xlsList, XSSFWorkbook wk, ISheet sht, string Title)
        {
            XSSFCellStyle cellStyle = (XSSFCellStyle)wk.CreateCellStyle();
            var TopRow = sht.CreateRow(0);
            cellStyle = CreateCellData(wk, cellStyle, HorizontalAlignment.Center, VerticalAlignment.Center, 12, FontBoldWeight.Bold);
            byte[] colorRgb = { (byte)255, (byte)232, (byte)42 };

            var myColor = new XSSFColor(colorRgb);

            cellStyle.SetFillForegroundColor(myColor);
            cellStyle.FillPattern = FillPattern.SolidForeground;

            cellStyle.BorderTop = BorderStyle.Medium;
            cellStyle.BorderLeft = BorderStyle.Medium;
            cellStyle.BorderRight = BorderStyle.Medium;
            cellStyle.BorderBottom = BorderStyle.Medium;

            SetCellData(TopRow, 0, Title, cellStyle);
            SetCellData(TopRow, 1, " ", cellStyle);
            SetCellData(TopRow, 2, " ", cellStyle);
            SetCellData(TopRow, 3, " ", cellStyle);
            SetCellData(TopRow, 4, " ", cellStyle);

            //---------------------------------------------------------------------- 
            var headerRow = sht.CreateRow(1);
            XSSFCellStyle cell3C = (XSSFCellStyle)wk.CreateCellStyle();
            cell3C = CreateCellData(wk, cell3C, HorizontalAlignment.Center, VerticalAlignment.Center, 12, FontBoldWeight.Bold);

            cell3C.SetFillForegroundColor(myColor);
            cell3C.FillPattern = FillPattern.SolidForeground;

            cell3C.BorderTop = BorderStyle.Medium;
            cell3C.BorderLeft = BorderStyle.Medium;
            cell3C.BorderRight = BorderStyle.Medium;
            cell3C.BorderBottom = BorderStyle.Medium;

            SetCellData(headerRow, 0, "日期", cell3C);
            SetCellData(headerRow, 1, "星期", cell3C);
            SetCellData(headerRow, 2, "上班打卡", cell3C);
            SetCellData(headerRow, 3, "下班打卡", cell3C);
            SetCellData(headerRow, 4, "備註", cell3C);

            //---------------------------------------------------------------------- 
            //---------------------------------------------------------------------- (Optional) set the width of the columns
            sht.SetColumnWidth(0, 24 * 256);
            sht.SetColumnWidth(1, 24 * 256);
            sht.SetColumnWidth(2, 24 * 256);
            sht.SetColumnWidth(3, 24 * 256);
            sht.SetColumnWidth(4, 24 * 256);

            //---------------------------------------------------------------------- Set the column names in the header row

            var rowNumber = 2;
            foreach (var exp in xlsList)
            {
                XSSFCellStyle cellStyle22 = (XSSFCellStyle)wk.CreateCellStyle();
                cellStyle22 = CreateCellData(wk, cellStyle22, HorizontalAlignment.Center, VerticalAlignment.Center, 10, FontBoldWeight.Bold);

                cellStyle22.BorderTop = BorderStyle.Medium;
                cellStyle22.BorderLeft = BorderStyle.Medium;
                cellStyle22.BorderRight = BorderStyle.Medium;
                cellStyle22.BorderBottom = BorderStyle.Medium;

                int R = 0;
                int G = 0;
                int B = 0;

                switch (exp.ci_da.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        R = 240;
                        G = 121;
                        B = 159;
                        break;
                    case DayOfWeek.Monday:
                        R = 255;
                        G = 255;
                        B = 255;
                        break;
                    case DayOfWeek.Tuesday:
                        R = 255;
                        G = 255;
                        B = 255;
                        break;
                    case DayOfWeek.Wednesday:
                        R = 255;
                        G = 255;
                        B = 255;
                        break;
                    case DayOfWeek.Thursday:
                        R = 255;
                        G = 255;
                        B = 255;
                        break;
                    case DayOfWeek.Friday:
                        R = 255;
                        G = 255;
                        B = 255;
                        break;
                    case DayOfWeek.Saturday:
                        R = 240;
                        G = 121;
                        B = 159;
                        break;
                    default:
                        break;
                }

                byte[] colorcell = { (byte)R, (byte)G, (byte)B };

                var colorData = new XSSFColor(colorcell);

                cellStyle22.SetFillForegroundColor(colorData);
                cellStyle22.FillPattern = FillPattern.SolidForeground;

                //--------------------------------
                var row = sht.CreateRow(rowNumber++);
                SetCellData(row, 0, exp.ci_da.ToString("yyyy/MM/dd"), cellStyle22);
                SetCellData(row, 1, exp.ci_da.ToString("ddd"), cellStyle22);
                if (exp.ci_ut.Ticks != 0)
                {
                    SetCellData(row, 2, exp.ci_ut.ToString(), cellStyle22);
                }
                else
                {
                    SetCellData(row, 2, "-", cellStyle22);
                }
                if (exp.ci_dt.Ticks != 0)
                {
                    SetCellData(row, 3, exp.ci_dt.ToString(), cellStyle22);
                }
                else
                {
                    SetCellData(row, 3, "-", cellStyle22);
                }
                SetCellData(row, 4, exp.Remark, cellStyle22);
            }
        }

        /// <summary>
        ///Excel儲存格資料內容格式 右對齊
        /// </summary>
        /// <param name="wb">Excel工作表</param>
        /// <param name="cs">格式</param>
        ////150108 共用Excel CreateCellStyle
        public static XSSFCellStyle CreateCellData(XSSFWorkbook wb, XSSFCellStyle cs, HorizontalAlignment ha, VerticalAlignment va, short fontSize, FontBoldWeight fontBold)
        {
            //set Font
            XSSFFont font = (XSSFFont)wb.CreateFont();
            font.FontHeightInPoints = fontSize;         //fontSize
            font.FontName = "Courier New";       //ftName;
            font.Boldweight = (short)fontBold;          //(short)FontBoldWeight.NORMAL; //(short)ftBW;
            cs.SetFont(font);

            //set Cell Style
            cs.Alignment = ha;
            cs.VerticalAlignment = va;

            return cs;
        }
        /// <summary>
        /// 設定Cell內容與格式
        /// </summary>
        public static void SetCellData(IRow row, short column, string cellText, XSSFCellStyle cs)
        {
            var cell = row.CreateCell(column);              //Create a new cell
            cell.SetCellValue(cellText);
            cell.CellStyle = cs;
        }
        /// <summary>
        /// 設定Cell內容與格式(指定儲存格格式)
        /// </summary>
        public static void SetCellData(IRow row, short column, string cellText, XSSFCellStyle cs, CellType ctype)
        {
            var cell = row.CreateCell(column);      //Create a new cell
            //cell.SetCellType(CellType.STRING);
            cell.SetCellType(ctype);                //設定儲存格型別
            if (ctype == CellType.Numeric)
                cell.SetCellValue(Convert.ToDouble(cellText));
            else
                cell.SetCellValue(cellText);

            cell.CellStyle = cs;
        }
    }
}
