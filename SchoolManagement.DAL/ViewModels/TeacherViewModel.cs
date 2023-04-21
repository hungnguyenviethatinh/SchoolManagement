namespace SchoolManagement.DAL.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}
