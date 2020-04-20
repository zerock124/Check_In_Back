using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Excel
{
	/// <summary>
	/// 成功訊息 ViewModel
	/// </summary>
	public class SuccessMessageViewModel
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
		/// 成功訊息
		/// </summary>
		public string SuccessMessage { get; set; }
	}
}
