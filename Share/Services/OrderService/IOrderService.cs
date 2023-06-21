using Share.Helpers;
using Share.Hepers.PagedList;
using Share.Models;
using Share.Repository;
using Share.Services.EmailService;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.OrderService
{
    public interface IOrderService
    {

        public Task<PagingResponseModel<List<OrderVM>>> GetAllOrderAsync(int currentPageNumber, int pageSize,
              string sort, string dir, string where, string search);
        public Task<bool> AddOrderAsync(Guid CustomerId, List<OrderItemVM> orderItemVm);
        public Task<Order> GetOrderAsync(Guid orderId);
        public Task<List<OrderDetailVM>> GetOrderDetailAsync(Guid orderId);
        public Task<bool> UpdateOrderAsync(Guid id, OrderUpdateVM Order);
        public Task RemoveOrderAsync(Order Order);
        public Task RemoveOrderAllAsync(List<OrderVM> Order);

        public Task<List<MonthlySaleViewModel>> MonthlySaleViewModel();
        public Task<List<MonthlySaleViewModel>> MonthlySaleViewModelFail();
        public Task<List<MonthlySalesResult>> GetMonthlySales();
        public Task<FormatExport> ExportExcel();
        public Task<FormatExport> MonthlySalesExportExcel();
        public Task<StatisticalVM> StatisticalAsync();


        public Task<MessageEmail> SendMessageAsync(Status status);
        public Task<HistoryOrderVM> GetHistoryOrderAsync(Guid? orderId, string? phone);
        public Task<bool> DeleteEnitys(Guid[] Ids);
    }
}
