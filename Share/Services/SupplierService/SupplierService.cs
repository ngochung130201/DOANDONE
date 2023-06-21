using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Polly;
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
using Supplier = Share.Models.Supplier;

namespace Share.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Supplier> _repositoryBase;
        public SupplierService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Supplier> repositoryBase,
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
        public async Task<Supplier> AddEnity(SupplierVM enity)
        {

            try
            {

                var mapperSupplier = _mapper.Map<Supplier>(enity);
                var reuslt = await _repositoryBase.AddEnity(mapperSupplier);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task<bool> DeleteEnity(Supplier enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.SupplierName} thành công");
        }



        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Supplier> GetByWhereAsync(Guid id)
        {

            var Supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierID == id);
            if (Supplier == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return Supplier;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(Guid Id, SupplierVM enity)
        {
            var getSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierID == Id);
            if (getSupplier == null)
            {
                throw new NotFoundException($"Nhà cung cấp {enity.SupplierName} không tồn tại ");
            }
           ;
            getSupplier.Address = enity.Address;
            getSupplier.Phone = enity.Phone;
            getSupplier.Email = enity.Email;
            getSupplier.SupplierName = enity.SupplierName;
        
            _context.Suppliers.Update(getSupplier);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật khách hàng thành công");
        }

    }
}