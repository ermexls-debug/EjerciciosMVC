using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public enum MealType
    {
        [Display(Name = "Desayuno")]
        Breakfast = 1,

        [Display(Name = "Almuerzo")]
        Lunch = 2,

        [Display(Name = "Cena")]
        Dinner = 3,

        [Display(Name = "Postre")]
        Dessert = 4,

        [Display(Name = "Mecato")]
        Snack = 5
    }
}
