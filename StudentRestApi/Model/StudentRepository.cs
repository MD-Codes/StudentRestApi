
using Microsoft.EntityFrameworkCore;

namespace StudentRestApi.Model
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            var result = _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return result.Result.Entity;
        }

        public async Task DeleteStudent(int studentId)
        {
            var result = await _context.Students.FirstOrDefaultAsync(e => e.StudentId == studentId);
            if (result != null)
            {
                _context.Students.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudent(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(e => e.StudentId == studentId);
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(e=>e.Email == email);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> Search(string name, Gender? gender)
        {
            IQueryable<Student> query = _context.Students;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e=>e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e=> e.Gender == gender);
            }
            return await query.ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var result = await _context.Students.FirstOrDefaultAsync(e => e.StudentId == student.StudentId);

            if (result != null)
            {
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Gender = student.Gender;
                result.Email = student.Email;
                if(student.Department != 0)
                {
                    result.Department = student.Department;
                }
                result.PhotoPath = student.PhotoPath;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
