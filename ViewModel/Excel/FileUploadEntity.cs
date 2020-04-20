using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Excel
{
	/// <summary>
	/// 檔案上傳Entity
	/// </summary>
	public class FileUploadEntity
	{
		private bool _returnResult = false;

		public FileUploadEntity() { }

		public FileUploadEntity(byte[] data, string fileName, Stream fileStream)
		{
			FileData = data;
			FileName = fileName;
			FileStram = fileStream;
		}

		/// <summary>
		/// 檔案名稱
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 檔案資料內byte
		/// </summary>
		public byte[] FileData { get; set; }

		/// <summary>
		/// 檔案位元流
		/// </summary>
		public Stream FileStram { get; set; }

		public bool ReturnResult
		{
			get
			{
				return _returnResult;
			}
			set
			{
				_returnResult = value;
			}
		}
	}

}
