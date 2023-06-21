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
using ProductComment = Share.Models.ProductComment;

namespace Share.Services.ProductCommentService
{
    public class ProductCommentService : IProductCommentService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<ProductComment> _repositoryBase;
        public ProductCommentService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<ProductComment> repositoryBase,
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
        public async Task<ProductComment> AddEnity(ProductCommentVM enity)
        {

            try
            {

                var mapperProductComment = _mapper.Map<ProductComment>(enity);

                var reuslt = await _repositoryBase.AddEnity(mapperProductComment);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task DeleteEnity(ProductComment enity)
        {

            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.ProductCommentName} thành công");



        }



        public async Task<List<ProductComment>> GetAllAsync()
        {
            var list = await _context.ProductComments.Include(x => x.Product).Include(x => x.Customer).ToListAsync();
            return list;



        }

        public async Task<List<ProductComment>> GetAllAsyncByProductId(string slug)
        {
            var productComment = await _context.ProductComments.Where(x => x.Product.Slug == slug).ToListAsync();
            return productComment;
        }

        public async Task<ProductComment> GetByWhereAsync(Guid id)
        {

            var ProductComment = await _context.ProductComments.Include(x => x.Customer).Include(x => x.Product).FirstOrDefaultAsync(x => x.CommentID == id);
            if (ProductComment == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return ProductComment;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(Guid id, ProductCommentVM enity)
        {
            var getProductComment = await _context.ProductComments.FirstOrDefaultAsync(x => x.CommentID! == id);
            if (getProductComment == null)
            {
                throw new NotFoundException($"Không tìm thấy bình luận {enity.ProductCommentName}");
            }
           ;
            getProductComment.UpdateDate = DateTime.Now;
            getProductComment.CustomerId = enity.CustomerId;
            getProductComment.Detail = enity.Detail;
            getProductComment.ProductCommentName = enity.ProductCommentName;
            getProductComment.ProductId = enity.ProductId;


            _context.ProductComments.Update(getProductComment);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật bình luận thành công");
        }
        public Task DeleteEnitys(Guid[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var getProductComment = _context.ProductComments.FirstOrDefault(x => x.CommentID! == id);
                    if (getProductComment == null)
                    {
                        throw new NotFoundException($"Không tìm thấy bình luận {getProductComment.ProductCommentName}");
                    }
                    _context.ProductComments.Remove(getProductComment);
                    _unitOfWork.SaveChanges();
                }
                throw new OKRequestException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Có lỗi xảy ra khi xóa");
            }
        }

    }
}