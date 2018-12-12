using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeList.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeList.Controllers
{
    public class EmployeeDetailsController : Controller
    {
       
        public IActionResult Index()
        {
            var Employee = new List<EmployeeForm>();
            using (
                var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");
                //HTTP GET
                var responseTask = client.GetAsync("GetEmployeeDetails");


                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<EmployeeForm>>();

                    Employee = readTask.Result;
                }
                return View(Employee);


            }

        }
        
        public IActionResult ChangeEmpType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangeEmpType(EmployeeForm emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/EmployeeList");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmployeeForm>("", emp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(emp);
        }
            
        }
    }
