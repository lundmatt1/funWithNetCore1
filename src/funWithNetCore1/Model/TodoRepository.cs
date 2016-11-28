using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace funWithNetCore1.Model
{
	public interface ITodoRepository
	{
		void Add(TodoItem item);
		IEnumerable<TodoItem> GetAll();
		TodoItem Find(string key);
		TodoItem Remove(string key);
		void Update(TodoItem item);
	}

	public class TodoRepository : ITodoRepository
	{
		private static readonly ConcurrentDictionary<string, TodoItem> Todos = new ConcurrentDictionary<string, TodoItem>();

		public TodoRepository()
		{
			Add(new TodoItem { Name = "Item1" });
		}

		public IEnumerable<TodoItem> GetAll()
		{
			return Todos.Values;
		}

		public void Add(TodoItem item)
		{
			item.Key = Guid.NewGuid().ToString();
			Todos[item.Key] = item;
		}

		public TodoItem Find(string key)
		{
			TodoItem item;
			Todos.TryGetValue(key, out item);
			return item;
		}

		public TodoItem Remove(string key)
		{
			TodoItem item;
			Todos.TryRemove(key, out item);
			return item;
		}

		public void Update(TodoItem item)
		{
			Todos[item.Key] = item;
		}
	}
}
