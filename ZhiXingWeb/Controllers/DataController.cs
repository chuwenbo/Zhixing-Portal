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

            foreach(var item in _adminService.GetCategorys(0,Int32.MaxValue))
            {
                categoryList.Add(new CategoryViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            } 

            return Json(categoryList,JsonRequestBehavior.AllowGet);
        }

        #region Category

        public JsonResult GetDesignWorks(string cid, int pageIndex)
        {
            List<DesignViewModel> viewModelList = new List<DesignViewModel>(); 

            int totalCount;

            foreach (var item in _adminService.GetDesignWorks(pageIndex - 1, 8, out totalCount, cid))
            {
                viewModelList.Add(new DesignViewModel()
                {
                    ImageURL = item.URL,
                    Description = item.Description
                });
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    viewModelList.Add(new DesignViewModel()
            //    {
            //        ImageURL = "http://www.flamesun.com/upload/20150514/143158729821520.jpg",
            //        Description = @"“筷做菜”为中国传统家常菜量身定制高水准的复合调味料，简化做菜流程，提升三餐质量，倡导人们回归家庭餐桌，并且把回家吃饭变成可轻松实现的生活方式。"
            //    });
            //}

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
            result.Success = _adminService.UpdateCategory(id, name);

            return Json(result);
        } 

        public JsonResult DeleteCategory(int id)
        {
            MessgeResult result = new MessgeResult();
            result.Success = _adminService.DeleteCategory(id);

            return Json(result);
        }

        public JsonResult CreateCategory(string name)
        {
            MessgeResult result = new MessgeResult();
            result.Success = _adminService.CreateCategory(name);

            return Json(result);
        }

        public JsonResult GetCategoryOptions()
        {
            var categorys = _adminService.GetCategorys(0, Int32.MaxValue);

            return Json(categorys, JsonRequestBehavior.AllowGet);
        }

        #endregion 

        #region Image

        public JsonResult GetImages()
        {
            int pageIndex = Converter.ToInt32(Request.Params["start"]);
            int pageSize = Converter.ToInt32(Request.Params["length"]);
            int totalCount = 0;

            List<ImageViewModel> imangeList = new List<ImageViewModel>();

            foreach (var item in _adminService.GetImagesList(pageIndex, pageSize, out totalCount, string.Empty))
            {
                imangeList.Add(new ImageViewModel()
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName,
                    ImageName = item.URL.Substring(item.URL.LastIndexOf("/")),
                    ImageURL = item.URL,
                    ImageDescription = item.Description
                });
            }

            var data = new
            {
                draw = Request.Params["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount,
                data = imangeList
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadImages()
        {
            MessgeResult result = new MessgeResult(); 

            int category =-1;
            Int32.TryParse(Request.Params["category"],out category);

            string name = Request.Params["name"]; 
            string saveName = Guid.NewGuid().ToString() + name.Substring(name.LastIndexOf("."));
            string relativePath = "/upload/" + saveName;
            string savePath = Server.MapPath("/upload/" + saveName);

            HttpPostedFileBase uploader = Request.Files["file"];

            string imageFileHash = MD5Provider.GetMD5FromFile(uploader.InputStream); 

            if (!_adminService.ExistImageHash(imageFileHash))
            {
                uploader.SaveAs(savePath);
            }

           result.Success = _adminService.UploadImages(imageFileHash, relativePath, category);

            return Json(result);
        }

        public JsonResult DeleteImageRel(int id)
        {
            MessgeResult result = new MessgeResult();
            result.Success = _adminService.DeleteCategoryImageRel(id);

            return Json(result);
        }


        #endregion
    }
}
