using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;
using ZhiXing.Core.Utility;

namespace ZhiXing.Core.Repository
{
    public class ImageHashReporsitory : IImageHashReporsitory
    {
        public List<ImageHash> GetImageHash(List<string> hashCodes)
        {
            List<ImageHash> imageHash = new List<ImageHash>();

            StringBuilder sb = new StringBuilder();


            sb.Append("select Id,[URL],[HashCode] from [dbo].[ImageHash] ");

            if (hashCodes != null && hashCodes.Count > 0)
            {
                sb.Append("where HashCode in (");

                foreach (var item in hashCodes)
                {
                    sb.Append(string.Format("'{0}',", item));
                }

                sb.Append("'')");
            }

            DataTable dt = BaseRepository.ExecuteDataTable(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    imageHash.Add(new ImageHash()
                    {
                        Id = Converter.ToInt32(item["Id"]),
                        URL = Converter.ToStr(item["URL"]),
                        HashCode = Converter.ToStr(item["HashCode"])
                    });
                }
            }

            return imageHash;
        }

        public bool CreatImageHash(string url,string imageHashCode)
        {
            string sql = string.Format("insert into ImageHash (URL,HashCode) values ('{0}','{1}')", url, imageHashCode);

            return BaseRepository.ExecuteNonQuery(sql) <= 0 ? false : true;
        }
    }
}
