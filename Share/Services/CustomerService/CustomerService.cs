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
using Customer = Share.Models.Customer;

namespace Share.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Customer> _repositoryBase;
        public CustomerService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Customer> repositoryBase,
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
        public async Task<Customer> AddEnity(CustomerVM enity)
        {

            try
            {

                var mapperCustomer = _mapper.Map<Customer>(enity);
                var reuslt = await _repositoryBase.AddEnity(mapperCustomer);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task<bool> DeleteEnity(Customer enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.FullName} thành công");
        }



        public async Task<List<Customer>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Customer> GetByWhereAsync(string email)
        {

            var Customer = await _context.Customers.FirstOrDefaultAsync(x=>x.Email == email);
            if (Customer == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return Customer;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(Guid Id, CustomerVM enity)
        {
            var getCustomer =  await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == Id);
            if (getCustomer == null)
            {
                throw new NotFoundException($"Không tìm thấy khách hàng {enity.FullName}");
            }
           ;
            getCustomer.Address = enity.Address;
            getCustomer.Phone = enity.Phone;
            getCustomer.Email = enity.Email;
            getCustomer.FullName = enity.FullName;
            _context.Customers.Update(getCustomer);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật khách hàng thành công");
        }

    }
}