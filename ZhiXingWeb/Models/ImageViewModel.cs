using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiXingWeb.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageHashId { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }

        public string ImageDescription { get; set; }
    }
}