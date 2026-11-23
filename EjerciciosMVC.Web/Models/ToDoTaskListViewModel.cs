using EjerciciosMVC.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;


namespace EjerciciosMVC.Web.Models
{
    public class ToDoTaskListViewModel
    {
        [Required (ErrorMessage = "Escriba la tarea")]
        [MaxLength (200, ErrorMessage = "La tarea no puede tener más de 200 caracteres")]
        [Display (Name = "Nueva tarea")]
        public string? NewTitle { get; set; }

        public List<TodoTask> Tasks { get; set; } = new List<TodoTask>();    

    }
}
