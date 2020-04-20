using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Excel
{
	/// <summary>
	/// 錯誤訊息ViewModel
	/// </summary>
	public class ErrorMessageViewModel
	{
		/// <summary>
		/// 列數字
		/// </summary>
		public int RowNumber { get; set; }

		/// <summary>
		/// 值
		/// </summary>
		public string ColumnValue { get; set; }

		/// <summary>
		/// 錯誤訊息
		/// </summary>
		public string ErrorMessage { get; set; }
	}
}
