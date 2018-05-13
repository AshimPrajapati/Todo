using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo1.Models;

namespace Todo1
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext db;
        public TodoService(TodoContext context)
        {
            db = context;
          
        }

        public IActionResult AddList(Todolist obj)
        {
             
            db.Add(obj);
            //db.Entry(obj).State = EntityState.Added;
            db.SaveChanges();

            return new ObjectResult("Task Added successfully!");
        }

        public IActionResult Delete(int id)
        {
            db.Todolist.Remove(db.Todolist.Find(id));
            db.SaveChanges();
            return new ObjectResult("Task deleted successfully!");
        }

        public IEnumerable<Todolist> GetList()
        {
            return db.Todolist;
        }

        public IActionResult UpdateList(Todolist obj)
        {
            var oldTask = db.Todolist.Find(obj.Id);
            oldTask.IsCompleted = obj.IsCompleted;
            //db.Entry(obj).State = EntityState.Modified;
           
            db.SaveChanges();
            db.Dispose();
            
            return new ObjectResult("Task Completed!");
        }
    }
}
