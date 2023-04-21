namespace SchoolManagement.DAL.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int TeacherId { get; set; }

        public string TeacherName { get; set; }
    }
}
