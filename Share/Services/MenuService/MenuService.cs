using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Share.Data;
using Share.Exceptions;
using Share.Hepers;
using Share.ListException.Brand;
using Share.Logger;
using Share.Models;
using Share.Repository;
using Share.Services.MenuService;
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

namespace Share.Services.MenuService
{
    public class MenuService : IMenuService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Menu> _repositoryBase;
        private IMapper _mapper;
        public MenuService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Menu> repositoryBase,
             ILoggerManager logger,
          DoAnBanHangContext context,
          IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _repositoryBase = repositoryBase;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Menu> AddEnity(Menu enity)
        {
            try
            {
                enity.Link = Common.Slugify(enity.Name);
                var reuslt = await _repositoryBase.AddEnity(enity);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Lỗi khi thêm menu {ex.Message} ");
                throw new BadRequestException($"Lỗi khi thêm menu {ex.Message}");
            }


        }

        public async Task<bool> DeleteEnity(Menu  enity)
        {

            try
            {

                await _repositoryBase.DeleteEnity(enity);
                _unitOfWork.SaveChanges();
                //  throw new OKRequestException($"Xóa {enity.Name} thành công");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Xóa {enity.Name} không thành công");
                throw new BadRequestException($"Xóa {enity.Name} không thành công");
            }
            

        }

     

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();

          

        }

        public async Task<Menu> GetByWhereAsync(int id)
        {
            var getMenu = await _repositoryBase.GetByWhereAsync(id);
            if(getMenu == null)
            {
                _logger.LogError($"{id} không tìm thấy");
                throw new NotFoundException($"{id} không tìm thấy");
            }
            return getMenu;
        }

      

        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, MenuVM enity)
        {
            try
            {
                var getMenu = await _repositoryBase.GetByWhereAsync(Id);
            
                //ar mapperBrand = _mapper.Map<Brand>(enity);
                getMenu.Name = enity.Name;
                getMenu.IsStatus = enity.IsStatus;
                getMenu.Link = Common.Slugify(enity.Name!);

                await _repositoryBase.UpdateEnity(Id, getMenu);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Cập nhật {enity.Name} không thành công");
                throw new BadRequestException($"Cập nhật  {enity.Name} không thành công");
            }
        }
      
    }
}