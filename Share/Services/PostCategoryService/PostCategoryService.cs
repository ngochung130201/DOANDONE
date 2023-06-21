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
using PostCategory = Share.Models.PostCategory;

namespace Share.Services.PostCategoryService
{
    public class PostCategoryService : IPostCategoryService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<PostCategory> _repositoryBase;
        public PostCategoryService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<PostCategory> repositoryBase,
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
        public async Task<PostCategory> AddEnity(PostCategoryVM enity)
        {


            var checkName = await _context.PostCategories.FirstOrDefaultAsync(x => x.PostCateName!.ToLower() == enity.PostCateName!.ToLower());
            if (checkName != null)
            {
                throw new BadRequestException($"{checkName!.PostCateName}  đã tồn tại");
            }
            var mapperPostCategory = _mapper.Map<PostCategory>(enity);
            mapperPostCategory.Slug = Common.Slugify(enity.PostCateName!);
            var reuslt = await _repositoryBase.AddEnity(mapperPostCategory);
            _unitOfWork.SaveChanges();
            return reuslt;






        }

        public async Task<bool> DeleteEnity(PostCategory enity)
        {
            try
            {
                await _repositoryBase.DeleteEnity(enity);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<List<PostCategory>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<PostCategory> GetByWhereAsync(string slug)
        {

            var PostCategory = await _context.PostCategories.FirstOrDefaultAsync(x => x.Slug == slug);
            if (PostCategory == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy bài viết");
                }
            }
            return PostCategory;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(string slug, PostCategoryVM enity)
        {
            var getPostCategory = await _context.PostCategories.FirstOrDefaultAsync(x => x.Slug!.ToUpper() == slug.ToUpper());
            if (getPostCategory == null)
            {
                throw new NotFoundException($"Không tìm thấy bài viết{enity.PostCateName}");
            }
           ;
            getPostCategory.Sort = enity.Sort;
            getPostCategory.UpdateDate = DateTime.Now;
            getPostCategory.MetaDescription = enity.MetaDescription;
            getPostCategory.PostCateName = enity.PostCateName;
            getPostCategory.Slug = Common.Slugify(enity.PostCateName);
            getPostCategory.MetaKeyword = enity.MetaKeyword;
            getPostCategory.ParentID = enity.ParentID;

            _context.PostCategories.Update(getPostCategory);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật bài viết thành công");
        }
        public Task<bool> DeleteEnitys(string[] slugs)
        {
            try
            {
                foreach (var slug in slugs)
                {
                    var getPostCategory = _context.PostCategories.FirstOrDefault(x => x.Slug!.ToUpper() == slug.ToUpper());
                    if (getPostCategory == null)
                    {
                        throw new NotFoundException($"Không tìm thấy bài viết{slug}");
                    }
                    _context.PostCategories.Remove(getPostCategory);
                    _unitOfWork.SaveChanges();
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

    }
}