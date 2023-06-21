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
using Slider = Share.Models.Slider;

namespace Share.Services.SliderService
{
    public class SliderService : ISliderService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Slider> _repositoryBase;
        public SliderService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Slider> repositoryBase,
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
        public async Task<Slider> AddEnity(SliderVM enity)
        {

            try
            {

                var mapperSlider = _mapper.Map<Slider>(enity);
                var reuslt = await _repositoryBase.AddEnity(mapperSlider);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task<bool> DeleteEnity(Slider enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.Title} thành công");
        }



        public async Task<List<Slider>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Slider> GetByWhereAsync(int id)
        {
            var Slider = await _repositoryBase.GetByWhereAsync(id);
            if (Slider == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return Slider;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, SliderVM enity)
        {
            var getSlider = await _repositoryBase.GetByWhereAsync(Id);
            if (getSlider == null)
            {
                throw new NotFoundException($"Không tìm thấy liên hệ {enity.Title}");
            }
            getSlider.Title = enity.Title;
            getSlider.Link = enity.Link;
            getSlider.Image = enity.Image;
            getSlider.IsStatus = enity.IsStatus;
            await _repositoryBase.UpdateEnity(Id, getSlider);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật liên hệ thành công");
        }

    }
}