using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api01.Models
{
   public interface ITodoRepository
    {

        void Add(TodoItem item);
        void Update(TodoItem item);

        TodoItem Find(string Key);

        TodoItem Remove(string Key);

        IEnumerable<TodoItem> GetAll();


    }
}
