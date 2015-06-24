using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;

namespace ZhiXing.Core.Repository
{
    public interface IImageHashReporsitory
    {
        List<ImageHash> GetImageHash(List<string> hashCodes);
        bool CreatImageHash(string url, string imageHashCode);
    }
}
