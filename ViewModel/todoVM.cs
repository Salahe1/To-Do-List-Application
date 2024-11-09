using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class todoVM
    {
        //[Required(ErrorMessage = "You must enter an ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a libelle")]
        [StringLength(50, ErrorMessage = "Libelle cannot be longer than 50 characters")]
        public string Libelle { get; set; }

        [Required(ErrorMessage = "You must enter a description")]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must select a state")]
        public TodoState State { get; set; }

        [Required(ErrorMessage = "You must enter a date limite")]
        [DataType(DataType.Date)]
        [DateNotInPast(ErrorMessage = "Date limite must be today or a future date")]
        public DateTime DateLimite { get; set; }
        public enum TodoState
        {
            ToDo,
            Doing,
            Done
        }  
       
    }

    public class DateNotInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            return date >= DateTime.Now.Date;
        }
    }
}
