using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Verity;

namespace Check_In.Utility
{
    public static class PathUtils
    {
        /// <summary>
        /// Secure path combining<br/>
        /// Throw exception if any part contains ".." or other invalid value<br/>
        /// 安全的合併路徑
        /// 如果路徑中有..或其他不合法的值 則拋出例外
        /// </summary>
        /// <param name="paths">Path parts</param>
        /// <returns></returns>
        /// <example>
        /// <code language="cs">
        /// PathUtils.SecureCombine("a", "b", "c") == Path.Combine("a", "b", "c")
        /// PathUtils.SecureCombine("a", "/b", "c") throws exception
        /// PathUtils.SecureCombine("a", "\\b", "c") throws exception
        /// PathUtils.SecureCombine("a", "", "c") throws exception
        /// PathUtils.SecureCombine("a", "..", "c") throws exception
        /// PathUtils.SecureCombine("a/../b", "c") throws exception
        /// </code>
        /// </example>
        public static string SecureCombine(params string[] paths)
        {
            for (var i = 0; i < paths.Length; ++i)
            {
                var path = paths[i];
                if (i > 0 && path.StartsWith("/"))
                {
                    throw new ArgumentException($"path startswith '/'");
                }
                else if (path.StartsWith("\\"))
                {
                    throw new ArgumentException($"path startswith '\'");
                }
                else if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException($"path {path} is null or empty");
                }
                else if (path.Contains(".."))
                {
                    throw new ArgumentException($"path {path} contains '..'");
                }
            }
            return Path.Combine(paths);
        }

        /// <summary>
        /// Ensure parent directories are exist<br/>
        /// 確保路徑的上層資料夾存在
        /// </summary>
        /// <param name="path">Path</param>
        /// <example>
        /// <code language="cs">
        /// PathUtils.EnsureParentDirectory("c:\abc\123.txt");
        /// // will create c:\abc if not exist
        /// </code>
        /// </example>
        public static void EnsureParentDirectory(string path)
        {
            var parentDirectory = Path.GetDirectoryName(path);
            if (!Directory.Exists(parentDirectory))
            {
                Directory.CreateDirectory(parentDirectory);
            }
        }

        /// <summary>
        /// 建立指定路徑上所缺少的資料夾
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static VerityResult CreateFolderIfNeeded(string path)
        {
            VerityResult result = new VerityResult();
            if (!Directory.Exists(path))
            {
                try {
                    Directory.CreateDirectory(path);
                    result.Success = true;
                }
                catch (Exception) {
                    result.Success = false;
                    result.Message = "新增指定路徑資料夾失敗";
                }
            }
            return result;
        }
    }
}
