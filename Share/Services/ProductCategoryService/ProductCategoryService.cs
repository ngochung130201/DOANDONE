using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Polly;
using Share.Data;
using Share.Exceptions;
using Share.Hepers;
using Share.Hepers.Upload;
using Share.ListException.Brand;
using Share.Logger;
using Share.Models;
using Share.Repository;
using Share.UnitOfWork;
using Share.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ProductCategory = Share.Models.ProductCategory;

namespace Share.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<ProductCategory> _repositoryBase;
        public ProductCategoryService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<ProductCategory> repositoryBase,
             IMapper mapper,
             DoAnBanHangContext context,
             ILoggerManager logger
            )
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _repositoryBase = repositoryBase;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductCategory> AddEnity(ProductCategoryVM enity)
        {

            var checkName = await _context.ProductCategories.FirstOrDefaultAsync(x => x.ProductCateName!.ToLower() == enity.ProductCateName!.ToLower());
                if(checkName != null )
                {
                    throw new BadRequestException($"{checkName.ProductCateName} đã tồn tại");
                }
                var mapperProductCategory = _mapper.Map<ProductCategory>(enity);
                mapperProductCategory.Slug = Common.Slugify(enity.ProductCateName!);
                var reuslt = await _repositoryBase.AddEnity(mapperProductCategory);
                _unitOfWork.SaveChanges();
                return reuslt;
      



        }

        public async Task<bool> DeleteEnity(ProductCategory enity)
        {
            try
            {
                await _repositoryBase.DeleteEnity(enity);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }



        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<ProductCategory> GetByWhereAsync(string slug)
        {

            var ProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Slug == slug);
            if (ProductCategory == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return ProductCategory;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(string slug, ProductCategoryVM enity)
        {
            var getProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Slug!.ToUpper() == slug.ToUpper());
            if (getProductCategory == null)
            {
                throw new NotFoundException($"Không tìm thấy khách hàng {enity.ProductCateName}");
            }
           ;
            getProductCategory.Sort = enity.Sort;
            getProductCategory.UpdateDate = DateTime.Now;
            getProductCategory.MetaDescription = enity.MetaDescription;
            getProductCategory.ProductCateName = enity.ProductCateName;
            getProductCategory.Slug = Common.Slugify(enity.ProductCateName!);
            getProductCategory.MetaKeyword = enity.MetaKeyword;
            getProductCategory.ParentID =enity.ParentID;
          
            _context.ProductCategories.Update(getProductCategory);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật khách hàng thành công");
        }

    }
}