using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORETest.Utilities
{
	public class OperationResault
	{
            public string Message { get; set; }
            public OperationResultStatus Status { get; set; }

            #region Errors
            public static OperationResault Error()
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.Error,
                    Message = "عملیات ناموفق",
                };
            }
            public static OperationResault Error(string message)
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.Error,
                    Message = message,
                };
            }
            #endregion

            #region NotFound

            public static OperationResault NotFound(string message)
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.NotFound,
                    Message = message,
                };
            }
            public static OperationResault NotFound()
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.NotFound,
                    Message = "اطلاعات درخواستی یافت نشد",
                };
            }

            #endregion

            #region Succsess

            public static OperationResault Success()
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.Success,
                    Message = "عملیات با موفقیت انجام شد",
                };
            }
            public static OperationResault Success(string message)
            {
                return new OperationResault()
                {
                    Status = OperationResultStatus.Success,
                    Message = message,
                };
            }
            #endregion
    }
		public enum OperationResultStatus
        {
            Error = 10,
            Success = 200,
            NotFound = 404
        }
}
