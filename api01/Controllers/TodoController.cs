using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api01.Models;
namespace api01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        public ITodoRepository todoRepository { get; set; }

        public TodoController(ITodoRepository repository)
        {
            todoRepository = repository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()

        {

            return todoRepository.GetAll();
        }

       [HttpGet("{Id}",Name = "GetTodo")]
        public ActionResult GetbyId(string Id)
        {
            var item = todoRepository.Find(Id);
            if (item==null)
            {
                return NotFound();
            }

            return new  ObjectResult(item);
        }

        [HttpPost]
        public ActionResult Create([FromBody] TodoItem item)
        {

            if(item==null)
            {

                return BadRequest();
            }

            todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new {Id= item.Key}, item);
            //return Ok()
        }

        [HttpPut("{Id}")]

        public ActionResult Update( string Id, [FromBody] TodoItem item)
        {

            if (item == null || item.Key != Id)
                return BadRequest();

            todoRepository.Update(item);
            //هیچ مقدار برگشتی ندارد 
            //  در این نوع اپدیت ما نیازی نیست دنبل مقدار اپدیت بگردیم بلکه تمام مقادیر را 
            return new NoContentResult();
        }

        [HttpPatch("{Id}")]
        /////   در این حالت ما یک ایتم داریم برای اپدیت شدن و یک ای دی ککه باید اول پیدا کنیم ایتم ان رو 
        ///بعد میاییم مقدار ای دی فایند شده رو درون ایتمی که قراره اپدیت بشه میفرستیم 
        /// در اینجا ای دی ما برای اپدیت عوض نخواند شد در عملیات اپدیت در حالت بالا ما برامون مهم نیست کلید اصلی مقدرا تازه به خودش بگیره 
        /// اماا در این شرایط لازم داریم که کلید اصلی عوش نشد 
        /// 
        ///

        public IActionResult Update([FromBody] TodoItem item, string Id)

        {
            

            if (item==null)
            {
                return BadRequest();
            }


            var todoitem = todoRepository.Find(Id);
            if (todoitem== null)
            {
                return BadRequest();

            }


            item.Key = todoitem.Key;

            todoRepository.Update(item);

            return new NoContentResult();
        }

        [HttpDelete("{Id}")]

        public IActionResult Remove(string Id)
        {
            var item = todoRepository.Find(Id);
            if (item==null)
            {
                return NotFound();
            }
            todoRepository.Remove(Id);

            return new NoContentResult();

        }



    }
}