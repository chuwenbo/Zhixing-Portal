﻿using System;
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
    }
}
