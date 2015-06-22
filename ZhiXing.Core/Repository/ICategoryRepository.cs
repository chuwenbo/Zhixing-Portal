using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;

namespace ZhiXing.Core.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategorys(int pageIndex, int pageSize, string nameFilters);
    }
}
