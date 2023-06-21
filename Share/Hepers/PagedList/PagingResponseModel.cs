using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Hepers.PagedList
{
    public class PagingResponseModel<T> where T : class
    {
        public int TotalRecords { get; set; } // 
        public int CurrentPageNumber { get; set; } // số trang hiện tại
        public int PageSize { get; set; } // kích thước trang
        public int TotalPages { get; set; } // tổng số trang
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public T Data { get; set; } // 
        public PagingResponseModel(T data, int totalRecords, int currentPageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;

            // total pages count
            TotalPages = Convert.ToInt32(Math.Ceiling(((double)TotalRecords / (double)pageSize)));

            HasNextPage = CurrentPageNumber < TotalPages;
            HasPreviousPage = CurrentPageNumber > 1;
        }
    }
}
