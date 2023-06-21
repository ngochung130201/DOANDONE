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
using PostComment = Share.Models.PostComment;

namespace Share.Services.PostCommentService
{
    public class PostCommentService : IPostCommentService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<PostComment> _repositoryBase;
        public PostCommentService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<PostComment> repositoryBase,
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
        public async Task<PostComment> AddEnity(PostCommentVM enity)
        {

            try
            {

                var mapperPostComment = _mapper.Map<PostComment>(enity);

                var reuslt = await _repositoryBase.AddEnity(mapperPostComment);
                _unitOfWork.SaveChanges();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new BrandBadRequestException($"Có lỗi xảy ra khi liên hệ");
            }



        }

        public async Task DeleteEnity(PostComment enity)
        {

            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.PostCommentName} thành công");



        }



        public async Task<List<PostComment>> GetAllAsync()
        {
            var list = await _context.PostComments.Include(x=>x.Post).ToListAsync();
            return list;



        }

        public async Task<PostComment> GetByWhereAsync(Guid id)
        {

            var PostComment = await _context.PostComments.Include(x => x.Post).FirstOrDefaultAsync(x => x.CommentID == id);
            if (PostComment == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return PostComment;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(Guid id, PostCommentVM enity)
        {
            var getPostComment = await _context.PostComments.FirstOrDefaultAsync(x => x.CommentID! == id);
            if (getPostComment == null)
            {
                throw new NotFoundException($"Không tìm thấy khách hàng {enity.PostCommentName}");
            }
           ;
            getPostComment.UpdateDate = DateTime.Now;
            getPostComment.PostID = enity.PostID;
            getPostComment.Detail = enity.Detail;
            getPostComment.PostCommentName = enity.PostCommentName;
            getPostComment.CustomerId = enity.CustomerId;
            getPostComment.Email = enity.Email;
           


            _context.PostComments.Update(getPostComment);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật bình luận thành công");
        }

    }
}