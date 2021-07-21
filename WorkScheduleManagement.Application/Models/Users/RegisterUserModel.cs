using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Application.Models.Users
{
    public class RegisterUserModel
    {
        [Required]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public int Position { get; set; }
        
        public List<UserPosition> AllPositions { get; set; }
        
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, 
            ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", 
            MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}