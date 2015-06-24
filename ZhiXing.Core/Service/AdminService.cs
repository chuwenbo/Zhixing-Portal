using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiXing.Core.Model;
using ZhiXing.Core.Repository;

namespace ZhiXing.Core.Service
{
    public class AdminService : IAdminService
    {
        ICategoryRepository _categoryRepository;
        ICategoryImageRelReporsitory _categoryImageRelReporsitory;
        IImageHashReporsitory _imageHashReporsitory;

        public AdminService()
        {
            _categoryRepository = new CategoryRepository();
            _categoryImageRelReporsitory = new CategoryImageRelReporsitory();
            _imageHashReporsitory = new ImageHashReporsitory();
        }

        public List<Category> GetCategorys(int pageIndex, int pageSize,string nameFilters = "")
        {
            List<Category> categorys = new List<Category>();

            categorys = _categoryRepository.GetCategorys(pageIndex, pageSize, nameFilters);

            return categorys;
        }

        public bool CreateCategory(string name)
        {
            return _categoryRepository.CreateCategory(name);
        }

        public bool DeleteCategory(int id)
        {
            return _categoryRepository.DeleteCategory(id);
        }

        public bool UpdateCategory(int id,string name)
        {
            return _categoryRepository.UpdateCategory(id, name);
        }

        public List<ImageCategoryList> GetImagesList(int pageIndex, int pageSize, out int totalCount, string filters)
        {
            string relFilters = string.Empty;
            var categorys = _categoryRepository.GetCategorys(0, Int32.MaxValue, relFilters);

            if (!string.IsNullOrEmpty(relFilters))
            {
                if (categorys != null && categorys.Count > 0)
                {
                    foreach (var item in categorys)
                    {
                        relFilters += item.Id.ToString() + ",";
                    }

                    relFilters += "-1";
                }
            }

            var imageCategoryRel = _categoryImageRelReporsitory.GetCategoryImageRels(pageIndex, pageSize,out totalCount, relFilters);

            List<string> imageHashList = new List<string>();
            List<ImageHash> imageHashTable = new List<ImageHash>();

            foreach (var item in imageCategoryRel)
            {
                imageHashList.Add(item.ImageHashCode);
            }

            if (imageHashList.Count > 0)
                imageHashTable = _imageHashReporsitory.GetImageHash(imageHashList);

            List<ImageCategoryList> imageCategoryList = new List<ImageCategoryList>();

            foreach(var item in imageCategoryRel)
            {
                imageCategoryList.Add(new ImageCategoryList()
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    CategoryName = categorys.FirstOrDefault(p=>p.Id == item.CategoryId).Name,
                    Description = item.Description,
                    ImageHashCode =item.ImageHashCode,
                    URL = imageHashTable.FirstOrDefault(p=>p.HashCode == item.ImageHashCode).URL
                });
            }

            return imageCategoryList;
        }


        public List<ImageCategoryList> GetDesignWorks(int pageIndex, int pageSize, out int totalCount, string categoryId)
        {  
            var imageCategoryRel = _categoryImageRelReporsitory.GetCategoryImageRels(pageIndex, pageSize, out totalCount, categoryId);

            List<string> imageHashList = new List<string>();
            List<ImageHash> imageHashTable = new List<ImageHash>();

            foreach (var item in imageCategoryRel)
            {
                imageHashList.Add(item.ImageHashCode);
            }

            if (imageHashList.Count > 0)
                imageHashTable = _imageHashReporsitory.GetImageHash(imageHashList);

            List<ImageCategoryList> imageCategoryList = new List<ImageCategoryList>();

            foreach (var item in imageCategoryRel)
            {
                imageCategoryList.Add(new ImageCategoryList()
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId, 
                    Description = item.Description,
                    ImageHashCode = item.ImageHashCode,
                    URL = imageHashTable.FirstOrDefault(p => p.HashCode == item.ImageHashCode).URL
                });
            }

            return imageCategoryList;
        }

        public bool ExistImageHash(string hashCode)
        {
            List<string> hashList = new List<string>();
            hashList.Add(hashCode);

            var imageHash = _imageHashReporsitory.GetImageHash(hashList);

            return imageHash.Count > 0;
        }

        public bool UploadImages(string imageHashCode, string url, int categoryId)
        {
            bool successed = true;
            bool createdImageHash = true;

            if (!this.ExistImageHash(imageHashCode))
            {
                createdImageHash = _imageHashReporsitory.CreatImageHash(url, imageHashCode);
            }

            if (createdImageHash && categoryId > 0)
            {
                successed = _categoryImageRelReporsitory.CreatCategoryImageRel(categoryId, imageHashCode, string.Empty);
            }
            else
            {
                successed = false;
            }

            return successed;
        }

        public bool DeleteCategoryImageRel(int id)
        {
            return _categoryImageRelReporsitory.DeleteCategoryImageRel(id);
        }
    }
}
