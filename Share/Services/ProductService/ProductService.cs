using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.Security;
using Polly;
using Share.Data;
using Share.Exceptions;
using Share.Hepers;
using Share.Hepers.PagedList;
using Share.ListException.Brand;
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


namespace Share.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DoAnBanHangContext _context;

        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;

        private IMapper _mapper;
        public ProductService(DoAnBanHangContext context, IMapper mapper,
            IUnitOfWork<DoAnBanHangContext> unitOfWork


            )
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<bool> AddProductAsync(ProductVM products)
        {
            var query = "INSERT INTO Products (ProductID, Name, Slug, Image, IsFreeship, ListImage, Price, PromotionPrice, Quantity, Hot, Content, Description, ViewCount, MetaKeyword, MetaDescription, CreateBy, UpdateBy, CreateDate ,UpdateDate,ProductCategoryCateID, CateID, Supplierid, BrandID) " +
                "VALUES(@ProductID, @Name, @Slug, @Image, @IsFreeship, @ListImage, @Price, @PromotionPrice, @Quantity, @Hot, @Content, @Description, @ViewCount," +
                " @MetaKeyword, @MetaDescription, @CreateBy, @UpdateBy, @CreateDate,@UpdateDate, @ProductCategoryCateID, @CateID, @Supplierid, @BrandID)";
            var checkName = await _context.Products.FirstOrDefaultAsync(x => x.Name!.ToLower() == products.Name!.ToLower());
            if (checkName != null)
            {
                return false;
            }
            var parameters = new DynamicParameters();
            parameters.Add("ProductID", Guid.NewGuid(), DbType.Guid);
            parameters.Add("Name", products.Name, DbType.String);
            parameters.Add("Slug", Common.Slugify(products.Name), DbType.String);
            parameters.Add("Image", products.Image, DbType.String);
            parameters.Add("IsFreeship", products.IsFreeship, DbType.Boolean);
            parameters.Add("ListImage", products.ListImage, DbType.String);
            parameters.Add("Price", products.Price, DbType.Decimal);
            parameters.Add("PromotionPrice", products.PromotionPrice, DbType.Decimal);
            parameters.Add("Quantity", products.Quantity, DbType.Int32);
            parameters.Add("Hot", products.Hot, DbType.DateTime);
            parameters.Add("Content", products.Content, DbType.String);
            parameters.Add("Description", products.Description, DbType.String);
            parameters.Add("ViewCount", products.ViewCount, DbType.Int32);
            parameters.Add("MetaKeyword", products.MetaKeyword, DbType.String);
            parameters.Add("MetaDescription", products.MetaDescription, DbType.String);
            parameters.Add("CreateBy", products.CreateBy, DbType.Int32);
            parameters.Add("UpdateBy", products.UpdateBy, DbType.Int32);
            parameters.Add("CreateDate", products.CreateDate, DbType.DateTime);
            // parameters.Add("Brand", products.Brand, DbType.String);
            parameters.Add("UpdateDate", products.UpdateDate, DbType.DateTime);
            parameters.Add("ProductCategoryCateID", products.CateID, DbType.Guid);
            parameters.Add("CateID", products.CateID, DbType.Guid);
            parameters.Add("Supplierid", products.Supplierid, DbType.Guid);
            parameters.Add("BrandID", products.BrandID, DbType.Int32);
            using (var connect = _context.CreateConnection())
            {

                var insertContact = await connect.ExecuteAsync(query, parameters);

            }
            return true;

        }

        public async Task<PagingResponseModel<List<GetAllProductVM>>> GetAllProductAsync(int currentPageNumber, int pageSize,
            string sort, string dir, string where, string search)
        {
            var query = "";
            int maxPagSize = pageSize;
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
                query = @$"SELECT COUNT(*) FROM Products;
                         select b.BrandName ,b.BrandID ,s.SupplierID,p2.CateID ,s.SupplierName,p.Slug  ,p2.ProductCateName  , p.ProductID ,p.Name ,p.Image ,p.IsFreeship ,p.Price ,p.Quantity ,
                        p.PromotionPrice ,p.Hot ,p.ViewCount ,p.CreateDate ,p.UpdateDate , p.Content, p.Description
                         FROM products p 
                        inner join brands b  on p.BrandID  = b.BrandID 
                        inner join suppliers s on p.Supplierid = s.SupplierID 
                        inner join productcategories p2 on p.CateID = p2.CateID 
                         ORDER BY {sort}  {dir}
                         Limit @Skip,@Take";
            }
            else
            {
                query = @$"SELECT COUNT(*) FROM Products;
                        select b.BrandName ,b.BrandID ,s.SupplierID,p2.CateID ,s.SupplierName,p.Slug  ,p2.ProductCateName  , p.ProductID ,p.Name ,p.Image ,p.IsFreeship ,p.Price ,p.Quantity ,
                        p.PromotionPrice ,p.Hot ,p.ViewCount ,p.CreateDate ,p.UpdateDate  , p.Content, p.Description
                         FROM products p 
                        inner join brands b  on p.BrandID  = b.BrandID 
                        inner join suppliers s on p.Supplierid = s.SupplierID 
                        inner join productcategories p2 on p.CateID = p2.CateID 
                        WHERE {where} Like '%{search}%'
                        ORDER BY {sort}  {dir}
                        Limit @Skip,@Take";
            }


            using (var connect = _context.CreateConnection())
            {
                var reader = await connect.QueryMultipleAsync(query, new { Skip = skip, Take = take });
                int count = reader.Read<int>().FirstOrDefault();
                List<GetAllProductVM> allProduct = reader.Read<GetAllProductVM>().ToList();
                var result = new PagingResponseModel<List<GetAllProductVM>>(allProduct, count, currentPageNumber, pageSize);
                return result;


            }

        }

        public async Task<Product> GetProductAsync(string slug)
        {
            // dapper

            //var query = "SELECT * FROM Products WHERE slug = @slug";
            //using (var connect = _context.CreateConnection())
            //{
            //    var Products = await connect.QuerySingleOrDefaultAsync<Product>(query, new { slug });
            //    return Products;

            //}
            var product = await _context.Products.Include(x => x.ProductCategory).Include(x => x.Brand).Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Slug!.ToLower() == slug.ToLower());
            //var resultDetailProduct = await _context.Products.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            //return _mapper.Map<ProductVM>(resultDetailProduct);
            return product;
        }
        public async Task<Product> UpdateProductViewCount(string slug)
        {
            var product = await _context.Products.Include(x => x.ProductCategory).Include(x => x.Brand).Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Slug!.ToLower() == slug.ToLower());
            //var resultDetailProduct = await _context.Products.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            //return _mapper.Map<ProductVM>(resultDetailProduct);
            product!.ViewCount = product.ViewCount + 1;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task RemoveProductAllAsync(Guid[] ProductId)
        {
            //foreach (var item in Product)
            //{
            //    RemoveProductAsync(item.ProductID.ToString());
            //    await _context.SaveChangesAsync();
            //}
            try
            {
                var query = "DELETE FROM Products WHERE ProductID = @ProductId";
                using (var connect = _context.CreateConnection())
                {
                    foreach (var item in ProductId)
                    {
                        var deleteProduct = await connect.ExecuteAsync(query, new { ProductId = item });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"Không tồn tại sản phẩm này");
            }
        }

        public async Task<bool> RemoveProductAsync(Product product)
        {

            try
            {
                _context.Products.Remove(product);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateProductAsync(string slug, ProductVM Product)
        {


            var updateProduct = await _context.Products.FirstOrDefaultAsync(x => x.Slug! == slug);
            if (updateProduct == null)
            {
                throw new NotFoundException($"Không tồn tại sản phẩm này");
            }
            updateProduct.Price = Product.Price;
            updateProduct.Supplierid = Product.Supplierid;
            updateProduct.CateID = Product.CateID;
            updateProduct.BrandID = Product.BrandID;
            updateProduct.CreateBy = Product.CreateBy;
            updateProduct.UpdateDate = DateTime.Now;
            updateProduct.Description = Product.Description;
            updateProduct.Hot = Product.Hot;
            updateProduct.Image = Product.Image;
            updateProduct.ListImage = Product.ListImage;
            updateProduct.MetaDescription = Product.MetaDescription;
            updateProduct.MetaKeyword = Product.MetaKeyword;
            updateProduct.Name = Product.Name;
            updateProduct.PromotionPrice = Product.PromotionPrice;
            updateProduct.Quantity = Product.Quantity;
            updateProduct.Slug = Common.Slugify(Product.Name);
            updateProduct.UpdateBy = Product.UpdateBy;
            updateProduct.UpdateDate = DateTime.Now;
            updateProduct.IsFreeship = Product.IsFreeship;

            updateProduct.ViewCount = Product.ViewCount;
            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();

            return true;


        }

    }
}
