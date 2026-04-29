#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI_MVC.Models;





// 擴充類別寫法
[ModelMetadataType(typeof(CourseMetadata))]
public partial class Course{}



public partial class CourseMetadata
{
    public int CourseId { get; set; }

    [DisplayName("課程名稱")]
    public string Title { get; set; }

    [DisplayName("課程評價")]
    public int Credits { get; set; }

    [DisplayName("部門名稱")]
    public int DepartmentId { get; set; }

    [DisplayName("課程描述")]
    public string Description { get; set; }


}