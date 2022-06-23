using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        // [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {

            var students = await _studentRepository.GetStudentsAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }
        // [HttpDelete("{id}")]
        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
           
            var student = await _studentRepository.GetStudentAsync(studentId);
            if(student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {

            if(await _studentRepository.Exists(studentId))
            {
                var updatedStudent = await  _studentRepository.UpdateStudent(studentId, _mapper.Map<Models.Student>(request));
                if(updatedStudent  !=  null)
                {
                    return Ok(_mapper.Map<Student>(updatedStudent));
                }
            } //güncelleme de kendi yaptığımız gibi yapabiliriz
             return NotFound();  
          
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {

            if (await _studentRepository.Exists(studentId))
            {
                var student = await _studentRepository.DeleteStudent(studentId);
                if (student != null)
                {
                    return Ok(_mapper.Map<Student>(student));
                }
            } 
            return NotFound();

        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await _studentRepository.AddStudent(_mapper.Map<Models.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, _mapper.Map<Student>(student));
        }

    }
    }

//model student yerine student, diğeri yerine dto
//dto yapabilirsin