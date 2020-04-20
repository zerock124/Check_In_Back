using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Excel
{
	/// <summary>
	/// 匯入結果
	/// </summary>
	public class ImportResult
	{
		private Boolean _result = true;
		public Boolean Result
		{
			get { return _result; }
			set { _result = value; }
		}

		private byte[] _fileData { get; set; }
		public byte[] FileData
		{
			get { return _fileData; }
			set { _fileData = value; }
		}

		/// <summary>
		/// 成功清單
		/// </summary>
		private List<SuccessMessageViewModel> _successList { get; set; }
		public List<SuccessMessageViewModel> SuccessList
		{
			get
			{
				if (_successList == null)
				{
					_successList = new List<SuccessMessageViewModel>();
				}
				return _successList;
			}
			set { _successList = value; }
		}

		/// <summary>
		/// 將成功訊息加進成功清單
		/// </summary>
		/// <param name="ErrorMessage"></param>
		/// <param name="changeResult"></param>
		public void AddSuccess(SuccessMessageViewModel SuccessMessage)
		{
			if (_successList != null)
			{
				if (!_successList.Contains(SuccessMessage))
					_successList.Add(SuccessMessage);
			}
			else
			{
				_successList = new List<SuccessMessageViewModel>();
				_successList.Add(SuccessMessage);
			}
		}

		/// <summary>
		/// 錯誤清單
		/// </summary>
		private List<ErrorMessageViewModel> _errorList { get; set; }
		public List<ErrorMessageViewModel> ErrorList
		{
			get
			{
				if (_errorList == null)
				{
					_errorList = new List<ErrorMessageViewModel>();
				}
				return _errorList;
			}
			set { _errorList = value; }
		}

		/// <summary>
		/// 將錯誤訊息加進錯誤清單
		/// </summary>
		/// <param name="ErrorMessage"></param>
		/// <param name="changeResult"></param>
		public void AddError(ErrorMessageViewModel ErrorMessage)
		{
			if (_errorList != null)
			{
				if (!_errorList.Contains(ErrorMessage))
					_errorList.Add(ErrorMessage);
			}
			else
			{
				_errorList = new List<ErrorMessageViewModel>();
				_errorList.Add(ErrorMessage);
			}
		}
	}
}
