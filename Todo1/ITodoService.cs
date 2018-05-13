using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo1.Models;

namespace Todo1
{
    public interface ITodoService
    {
        IEnumerable<Todolist> GetList();
        IActionResult AddList(Todolist obj);
        IActionResult UpdateList(Todolist obj);
        IActionResult Delete(int id);
    }
}
