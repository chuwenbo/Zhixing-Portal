using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;

namespace ZhiXing.Core.Service
{
    public interface IAdminService
    {
        List<Category> GetCategorys(int pageIndex, int pageSize, string nameFilters = "");
        bool CreateCategory(string name);
        bool DeleteCategory(int id);
        bool UpdateCategory(int id, string name);
        List<ImageCategoryList> GetImagesList(int pageIndex, int pageSize, out int totalCount, string filters);
        List<ImageCategoryList> GetDesignWorks(int pageIndex, int pageSize, out int totalCount, string categoryId);


        bool ExistImageHash(string hashCode);

        bool UploadImages(string imageHashCode, string url, int categoryId);
        bool DeleteCategoryImageRel(int id);
    }
}
