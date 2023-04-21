namespace SchoolManagement.DAL.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
