using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.Security;
using Polly;
using Share.Data;
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
using Microsoft.Graph.Models;
using Status = Share.Helpers.Status;
using Share.Services.EmailService;
using Share.Services.CustomerService;
using Share.Helpers;
using Share.Exceptions;

namespace Share.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DoAnBanHangContext _context;
        private readonly IEmailService _emailService;

        private readonly ICustomerService _customerService;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;

        private IMapper _mapper;
        public OrderService(DoAnBanHangContext context, IMapper mapper,
            IUnitOfWork<DoAnBanHangContext> unitOfWork,
            IEmailService emailService,
            ICustomerService custommerService


            )
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _customerService = custommerService;
        }
        public async Task<bool> AddOrderAsync(Guid CustomerId, List<OrderItemVM> orderItemVm)
        {


            var openOrder = _context.Orders.FirstOrDefault(o => o.CustomerID == CustomerId && o.OrderDate == null);

            // If there is no open order for the customer, create a new one
            if (openOrder == null)
            {
                openOrder = new Order
                {
                    OrderID = Guid.NewGuid(),
                    CustomerID = CustomerId,
                    TotalAmount = 0,
                    OrderDate = DateTime.Now,
                    IsStatus = Status.Confirm,
                    OrderDetails = new List<OrderDetail>()
                };

            }

            foreach (var orderItem in orderItemVm)
            {
                var orderDetail = openOrder.OrderDetails.FirstOrDefault(od => od.ProductID == orderItem.ProductId);

                // If the product is not in the order, add a new order detail
                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid(),
                        ProductID = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Price,

                    };
                    openOrder.OrderDetails.Add(orderDetail);
                }
                else // If the product is already in the order, update the quantity
                {
                    orderDetail.Quantity += orderItem.Quantity;
                }

                // Update the total amount of the order
                openOrder.TotalAmount += orderItem.Quantity * orderItem.Price;

            }

            // Save the changes to the database
            // call function send mail
            var message = await SendMessageAsync(Status.Unconfirmed);
            var emailModel = new EmailVM
            {
                Name = openOrder.Customer.FullName ?? "",
                To = openOrder.Customer.Email ?? ""
            };
            MessageEmail messageEmail = await SendMessageAsync(status: Status.Unconfirmed);
            emailModel.Body = messageEmail.Body;
            emailModel.Subject = messageEmail.Subject;
            _emailService.HandleSendMail(emailModel, orderDetails: openOrder.OrderDetails, openOrder.TotalAmount);

            _context.Orders.Add(openOrder);
            _context.SaveChanges();
            return true;



        }

        public async Task<PagingResponseModel<List<OrderVM>>> GetAllOrderAsync(int currentPageNumber, int pageSize,
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
                sort = "OrderDate";
                dir = "desc";

            }
            if (search == null)
            {
                query = @$"SELECT COUNT(*) FROM Orders;
                            SELECT o.OrderID ,o.OrderDate ,o.CustomerID ,o.IsStatus ,o.OrderID ,o.MemberId ,c.FullName ,c.Phone ,c.Email,c.Address ,o.TotalAmount 
                            FROM  Orders o   
                            INNER JOIN  Customers c on o.CustomerID  = c.CustomerId    
                            ORDER BY {sort}  {dir}
                            Limit @Skip,@Take";
            }
            else
            {
                query = @$"SELECT COUNT(*) FROM Orders;
                        select o.OrderID ,o.OrderDate ,o.CustomerID ,o.IsStatus ,o.OrderID ,o.MemberId ,c.FullName ,c.Phone ,c.Email,c.Address ,o.TotalAmount 
                        FROM  Orders o   
                        INNER JOIN  Customers c  on o.CustomerID  = c.CustomerId   
                         WHERE {where} Like '%{search}%'
                         ORDER BY {sort}  {dir}
                         Limit @Skip,@Take";
            }


            using (var connect = _context.CreateConnection())
            {
                var reader = await connect.QueryMultipleAsync(query, new { Skip = skip, Take = take });
                int count = reader.Read<int>().FirstOrDefault();
                List<OrderVM> allOrder = reader.Read<OrderVM>().ToList();
                var result = new PagingResponseModel<List<OrderVM>>(allOrder, count, currentPageNumber, pageSize);
                return result;


            }

        }

        public Task<bool> AddOrderAsync(OrderVM Order)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDetailVM>> GetOrderDetailAsync(Guid orderId)
        {
            // dapper

            // var query = @$"select * 
            // from  orders o 
            // inner join orderdetails o2 on o.OrderID  = o2.OrderId 
            // inner join customers c   on o.CustomerID  = c.CustomerId  
            // inner join products p   on p.ProductID  = O2.ProductID  
            // where o.OrderID  = @orderId";
            var order = await _context.OrderDetails.Include(x => x.Product).Where(x => x.OrderId == orderId).ToListAsync();
            var result = _mapper.Map<List<OrderDetailVM>>(order);
            return result;

            //var resultDetailOrder = await _context.Orders.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            //return _mapper.Map<OrderVM>(resultDetailOrder);
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            // dapper

            // var query = @$"select * 
            // from  orders o 
            // inner join orderdetails o2 on o.OrderID  = o2.OrderId 
            // inner join customers c   on o.CustomerID  = c.CustomerId  
            // inner join products p   on p.ProductID  = O2.ProductID  
            // where o.OrderID  = @orderId";
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == orderId);

            //return order;

            //var resultDetailOrder = await _context.Orders.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            return order;
        }
        public async Task RemoveOrderAllAsync(List<OrderVM> Order)
        {
            //foreach (var item in Order)
            //{
            //    RemoveOrderAsync(item.OrderID.ToString());
            //    await _context.SaveChangesAsync();
            //}
        }

        public async Task RemoveOrderAsync(Order Order)
        {

            _context.Orders.Remove(Order);
            _unitOfWork.SaveChanges();
        }
        public async Task<bool> UpdateOrderAsync(Guid id, OrderUpdateVM order)
        {


            var updateOrder = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (updateOrder != null)
            {
                updateOrder.MemberId = order.MemberId;
                updateOrder.IsStatus = order.IsStatus;
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == updateOrder.CustomerID);
                var emailModel = new EmailVM
                {
                    Name = customer.FullName,
                    To = customer.Email
                };
                MessageEmail messageEmail = await SendMessageAsync(updateOrder.IsStatus);
                emailModel.Body = messageEmail.Body;
                emailModel.Subject = messageEmail.Subject;



                _context.Orders.Update(updateOrder);
                await _context.SaveChangesAsync();
                var orderDetails = await _context.OrderDetails.Include(x => x.Product).Where(x => x.OrderId == id).ToListAsync();
                _emailService.HandleSendMail(emailModel, orderDetails, updateOrder.TotalAmount);
                return true;
            }
            return false;

        }
        // tinh tong don hang trang 1 thang
        public async Task<List<MonthlySaleViewModel>> MonthlySaleViewModel()
        {

            var monthlySales = from order in _context.Orders
                               where order.IsStatus == Status.Sussess
                               group order by new { Year = order.OrderDate.Year, Month = order.OrderDate.Month } into g

                               select new MonthlySaleViewModel
                               {
                                   Year = g.Key.Year,
                                   Month = g.Key.Month,
                                   TotalAmount = g.Sum(x => x.TotalAmount),
                                   OrderCount = g.Count()
                               };

            return monthlySales.ToList();
        }
        public async Task<List<MonthlySaleViewModel>> MonthlySaleViewModelFail()
        {

            var monthlySales = from order in _context.Orders
                               where order.IsStatus == Status.fail
                               group order by new { Year = order.OrderDate.Year, Month = order.OrderDate.Month } into g

                               select new MonthlySaleViewModel
                               {
                                   Year = g.Key.Year,
                                   Month = g.Key.Month,
                                   TotalAmount = g.Sum(x => x.TotalAmount),
                                   OrderCount = g.Count()
                               };

            return monthlySales.ToList();
        }
        // check status order : send mail
        public async Task<MessageEmail> SendMessageAsync(Status status)
        {
            var messageEmail = new MessageEmail();
            if (status == Status.Unconfirmed)
            {
                messageEmail.Body = "Đơn hàng của bạn đang chờ xác nhận bởi quản trị viên";
                messageEmail.Subject = "Đặt hàng thành công";
            };
            if (status == Status.Confirm)
            {
                messageEmail.Body = "Đơn hàng của bạn đã được xác nhận và đang giao đến bạn";
                messageEmail.Subject = "Xác nhận đơn hàng";
            };
            if (status == Status.Sussess)
            {
                messageEmail.Body = "Đơn hàng của bạn đã được giao";
                messageEmail.Subject = "Giao hàng thành công";
            };
            if (status == Status.fail)
            {
                messageEmail.Body = "Đơn hàng của bạn thất bại";
                messageEmail.Subject = "Giao hàng thất bại";
            };

            return messageEmail;
        }

        public async Task<List<MonthlySalesResult>> GetMonthlySales()
        {
            var monthlySales = await _context.Orders
                 .GroupBy(o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month })
                 .Select(g => new MonthlySalesResult
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     TotalSalesFail = g.Where(x => x.IsStatus == Share.Helpers.Status.fail).Count(),
                     TotalSalesSusses = g.Where(x => x.IsStatus == Share.Helpers.Status.Sussess).Count(),
                 })
                 .OrderBy(g => g.Year)
                 .ThenBy(g => g.Month)
                 .ToListAsync();
            var fullYearData = GetFullYearData(monthlySales);
            return fullYearData;
        }
        private List<MonthlySalesResult> GetFullYearData(List<MonthlySalesResult> monthlySales)
        {
            var fullYearData = new List<MonthlySalesResult>();

            for (int month = 1; month <= 12; month++)
            {
                var monthData = monthlySales.FirstOrDefault(m => (int)m.GetType().GetProperty("Month").GetValue(m) == month);

                if (monthData != null)
                {
                    fullYearData.Add(monthData);
                }
                else
                {
                    fullYearData.Add(new MonthlySalesResult
                    {

                        Year = DateTime.Now.Year,
                        Month = month,
                        TotalSalesFail = 0,
                        TotalSalesSusses = 0,

                    });
                }
            }

            return fullYearData;
        }



        public async Task<FormatExport> ExportExcel()
        {
            // Get the data from the "Order" table
            // Create a StringBuilder to store the CSV content
            var csvContent = new StringBuilder();

            // Append the column headers
            csvContent.AppendLine("Ma don hang,Ten khach hang,Ngay dat,Tong tien,Trang thai");

            // Append the data rows
            // Convert the StringBuilder to a byte array
            byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

            // Return the CSV file

            var query = @$"SELECT COUNT(*) FROM Orders;
                            SELECT o.OrderID ,o.OrderDate ,o.CustomerID ,o.IsStatus ,o.OrderID ,o.MemberId ,c.FullName ,c.Phone ,c.Email,c.Address ,o.TotalAmount 
                            FROM  Orders o   
                            INNER JOIN  Customers c on o.CustomerID  = c.CustomerId  ";
            using (var connect = _context.CreateConnection())
            {
                var reader = await connect.QueryMultipleAsync(query);
                int count = reader.Read<int>().FirstOrDefault();
                List<OrderVM> allOrder = reader.Read<OrderVM>().ToList();
                foreach (var order in allOrder)
                {


                    csvContent.AppendLine($"{order.OrderID},{order.FullName},{order.OrderDate:yyyy-MM-dd},{order.TotalAmount},{order.IsStatus}");

                }
                return new FormatExport
                {
                    FileContext = Encoding.UTF8.GetBytes(csvContent.ToString()),
                    ContextType = "text/csv; charset=utf-16",
                    DownloadName = $"export__{DateTime.Now.ToString("yyyy-MM-dd")}.csv"
                };


            }

        }
        public async Task<FormatExport> MonthlySalesExportExcel()
        {
            // Get the data from the "Order" table
            // Create a StringBuilder to store the CSV content
            var csvContent = new StringBuilder();

            // Append the column headers
            csvContent.AppendLine("Thang,Nam ,Don hang thanh cong,Don hang bi huy");

            // Append the data rows
            // Convert the StringBuilder to a byte array
            byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

            // Return the CSV file
            var monthlySales = await _context.Orders
             .GroupBy(o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month })
             .Select(g => new MonthlySalesResult
             {
                 Year = g.Key.Year,
                 Month = g.Key.Month,
                 TotalSalesFail = g.Where(x => x.IsStatus == Share.Helpers.Status.fail).Count(),
                 TotalSalesSusses = g.Where(x => x.IsStatus == Share.Helpers.Status.Sussess).Count(),
             })
             .OrderBy(g => g.Year)
             .ThenBy(g => g.Month)
             .ToListAsync();
            var fullYearData = GetFullYearData(monthlySales);

            foreach (var order in fullYearData)
            {


                csvContent.AppendLine($"{order.Month},{order.Year},{order.TotalSalesSusses},{order.TotalSalesFail}");

            }
            return new FormatExport
            {
                FileContext = Encoding.UTF8.GetBytes(csvContent.ToString()),
                ContextType = "text/csv; charset=utf-16",
                DownloadName = $"MonthlySalesExportExcel_{DateTime.Now.ToString("yyyy-MM-dd")}.csv"
            };


        }
        // thống kê theo tháng

        public async Task<StatisticalVM> StatisticalAsync()
        {
            var orderCount = await _context.Orders.CountAsync();
            var orderStatusUnconfirmed = await _context.Orders.Where(x => x.IsStatus == Share.Helpers.Status.Unconfirmed).CountAsync();
            // doanh thu
            var revenue = await _context.Orders.Where(x => x.IsStatus == Share.Helpers.Status.Sussess).SumAsync(x => x.TotalAmount);
            // customer
            var customer = await _context.Customers.CountAsync();
            // comment
            var comment = await _context.ProductComments.CountAsync();
            // product
            var product = await _context.Products.OrderByDescending(x => x.ViewCount).Take(6).ToListAsync();
            var mapperProduct = _mapper.Map<List<ProductVM>>(product); // Map to ProductVM models
            return new StatisticalVM
            {
                Revenue = revenue,
                TotalOrderUnconfirmed = orderStatusUnconfirmed,
                TotalComment = comment,
                TotalCustomer = customer,
                ProductOrderViewCount = mapperProduct,
                TotalOrder = orderCount
            };
        }

        public async Task<HistoryOrderVM> GetHistoryOrderAsync(Guid? orderId, string? phone)
        {
            var historyOrderVM = new HistoryOrderVM();
            if (orderId == null && phone == null)
            {
                historyOrderVM.Description = "Vui lòng nhập mã đơn hàng hoặc số điện thoại";
                return historyOrderVM;
            }
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == orderId || x.Customer!.Phone == phone);
            if (order != null)
            {
                if (order.IsStatus == Status.Sussess)
                {
                    historyOrderVM.Description = Status.Sussess.GetDescription();
                    historyOrderVM.OrderStatus = order.IsStatus;
                    return historyOrderVM;
                }
                if (order.IsStatus == Status.fail)
                {
                    historyOrderVM.Description = Status.fail.GetDescription();
                    historyOrderVM.OrderStatus = order.IsStatus;
                    return historyOrderVM;
                }
                if (order.IsStatus == Status.Confirm)
                {
                    historyOrderVM.Description = Status.Confirm.GetDescription();
                    historyOrderVM.OrderStatus = order.IsStatus;
                    return historyOrderVM;
                }
            }
            historyOrderVM.Description = "Không tìm thấy đơn hàng";
            return historyOrderVM;
        }
        public Task<bool> DeleteEnitys(Guid[] Ids)
        {
            try
            {
                foreach (var item in Ids)
                {
                    var order = _context.Orders.Where(x => x.OrderID == item).FirstOrDefault();
                    if (order == null)
                    {
                        throw new NotFoundException($"Không tìm thấy đơn hàng {order.OrderID}");
                    }
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
                throw new OKRequestException($"Xóa đơn hàng thành công");
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Xóa đơn hàng thất bại {ex.Message}");
            }
        }
    }

}



