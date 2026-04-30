using EmployeeAPI_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeAPI_MVC.Controllers
{

    public class LectureController : Controller
    {
        private readonly ContosoUniversityContext db;


        // 注入 ContosoUniversityContext 命名為 db
        public LectureController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        // 分頁設計
        // Index(int? page)
        // 分頁數字 query string 直接 Model Binding 進來

        public IActionResult Index(int page = 1)
        {
            // ToListAsync() 取出資料會太浪費記憶體
            // AsQueryable() 查詢語法
            // var paged = await data.ToPagedListAsync(page, 4);   會出錯找不到ToPagedListAsync
            // 改用同步的做法

            var data = db.Courses.Include(p => p.Department).AsQueryable();

            var paged = data.ToPagedList(page, 4);

            return View(paged);
        }


        /*
            // 原先資料全部取出來

            public async Task<IActionResult> Index()
            {
                // Department 的資料要用 Include 方法加進來

                return View(await db.Courses.Include(p => p.Department).ToListAsync());
            }
        */



        // Creat 要思考要取得幾個欄位
        // 先設計好 Model
        // 做兩個 Controller action   GET    POST

        // mvca tab兩次

        // 用 Course來建立 View
        // 選模型快速建立 html


        public ActionResult Create()
        {
            return View();
        }



        // 接資料
        // mvcp tab 兩次

        [HttpPost]
        public ActionResult Create(CourseCreate data)
        {

            // 判斷接收的資料是否有效
            // 有效寫入資料庫
            if (ModelState.IsValid)
            {
                // CourseCreate 轉成 Course 資料

                db.Courses.Add(new Course
                {
                    Title = data.Title,
                    Credits = data.Credits
                });
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            // 轉失敗 CourseCreate data 會傳到 View
            // View 開頭要轉型別讓其一致，轉為 CourseCreate 才能成功

            return View(data);
        }



        // Details 頁面
        // 路由: http:/.../Details/3
        // mvca  + tab tab

        // Views 的資料夾中文命名-課程管理-無法讀到 View，改名為 Lecture
        // LectureController、_Layout也要跟著調整

        public ActionResult Details(int id)
        {
            //  FirstOrDefault() 取得一筆
            //  (p => p.CourseId == id) 條件

            var item = db.Courses.Include(p => p.Department).FirstOrDefault(p => p.CourseId == id);

            if (item is null)
            {
                return NotFound();
            }

            return View(item);
        }






        // Edit 頁面
        // GET: Lecture/Edit/5
        // 跟 Create 頁面很像
        // Model  課程名稱，課程評價
        // 用id  get 點選的課程資料； post 更新資料



        [HttpGet]
        public ActionResult Edit(int id)
        {
            // 找到符合 id 的資料

            var item = db.Courses.FirstOrDefault(p => p.CourseId == id);

            if (item == null)
            {
                return NotFound();
            }

            // 把資料轉成可以放入 CourseEdit

            var info = new CourseEdit
            {
                CourseId = item.CourseId,
                Title = item.Title,
                Credits = item.Credits
            };

            // View 要型別一致，轉為 CourseEdit 才能成功
            return View(info);

        }



        [HttpPost]
        public ActionResult Edit(CourseEdit data)
        {
            // 判斷修改後的資料是否有效
            // 有效寫入資料庫
            if (ModelState.IsValid)
            {

                // 找到符合 id 的資料
                var item = db.Courses.FirstOrDefault(p => p.CourseId == data.CourseId);


                if (item == null)
                {
                    return NotFound();
                }


                // 修改資料
                item.Title = data.Title;
                item.Credits = data.Credits;

                db.SaveChanges();


                return RedirectToAction("Index");
            }

            // 轉失敗 CourseEdit data 會傳到 View
            // View 開頭要轉型別讓其一致，轉為 CourseEdit 才能成功

            return View(data);
        }






        // Delete 頁面


    }
}
