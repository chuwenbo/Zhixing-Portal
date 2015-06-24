using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;

namespace ZhiXing.Core.Repository
{
    public interface ICategoryImageRelReporsitory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters">sql in condition </param>
        /// <returns></returns>
        List<ImageCategoryRel> GetCategoryImageRels(int pageIndex, int pageSize, out int totalCount, string filters = "");

        bool CreatCategoryImageRel(int categoryId, string imageHashCode, string description);

        bool DeleteCategoryImageRel(int id);
    }
}
