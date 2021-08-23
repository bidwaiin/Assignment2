using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ADODNET;
using System.Data.SqlClient;

namespace Assignment2_3.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration Configuration;
        ProcessData ProcessData;
        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            ProcessData = new ProcessData(Configuration.GetConnectionString("DbConnection"));
        }

       
        public IActionResult Index()
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
            List<Product> p=ProcessData.getListProduct(sqlconnection);
            
            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            try
            {
                SqlConnection sqlconnection = ProcessData.GetConnection();
                 ProcessData.saveProduct(sqlconnection,p);
                
            }
            catch(Exception e )
            { 
            
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
           //Product p =ProcessData.getproduct(sqlconnection, id);
           Product p =ProcessData.usp_getproduct(sqlconnection, id);

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            try
            {
                SqlConnection sqlconnection = ProcessData.GetConnection();
                ProcessData.UpdateProduct(sqlconnection, p);

            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
            ProcessData.deleteproduct(sqlconnection, id);

            return RedirectToAction("Index");
        }

    }
}
