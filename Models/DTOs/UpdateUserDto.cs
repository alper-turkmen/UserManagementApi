using System.ComponentModel.DataAnnotations;

namespace UserManagement.Api.Models.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(200, ErrorMessage = "Email en fazla 200 karakter olabilir")]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string? Address { get; set; }
    }
}