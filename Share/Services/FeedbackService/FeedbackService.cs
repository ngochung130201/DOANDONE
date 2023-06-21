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
using Feedback = Share.Models.Feedback;

namespace Share.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Feedback> _repositoryBase;
        public FeedbackService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Feedback> repositoryBase,
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
        public async Task<Feedback> AddEnity(FeedbackVM enity)
        {

            try
            {

                var mapperFeedback = _mapper.Map<Feedback>(enity);
                var reuslt = await _repositoryBase.AddEnity(mapperFeedback);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task<bool> DeleteEnity(Feedback enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.Customer.FullName} thành công");
        }



        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Feedback> GetByWhereAsync(int id)
        {
            var Feedback = await _repositoryBase.GetByWhereAsync(id);
            if (Feedback == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return Feedback;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, FeedbackVM enity)
        {
            var getFeedback = await _repositoryBase.GetByWhereAsync(Id);
            if (getFeedback == null)
            {
                throw new NotFoundException($"Không tìm thấy liên hệ {enity.Customer.FullName}");
            }
            getFeedback.UpdateDate = DateTime.Now;
            getFeedback.CustomerId = enity.CustomerId;
            getFeedback.MemberId = enity.MemberId;

            getFeedback.Content = enity.Content;
            await _repositoryBase.UpdateEnity(Id, getFeedback);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật liên hệ thành công");
        }
        public async Task<bool> DeleteEnitys(int[] Ids)
        {
            try
            {
                foreach (var item in Ids)
                {
                    var getFeedback = _repositoryBase.GetByWhereAsync(item).Result;
                    if (getFeedback == null)
                    {
                        throw new NotFoundException($"Không tìm thấy liên hệ");
                    }
                    _repositoryBase.DeleteEnity(getFeedback);
                    _unitOfWork.SaveChanges();
                }
                throw new OKRequestException($"Xóa liên hệ thành công");
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi xóa liên hệ");
            }
        }

    }
}