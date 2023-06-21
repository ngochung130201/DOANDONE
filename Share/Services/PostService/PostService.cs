using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.Security;
using Polly;
using Share.Data;
using Share.Exceptions;
using Share.Hepers;
using Share.Hepers.PagedList;
using Share.Models;
using Share.Repository;
using Share.UnitOfWork;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Share.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DoAnBanHangContext _context;

        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;

        private IMapper _mapper;
        public PostService(DoAnBanHangContext context, IMapper mapper,
            IUnitOfWork<DoAnBanHangContext> unitOfWork


            )
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddPostAsync(PostVM Posts)
        {
            var query = "INSERT INTO Posts (PostID, Name, Slug, Image, Content, Description, ViewCount, MetaKeyword, MetaDescription, CreateBy, UpdateBy, CreateDate, UpdateDate, PostCategoryCateID, CateID) " +
                "VALUES(@PostID, @Name, @Slug, @Image, @Content, @Description, @ViewCount," +
                " @MetaKeyword, @MetaDescription, @CreateBy, @UpdateBy, @CreateDate, @UpdateDate, @PostCategoryCateID, @CateID)";

            var checkName = await _context.Posts.FirstOrDefaultAsync(x => x.Name!.ToLower() == Posts.Name!.ToLower());
            if (checkName != null)
            {
                return false;
            }
            
            var parameters = new DynamicParameters();
            parameters.Add("PostID", Guid.NewGuid(), DbType.Guid);
            parameters.Add("Name", Posts.Name, DbType.String);
            parameters.Add("Slug", Common.Slugify(Posts.Name!), DbType.String);
            parameters.Add("Image", Posts.Image, DbType.String);
            parameters.Add("Content", Posts.Content, DbType.String);
            parameters.Add("Description", Posts.Description, DbType.String);
            parameters.Add("ViewCount", Posts.ViewCount, DbType.Int32);
            parameters.Add("MetaKeyword", Posts.MetaKeyword, DbType.String);
            parameters.Add("MetaDescription", Posts.MetaDescription, DbType.String);
            parameters.Add("CreateBy", Posts.CreateBy, DbType.Int32);
            parameters.Add("UpdateBy", Posts.UpdateBy, DbType.Int32);
            parameters.Add("CreateDate", DateTime.Now, DbType.DateTime);
            parameters.Add("UpdateDate", DateTime.Now, DbType.DateTime);
            parameters.Add("PostCategoryCateID", Posts.CateID, DbType.Guid);
            parameters.Add("CateID", Posts.CateID, DbType.Guid);

            using (var connect = _context.CreateConnection())
            {

                var insertContact = await connect.ExecuteAsync(query, parameters);

            }
            return true;

        }

        public async Task<PagingResponseModel<List<GetAllPosts>>> GetAllPostAsync(int currentPageNumber, int pageSize,
            string sort, string dir, string where, string search)
        {
            var query = "";
            int maxPagSize = 5;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;
            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;
            // where o day la tim kiem theo truong nao trong table
            if (sort == null)
            {
                sort = "ViewCount";
                dir = "desc";

            }
            if (search == null)
            {
                query = @$"SELECT COUNT(*) FROM Posts;
                       select p.PostID ,p.Name ,P.Slug ,p.Image ,p.ViewCount ,p.CreateBy ,p.UpdateBy ,p.CreateDate ,p.UpdateDate ,p2.PostCateName 
                        FROM  posts p 
                        INNER JOIN postcategories p2 on p.CateID = p2.CateID 
                        
                         ORDER BY {sort}  {dir}
                         Limit @Skip,@Take";
            }
            else
            {
                query = @$"SELECT COUNT(*) FROM Posts;
                      select p.PostID ,p.Name ,P.Slug ,p.Image ,p.ViewCount ,p.CreateBy ,p.UpdateBy ,p.CreateDate ,p.UpdateDate ,p2.PostCateName 
                        FROM  posts p 
                        INNER JOIN postcategories p2 on p.CateID = p2.CateID 
                       
                         WHERE {where} Like '%{search}%'
                         ORDER BY {sort}  {dir}
                         Limit @Skip,@Take";
            }


            using (var connect = _context.CreateConnection())
            {
                var reader = await connect.QueryMultipleAsync(query, new { Skip = skip, Take = take });
                int count = reader.Read<int>().FirstOrDefault();
                List<GetAllPosts> allPost = reader.Read<GetAllPosts>().ToList();
                var result = new PagingResponseModel<List<GetAllPosts>>(allPost, count, currentPageNumber, pageSize);
                return result;


            }

        }

        public async Task<Post> GetPostAsync(string slug)
        {
            // dapper

            var post = await _context.Posts.Include(x => x.PostCategory).FirstOrDefaultAsync(x => x.Slug!.ToLower() == slug.ToLower());
            //var resultDetailProduct = await _context.Products.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            //return _mapper.Map<ProductVM>(resultDetailProduct);
            return post;

            //var resultDetailPost = await _context.Posts.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            //return _mapper.Map<PostVM>(resultDetailPost);
        }


        public async Task RemovePostAllAsync(List<PostVM> Post)
        {
            //foreach (var item in Post)
            //{
            //    RemovePostAsync(item.PostID.ToString());
            //    await _context.SaveChangesAsync();
            //}
        }

        public async Task<bool> RemovePostAsync(Post Post)
        {

            try
            {
                _context.Posts.Remove(Post);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdatePostAsync(string slug, PostVM Post)
        {


            var updatePost = await _context.Posts.FirstOrDefaultAsync(x => x.Slug! == slug);
            if (updatePost == null)
            {
                throw new NotFoundException($"Không tồn tại sản phẩm này");
            }

            updatePost.CateID = Post.CateID;

            updatePost.CreateBy = Post.CreateBy;
            updatePost.UpdateDate = DateTime.Now;
            updatePost.Description = Post.Description;

            updatePost.Image = Post.Image;

            updatePost.MetaDescription = Post.MetaDescription;
            updatePost.MetaKeyword = Post.MetaKeyword;
            updatePost.Name = Post.Name;

            updatePost.Slug = Common.Slugify(Post.Name);
            updatePost.UpdateBy = Post.UpdateBy;
            updatePost.UpdateDate = DateTime.Now;


            updatePost.ViewCount = Post.ViewCount;
            _context.Posts.Update(updatePost);
            await _context.SaveChangesAsync();

            return true;


        }

    }
}
