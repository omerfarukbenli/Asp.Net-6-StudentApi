using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync(); //tüm liste

        Task<Student> GetStudentAsync(Guid studentId);  //tabloya yansıtma bir tanesi

        Task<List<Gender>> GetGendersAsync(); //drowpdown menüde cinsiyet olsun diye

        Task<bool> Exists(Guid studentId);

        Task<Student> UpdateStudent(Guid studentId, Student student);
        Task<Student>DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student student);
    }
}
