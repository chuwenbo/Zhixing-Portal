using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiXing.Core.Model
{
    public class ImageCategoryList
    {
        public int Id { get; set; }
        public string ImageHashCode { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public string URL { get; set; }
    }
}
