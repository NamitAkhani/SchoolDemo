using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolDemo.Models;
using SchoolDemo.Security;
using SchoolDemo.Service;
using SchoolDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IHostEnvironment hostEnvironment;
        private readonly StudentContext _studentContext;
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<Student> _StudentRepository;
        private readonly IEmailService _emailService;
        private readonly IDataProtector Protector;
        public HomeController(ILogger<HomeController> logger,IStudentRepository studentRepository , IHostEnvironment hostEnvironment,
            StudentContext studentContext,IUnitofWork unitofWork,IGenericRepository<Student> StudentRepository, IEmailService emailService,
            IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _studentRepository = studentRepository; 
            this.hostEnvironment = hostEnvironment;
            _studentContext = studentContext; 
            _unitofWork = unitofWork; 
            _StudentRepository = StudentRepository; 
            _logger = logger;   
            _emailService = emailService;
            Protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.StudentIdRouteValue);

        }
        [HttpGet]
        public async Task<IActionResult> Mails()
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { "test@gmail.com" },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}","Namit")
                }
            };
            await _emailService.SendTestEmail(options); 
            return View();  
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            //var inc = _unitofWork.StudentRepository.Get().ToList();
            ViewBag.model = _studentContext.Students.ToList();  
            //return View(inc);

            return View();
        }



        [HttpGet]
        
        public ViewResult Create()
        {
            loadDDL();
            loadsem();
            return View();
        }
        //[HttpPost]
       
        //public IActionResult Create(Student model)
        //{
        //    //if (ModelState.IsValid)
        //    //{

                


        //    //    _unitofWork.StudentRepository.Insert(model);
        //    //    _unitofWork.StudentRepository.Save();

                
        //    //    return RedirectToAction("Index");
        //    //}
        //    return View();
        //}
        [HttpGet]
        
        public ViewResult Edit(int id)
        {
            loadDDL();
            loadsem();

            //Student student = _unitofWork.StudentRepository.GetById(id);

            //StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            //{
            //    Id = student.Id,
            //    Name = student.Name,
            //    Fname = student.Fname,
            //    Email = student.Email,
            //    Mobile = student.Mobile,
            //    Description = student.Description,
            //    DepartmentId=student.DepartmentId,
            //    DepartmentName = student.Department.DepartmentName,
            //    SemId=student.SemId ,
            //    sem = student.sem
            //};
            //return View(studentEditViewModel);
            ViewBag.id = id;    
            return View();
        }

        [HttpPost]
       
        public IActionResult Edit([Bind()]Student model)
        {
            //if (ModelState.IsValid)
            //{
               
            //    _unitofWork.StudentRepository.Update(model);
            //    _unitofWork.StudentRepository.Save();
                
            //    return RedirectToAction("index");
            //}
            
            return View();
        }
            private void loadDDL()
            {
            try
            {
                List<Department> dplist = new List<Department>();
                dplist = _studentContext.departments.ToList();
                ViewBag.dplist = dplist;
                dplist.Insert(0, new Department { DepartmentId = 1, DepartmentName = "Please Select" });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Delete(Student student)
        {
           // _unitofWork.StudentRepository.Delete(student);
          //  _unitofWork.StudentRepository.Save();
                 ViewBag.id = student.Id;
            //return RedirectToAction("index");
            return View();
        }
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        //var student = _studentRepository.DeleteStudent(Student);
        //        var std = await _studentContext.Students.FindAsync(id);
        //        if (std != null)
        //        {
        //            _studentContext.Students.Remove(std);
        //            await _studentContext.SaveChangesAsync();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}
        private void loadsem()
        {
            try
            {
                List<Sem> seclist = new List<Sem>();
                seclist = _studentContext.sems.ToList();
                ViewBag.seclist = seclist;
                seclist.Insert(0, new Sem { SemId = 1, SemName = "please select" });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
