using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;
using ZhiXing.Core.Utility;

namespace ZhiXing.Core.Repository
{
    public class CategoryImageRelReporsitory : ICategoryImageRelReporsitory
    {
        public List<ImageCategoryRel> GetCategoryImageRels(int pageIndex, int pageSize,out int totalCount, string filters = "")
        {
            List<ImageCategoryRel> rels = new List<ImageCategoryRel>();

            StringBuilder sbMain = new StringBuilder();
             
            sbMain.Append("(select Id,[ImageHashCode],[CategoryId],[Description],ROW_NUMBER() OVER (ORDER BY [CategoryId]) AS RowNumber from [dbo].[CategoryImageRel] ");

            if (!string.IsNullOrEmpty(filters))
            {
                sbMain.AppendFormat("where CategoryId in ({0})", filters);
            }
            sbMain.AppendLine(") as T"); 

            DataTable dt = BaseRepository.ExecuteDataTable(string.Format("select top {0} * from {1} where RowNumber >{2} order by RowNumber", pageSize, sbMain.ToString(), pageIndex));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    rels.Add(new ImageCategoryRel()
                    {
                        Id = Converter.ToInt32(item["Id"]),
                        ImageHashCode = Converter.ToStr(item["ImageHashCode"]),
                        CategoryId = Converter.ToInt32(item["CategoryId"]),
                        Description = Converter.ToStr(item["Description"])
                    });
                }
            }


            totalCount = 0;
            DataTable dtCount = BaseRepository.ExecuteDataTable(string.Format("select count(1) from {0}", sbMain.ToString()));

            if (dtCount.Rows.Count > 0)
            {
                totalCount = Converter.ToInt32(dtCount.Rows[0][0]);
            }

            return rels;
        }

        public bool CreatCategoryImageRel(int categoryId, string imageHashCode, string description)
        {
            string sql = string.Format("insert into [dbo].[CategoryImageRel] ([ImageHashCode],[CategoryId],[Description]) values ('{0}','{1}','{2}')", imageHashCode, categoryId, description ?? "");

            return BaseRepository.ExecuteNonQuery(sql) <= 0 ? false : true;
        }

        public bool DeleteCategoryImageRel(int id)
        {
            string sql = string.Format("delete from [dbo].[CategoryImageRel] where id={0}", id);

            return BaseRepository.ExecuteNonQuery(sql) <= 0 ? false : true;
        }
    }
}
