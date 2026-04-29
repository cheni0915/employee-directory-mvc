#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmployeeAPI_MVC.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeAPI_MVC.Models;


// View Model ( 專門為了一張 View 設計)
// 把不用的欄位拿掉

public partial class CourseCreate : IValidatableObject
{
    [Required]
    [BindRequired]
    [判斷是否有重複的課程名稱]
    [MinLength(5)]
    [StringLength(100)]
    [DisplayName("課程名稱")]

    public string Title { get; set; } = "N/A";


    [Required]
    [BindRequired]
    [Range(0 , 5, ErrorMessage = "請在設定 Credit 的時候使用 1~5 的值")]
    [DisplayName("課程評價")]
    public int Credits { get; set; }


    // 多欄位一起驗證， Model 驗證方式
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Title.Contains("Git") && this.Credits < 3)
        {
            yield return new ValidationResult("Git 課程的評價過低! 不允許你新增", new string[] { nameof(Title) });
        }
    }
}