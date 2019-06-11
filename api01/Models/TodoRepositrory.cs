using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace api01.Models
{
    public class TodoRepositrory : ITodoRepository
    {

        private static ConcurrentDictionary<string, TodoItem> _todos =
            new ConcurrentDictionary<string, TodoItem>();

        public TodoRepositrory()
        {
            Add(new TodoItem { Name = "Item" });
        }
        public void Add(TodoItem item)
        {
            item.Key = System.Guid.NewGuid().ToString();
           _todos.TryAdd(item.Key, item);
        }

        public void Update(TodoItem item)
        {
            _todos[item.Key] = item;
           //odos.TryUpdate(item.Key,item)
        }


        public TodoItem Find(string Key)
        {
            TodoItem item;
            _todos.TryGetValue(Key, out item);

            return item;
        }

        public TodoItem Remove(string Key)
        {

            TodoItem item;
            _todos.TryRemove(Key, out item);

            return item;
        }

        
        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }
    }
}
