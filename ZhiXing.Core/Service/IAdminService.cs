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
    }
}
