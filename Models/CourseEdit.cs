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

// Edit 跟 Create 基本相同
// Edit 需要 ID 才能帶入原本資料 (不顯示在View)

public partial class CourseEdit
{

    public int CourseId { get; set; }


    [Required]
    [BindRequired]
    [MinLength(5)]
    [判斷自己以外是否有重複的課程名稱Attribute]
    [StringLength(100)]
    [DisplayName("課程名稱")]

    public string Title { get; set; } = "N/A";


    [Required]
    [BindRequired]
    [Range(0 , 5, ErrorMessage = "請在設定 Credit 的時候使用 1~5 的值")]
    [DisplayName("課程評價")]
    public int Credits { get; set; }

}