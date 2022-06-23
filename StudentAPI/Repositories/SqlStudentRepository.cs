using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync(); //çekerken hepsini alacak
                                                                                               //adres ve gender da
        }
        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x=> x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Gender.ToListAsync(); //gender
        }

        public async Task<bool> Exists(Guid studentId)
        {
          return await _context.Student.AnyAsync(x => x.Id == studentId); //varmı yokmu
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
          var existingStudent = await GetStudentAsync(studentId);
            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;     //mapleme işlemi//ben maplemeyi otomatik yapınca bir öyle denerim
                existingStudent.LastName = request.LastName;      //yapılmış linki bir denerim
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAdress = request.Address.PhysicalAdress;
                existingStudent.Address.PostalAdress = request.Address.PostalAdress;

               await _context.SaveChangesAsync();
                return existingStudent;

            } 
            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                _context.Student.Remove(existingStudent);
                await _context.SaveChangesAsync();
                return existingStudent;

            }
            return null;
        }

        public async Task<Student> AddStudent(Student request) //request yerine başka bir şeyde yazılabilir
        {
           var student = await _context.Student.AddAsync(request);
            await _context.SaveChangesAsync();
            return student.Entity;
        }
    }
}
