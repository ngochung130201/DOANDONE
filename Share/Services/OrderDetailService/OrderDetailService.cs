using Microsoft.EntityFrameworkCore;
using Share.Data;
using Share.Exceptions;
using Share.Models;
using Share.Repository;
using Share.UnitOfWork;
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

namespace Share.Services.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
     
        private readonly IRepositoryBase<OrderDetail> _repositoryBase;
       
        public OrderDetailService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<OrderDetail> repositoryBase,

             DoAnBanHangContext context
            )
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _repositoryBase = repositoryBase;
        }
        public async Task<OrderDetail> AddEnity(OrderDetail enity)
        {
            try
            {

                var reuslt = await _repositoryBase.AddEnity(enity);

                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch(Exception ex)
            {
                throw new BadRequestException("Lỗi khi thêm OrderDetail");
            }


        }

        public async Task<bool> DeleteEnity(OrderDetail enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            return true;
        }

     

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();

          

        }

        public async Task<OrderDetail> GetByWhereAsync(int id)
        {
            return await _repositoryBase.GetByWhereAsync(id);
        }

      

        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, OrderDetail enity)
        {
            await _repositoryBase.UpdateEnity(Id,enity);
            _unitOfWork.SaveChanges();
              return true;
        }
      
    }
}