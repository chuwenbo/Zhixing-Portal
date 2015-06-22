using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ZhiXing.Core.Service;
using ZhiXing.Core.Utility;
using ZhiXingWeb.Models;

namespace ZhiXingWeb.Controllers
{
    public class DataController : Controller
    {
        IAdminService _adminService;

        public DataController()
        {
            _adminService = new AdminService();
        }

        //
        // GET: /Data/

        public JsonResult GetSubMenu()
        {
            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
            
            categoryList.Add(new CategoryViewModel() {
                Id=1,
                Name="酒类"
            });

            categoryList.Add(new CategoryViewModel()
            {
                Id = 2,
                Name = "糖水"
            });

            return Json(categoryList,JsonRequestBehavior.AllowGet);
        }

        #region Category

        public JsonResult GetDesignWorks(string cid, int pageIndex)
        {
            List<DesignViewModel> viewModelList = new List<DesignViewModel>(); 

            for (int i = 0; i < 10; i++)
            {
                viewModelList.Add(new DesignViewModel()
                {
                    ImageURL = "http://www.flamesun.com/upload/20150514/143158729821520.jpg",
                    Description = @"“筷做菜”为中国传统家常菜量身定制高水准的复合调味料，简化做菜流程，提升三餐质量，倡导人们回归家庭餐桌，并且把回家吃饭变成可轻松实现的生活方式。"
                });
            }

            return Json(viewModelList);
        }

        public JsonResult GetCategory()
        {
            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();

            int pageIndex =Converter.ToInt32(Request.Params["start"]);
            int pageSize = Converter.ToInt32(Request.Params["length"]); 

             foreach(var item in _adminService.GetCategorys(pageIndex,pageSize))
             {
                 categoryList.Add(new CategoryViewModel()
                 {
                    Id=item.Id,
                    Name=item.Name
                 });
             }

            //categoryList.Add(new CategoryViewModel()
            //{
            //    Id = 1,
            //    Name = "酒类"
            //});

            //categoryList.Add(new CategoryViewModel()
            //{
            //    Id = 2,
            //    Name = "糖水"
            //});

            var data = new
            { 
                draw = Request.Params["draw"],
                recordsTotal = 2,
                recordsFiltered = 2,
                data = categoryList
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        } 

        public JsonResult UpdateCategory(int id,string name)
        {
            MessgeResult result = new MessgeResult();
            result.Success = true;

            return Json(result);
        } 

        public JsonResult DeleteCategory(int id)
        {
            MessgeResult result = new MessgeResult();
            result.Success = true;

            return Json(result);
        }

        public JsonResult CreateCategory(string name)
        {
            MessgeResult result = new MessgeResult();
            result.Success = true;

            return Json(result);
        }

        #endregion


        #region Image

        public JsonResult GetImages()
        {
            List<ImageViewModel> imangeList = new List<ImageViewModel>();

            imangeList.Add(new ImageViewModel()
            {
                CategoryName="酒",
                ImageName = "135503429458951.jpg",
                ImageURL = "http://i1.hoopchina.com.cn/blogfile/201212/09/135503429458951.jpg"

            });

            imangeList.Add(new ImageViewModel()
            {
                CategoryName = "糖水",
                ImageName = "135503428957435.jpg",
                ImageURL = "http://i1.hoopchina.com.cn/blogfile/201212/09/135503428957435.jpg"
            });

            var data = new
            {
                draw = Request.Params["draw"],
                recordsTotal = 2,
                recordsFiltered = 2,
                data = imangeList
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        } 

        public JsonResult UploadImages()
        {
            MessgeResult result = new MessgeResult();
            result.Success = true;
            string name = Request.Params["name"];

     

            HttpPostedFileBase uploader = Request.Files["file"];

            string imageFileHash = MD5Provider.GetMD5FromFile(uploader.InputStream);


            //string savePath = Server.MapPath("/upload/" + name);
            //uploader.SaveAs(savePath); 


            return Json(result);
        }

        #endregion
    }
}
