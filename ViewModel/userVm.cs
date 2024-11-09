using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class userVm
    {
        [Required(ErrorMessage = "you must enter a login ")]
        public string login { get; set; }

        [Required(ErrorMessage = "you must enter a password .")]     
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
