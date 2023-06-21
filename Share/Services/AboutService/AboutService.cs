using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Share.Data;
using Share.Exceptions;
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

namespace Share.Services.AboutService
{
    public class AboutService : IAboutService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
     
        private readonly IRepositoryBase<About> _repositoryBase;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AboutService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<About> repositoryBase,
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
        public async Task<About> AddEnity(AboutVM enity)
        {
            try
            {

                var mapper = _mapper.Map<About>(enity);
                var reuslt = await _repositoryBase.AddEnity(mapper);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi giới thiệu");
            }



        }

        public async Task<bool> DeleteEnity(About enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException("Giới thiệu đã bị xóa");
        }

     

        public async Task<List<About>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();

          

        }

        public async Task<About> GetByWhereAsync(int id)
        {
            return await _repositoryBase.GetByWhereAsync(id);
        }

      

        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, AboutVM enity)
        {
            var getAbout = await _repositoryBase.GetByWhereAsync(Id);
            if (getAbout == null)
            {
                throw new NotFoundException($"Không tìm thấy giới thiệu");
            }
            getAbout.UpdateDate = DateTime.UtcNow;
            getAbout.UpdateBy = enity.UpdateBy;
            getAbout.CreateBy = enity.CreateBy;
            getAbout.Tilte = enity.Tilte;
            getAbout.Description = enity.Description;
            getAbout.MetaDescription = enity.MetaDescription;
            getAbout.Facebook = enity.Facebook;
            getAbout.MetaKeyword = enity.MetaKeyword;
            getAbout.Zalo = enity.Zalo;

            await _repositoryBase.UpdateEnity(Id, getAbout);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật giới thiệu thành công");
        }
      
    }
}