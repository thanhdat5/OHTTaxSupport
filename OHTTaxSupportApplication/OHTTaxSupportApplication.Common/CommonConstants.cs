using OHTTaxSupportApplication.Model.ViewModels;
using System;

namespace OHTTaxSupportApplication.Common
{
    public class CommonConstants
    {
        public const string ProductTag = "product";
        public const string PostTag = "post";
        public const string DefaultFooterId = "default";

        public const string SessionCart = "SessionCart";

        public const string HomeTitle = "HomeTitle";
        public const string HomeMetaKeyword = "HomeMetaKeyword";
        public const string HomeMetaDescription = "HomeMetaDescription";

        public const string Administrator = "Administrator";

        // Error code
        public const int ApiResponseSuccessCode = 0;
        public const int ApiResponseExceptionCode = 1;
        public const int ApiResponseNotFoundCode = 2;


        // Message
        public const string DeleteSuccess = "The record was removed successfully.";
        public const string SaveSuccess = "The record was saved successfully.";
        public const string RemoveSuccess = "The record was removed successfully.";
        public const string CopySuccess = "The record was copied successfully.";
        public const string AddSuccess = "The record was added successfully.";
        public const string AlreadyExists = " already exists.";

        public const string ErrorMessage = "Some error occurred.";
        public const string NotFoundMessage = "Data not found.";


        public static ApiResponseViewModel accessDenied = new ApiResponseViewModel
        {
            Code = 999,
            Message = "Access Denied.",
            Result = null
        };
    }
}
