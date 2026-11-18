using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace EjerciciosMVC.Web.Models
{
    public class PasswordGeneratorViewModel
    {
        [Required (ErrorMessage ="Escriba la longitud de la contraseña")]
        [Range (8,128,ErrorMessage ="La contraseña debe tener entre 8 y 128 caracteres")]
        [Display (Name ="Longitud de la contraseña")]
        public int? PasswordLength { get; set; }

        [Display(Name = "mayúsculas A - Z")]
        public bool IncludeUppercase { get; set; } = true;

        [Display(Name = "Minúsculas a - z")]
        public bool IncludeLowercase { get; set; } = true;

        [Display(Name = "Números 0 - 9")]
        public bool IncludeNumbers { get; set; } = true;

        [Display (Name ="Símbolos (@#$%...)")]
        public bool IncludeSymbols { get; set; }    

        public string? Result { get; set; }

        public void Generate()
        {
            string characters = "";

            if (IncludeUppercase)
            {
                characters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }

            if (IncludeLowercase)
            {
                characters += "abcdefghijklmnopqrstuvwxyz";
            }

            if (IncludeNumbers)
            {
                characters += "0123456789";
            }

            if (IncludeSymbols)
            {
                characters += "!@#$%&*?-_";
            }

            Result = "";

            for (int i = 0; i < PasswordLength.Value; i++)
            {
                int position = RandomNumberGenerator.GetInt32(characters.Length);
                Result += characters[position];
            }
        }



    }
}
