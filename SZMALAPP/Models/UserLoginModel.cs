using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SZMALAPP.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Pole nie może być puste")]
       // [Remote("UserExists","Login",HttpMethod ="POST",ErrorMessage ="Ten login jest już używany")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [MinLength(3, ErrorMessage = "Hasło musi się składać z co najmniej 3 znaków")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Hasła różnią się")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Adres E-Mail nie jest poprawny")]
        public string Email { get; set; }

    }
}