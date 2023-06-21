using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Share.Data;
using Share.Exceptions;
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

namespace Share.Services.BrandService
{
    public class BrandService : IBrandService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Brand> _repositoryBase;

        public BrandService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Brand> repositoryBase,
             IMapper mapper,
              ILoggerManager logger,
             DoAnBanHangContext context
            )
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _repositoryBase = repositoryBase;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Brand> AddEnity(BrandVM brand)
        {

            var checkNameBrand = _context.Brands.Where(x => x.BrandName == brand.BrandName).FirstOrDefault();
            if (checkNameBrand != null)
            {
                _logger.LogError($"Thương hiệu {brand.BrandName} đã tồn tại");
                throw new BrandBadRequestException($"Thương hiệu {brand.BrandName} đã tồn tại");
            }
            //  var path = UploadImage.UploadFile("Brand", "Images", formFile).Result.ToString();
            brand.Image = brand.Image;
            var mapperBrand = _mapper.Map<Brand>(brand);
            var reuslt = await _repositoryBase.AddEnity(mapperBrand);
            _unitOfWork.SaveChanges();
            return reuslt;



        }

        public async Task<bool> DeleteEnity(int brand)
        {

            var getBrand = await _repositoryBase.GetByWhereAsync(brand);
            await CheckBrand(getBrand);
            await _repositoryBase.DeleteEnity(getBrand);
            _unitOfWork.SaveChanges();
            return true;
        }



        public async Task<List<Brand>> GetAllAsync()
        {

            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Brand> GetByWhereAsync(int Id)
        {
            var getBrand = await _repositoryBase.GetByWhereAsync(Id);
            await CheckBrand(getBrand);
            return getBrand;
        }




        public async Task<bool> UpdateEnity(int Id, BrandVM enity)
        {
            var getBrand = await _repositoryBase.GetByWhereAsync(Id);
            await CheckBrand(getBrand);
            //ar mapperBrand = _mapper.Map<Brand>(enity);
            getBrand.BrandName = enity.BrandName;
            getBrand.Image = enity.Image;

            await _repositoryBase.UpdateEnity(Id, getBrand);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật thương hiệu {getBrand.BrandName} thành công");
        }
        public static async Task<Brand> CheckBrand(Brand brand)
        {
            if (brand == null)
            {
                throw new BrandNotFoundException("Thương hiệu không tồn tạị");
            }

            return brand;
        }

        public async Task<bool> DeleteEnitys(int[] Ids)
        {
            try
            {
                foreach (var item in Ids)
                {
                    var getBrand = _repositoryBase.GetByWhereAsync(item);
                    await _repositoryBase.DeleteEnity(getBrand.Result);
                }
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException("Xóa thương hiệu không thành công");
            }
        }
    }
}