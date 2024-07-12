using System.ComponentModel.DataAnnotations;

namespace Portfolio_Admin.Models
{
    public class Admin
    {
        public long? Id { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNo { get; set; }

        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        public string? Password { get; set; }

        [StringLength(20, ErrorMessage = "User type cannot be longer than 20 characters.")]
        public string? UserType { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
        public string? OldPassword { get; set; }
    }
}
