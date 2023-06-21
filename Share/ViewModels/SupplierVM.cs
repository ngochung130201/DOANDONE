
using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class SupplierVM
    {
      
        public string? SupplierName { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ !.")]
        public string? Phone { get; set; }

      
    }
}
