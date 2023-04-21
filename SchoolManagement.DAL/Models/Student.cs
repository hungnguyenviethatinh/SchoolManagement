namespace SchoolManagement.DAL.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}
