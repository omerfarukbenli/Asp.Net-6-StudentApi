using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        // [Route("[controller]")]
        public async Task<IActionResult> GetAllGendersAsync()
        {

            var gendersList = await _studentRepository.GetGendersAsync();
            if(gendersList == null || !gendersList.Any())
            {
                return NotFound();
            }


            return Ok(_mapper.Map<List<Gender>>(gendersList)); //ben kendi projemde burada dto kullanacağım
                                                            //adam iki kere student yazdığı için hangisi diye çıkıyor
        }
    }
}
