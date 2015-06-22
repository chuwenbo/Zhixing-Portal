using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;
using ZhiXing.Core.Repository;

namespace ZhiXing.Core.Service
{
    public class AdminService : IAdminService
    {
        public List<Category> GetCategorys(int pageIndex, int pageSize,string nameFilters = "")
        {
            List<Category> categorys = new List<Category>(); 

            ICategoryRepository categoryRepository = new CategoryRepository();
            categorys = categoryRepository.GetCategorys(pageIndex, pageSize, nameFilters);

            return categorys;
        }
    }
}
