using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SchoolDemo.Models;
using SchoolDemo.Security;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _studentRepository;
        private readonly IHostEnvironment hostEnvironment;
        private readonly StudentContext _studentContext;
        private readonly IUnitofWork _unitofwork;
        private readonly IDataProtector Protector;
        public StudentController(IStudentRepository studentRepository, IHostEnvironment hostEnvironment, StudentContext studentContext , IUnitofWork unitofWork, 
            IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _studentRepository = studentRepository;
            this.hostEnvironment = hostEnvironment;
            _studentContext = studentContext;
            _unitofwork = unitofWork;
            Protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.StudentIdRouteValue);
        }
        
        //[Route("api/[controller]")]
        //[HttpGet("Administrator")]
        //[Authorize]
        [HttpGet]
        [Route("GetAllStudent")]
        public  IActionResult GetAllStudent()
        {
            var usename = GetCurrentUser();
            Console.WriteLine(usename.UserName);
            return Ok(_unitofwork.StudentRepository.GetAll());
            //return Ok(await PaginatedList<Student>.CreateAsync(_studentContext.Students, pageNumber, 5));
        }

        //[HttpGet("Administrator")]
        [HttpGet]
        [Route("GetStudent/{id}")]
        
        //[Authorize(Roles = "Administrator")]
        public IActionResult GetStudent(int id)
        {
            var student = _unitofwork.StudentRepository.GetById(id);
            return Ok(student);
        }
        [HttpPost]
        // [Authorize(Roles = "Administrator")]
        //[Route("api/[controller]")]
        [Route("Add")]
        public IActionResult Add(Student student)
        {
            
            _unitofwork.StudentRepository.Insert(student);
            _unitofwork.Save();
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + student.Id, student);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        
        public IActionResult Delete(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student != null)
            {
                _unitofwork.StudentRepository.Delete(student);
                _unitofwork.Save();
                return Ok(student);
            }
            return NotFound($"Student With Id:{id} was not found");
        }
        [HttpPut]
        [Route("Update/{id}")]
        
        public IActionResult Update(int id, Student student)
        {
            var existingstudent = _studentRepository.GetStudent(id);
            if (existingstudent != null)
            {
                student.Id = existingstudent.Id;
                _studentRepository.EditStudent(student);
            }
            return Ok(student);
        }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
