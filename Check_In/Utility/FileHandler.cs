using System.IO;

namespace Check_In.Utility
{
    public static class FileHandler
    {
        /// <summary>
        /// 刪除指定路徑的檔案，找不到則停止
        /// </summary>
        /// <param name="path">路徑</param>
        /// <param name="filename">檔案名稱</param>
        public static void DeleteFileIfDefind(string path, string filename)
        {
            if (string.IsNullOrEmpty(path)) { return; }
            if (string.IsNullOrEmpty(filename)) { return; }
            var fullPath = Path.Combine(path, filename);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}