using System.ComponentModel;

namespace MvcTodo.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set;}
    }
}
