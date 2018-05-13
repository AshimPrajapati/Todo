using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo1.Models;

namespace Todo1.Controllers
{
    [Produces("application/json")]
    [Route("api/Todolists")]
    public class TodolistsController : Controller
    {
        private readonly TodoContext db;
        private readonly ITodoService iApp;

        public TodolistsController(TodoContext context,ITodoService iAppService)
        {
            db = context;
            iApp = iAppService;
        }


        [Route("~/api/GetList")]
        [HttpGet]
        public IEnumerable<Todolist> GetList()
        {
            return iApp.GetList();
        }

        [Route("~/api/AddList")]
        [HttpPost]
        public IActionResult AddList([FromBody]Todolist obj)
        {

            return iApp.AddList(obj);


        }

        [Route("~/api/UpdateList")]
        [HttpPost]
        public IActionResult UpdateList([FromBody]Todolist obj)
        {
            return iApp.UpdateList(obj);
        }

        [Route("~/api/DeleteList/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var a = iApp.Delete(id);
            return a;
        }
    }
}