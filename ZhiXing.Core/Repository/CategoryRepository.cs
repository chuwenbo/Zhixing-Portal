using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;
using ZhiXing.Core.Utility;

namespace ZhiXing.Core.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategorys(int pageIndex, int pageSize, string nameFilters)
        {
            List<Category> categorys = new List<Category>();

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("select top {0} * from ", pageSize);
            sb.Append("(select Id,Name,ROW_NUMBER() OVER (ORDER BY Name) AS RowNumber from [dbo].[Category] ");

            if (!string.IsNullOrEmpty(nameFilters))
            {
                sb.AppendFormat("where Name like '%{0}%'", nameFilters);
            }

            sb.AppendFormat(") as T where RowNumber >{0} order by RowNumber", pageIndex);

            DataTable dt = BaseRepository.ExecuteDataTable(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    categorys.Add(new Category()
                    {
                        Id = Converter.ToInt32(item["Id"]),
                        Name = Converter.ToStr(item["Name"])
                    });
                }
            }

            return categorys;
        }
    }
}
