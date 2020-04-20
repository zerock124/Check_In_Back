using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using ViewModels.Verity;
using Check_In.Extensions;

namespace Check_In.Utility
{
    public static class ImageHandler
    {
        public static SaveFileVerityResult SaveFileToPath(HttpPostedFileBase file, string path, string filename )
        {
            SaveFileVerityResult result = new SaveFileVerityResult();
            /** 建立路徑缺少的資料夾 */
            var checkFolder = PathUtils.CreateFolderIfNeeded(path);
            if (!checkFolder.Success)
            {
                result.Success = false;
                result.Message = checkFolder.Message;
                result.FileName = null;
                return result;
            }

            /** 檢查是否提供指定的檔案名稱 */
            if (string.IsNullOrWhiteSpace(filename))
            {
                string mimeType = MimeMapping.GetMimeMapping(file.FileName);
                filename = Guid.NewGuid().ToString("N") + MimeTypeMapUtils.GetExtension(mimeType);
            }

            /** 壓縮與儲存圖片 */
            var fullFilePath = Path.Combine(path, filename);
            //HttpPostedFileBase fileBase = new ImageHttpPostedFileBase(
            //    file, new Size(300, 300), 90, ImageResizeMode.ByWidth);
            //fileBase.SaveAs(fullFilePath);
            file.SaveAs(fullFilePath);
            result.Success = true;
            result.Message += "儲存與壓縮圖片成功";
            result.FileName = filename;
            return result;
        }
    }
}
