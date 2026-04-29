using System.ComponentModel.DataAnnotations;
using EmployeeAPI_MVC.Models;

namespace EmployeeAPI_MVC.ValidationAttributes
{
    public class 判斷是否有重複的課程名稱Attribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // 如果是空值，驗證成功
            // 沒有填就不用驗， Required已驗證

            string? title = value as string;
            if (string.IsNullOrEmpty(title))
            {
                return ValidationResult.Success;
            }

            // 抓資料庫資料

            var db = (ContosoUniversityContext)validationContext.GetService(typeof(ContosoUniversityContext))!;


            // 如果有填，進資料庫判斷是否有相同標題，存在則 true

            bool exists = db!.Courses.Any(c => c.Title == title);

            // 不存在代表成功 => 希望 title 沒有重複

            if (!exists)
            {
                return ValidationResult.Success;
            }
            else
            {
                var fieldName = validationContext.MemberName ?? "";
                return new ValidationResult("課程名稱已重複", new string[] { fieldName });
            }
        }
    }
}
