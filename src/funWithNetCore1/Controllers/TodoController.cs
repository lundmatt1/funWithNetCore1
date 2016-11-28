﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funWithNetCore1.Model;
using Microsoft.AspNetCore.Mvc;

namespace funWithNetCore1.Controllers
{
	// Note that [Controller] is replaced with the name of the controller, which is the controller class name minus the "Controller" suffix.
	[Route("api/[controller]")]
	public class TodoController : Controller
	{
	    private ITodoRepository _todoItems;

	    public TodoController(ITodoRepository todoItems)
		{
			TodoItems = todoItems;
		}

	    public ITodoRepository TodoItems
	    {
		    get { return _todoItems; }
		    set { _todoItems = value; }
	    }

	    [HttpGet]
		public IEnumerable<TodoItem> GetAll()
		{
			return TodoItems.GetAll();
		}

		[HttpGet("{id}", Name = "GetTodo")]
		public IActionResult GetById(string id)
		{
			TodoItem item = TodoItems.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			return new ObjectResult(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] TodoItem item)
		{
			if (item == null)
			{
				return BadRequest();
			}
			TodoItems.Add(item);
			return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(string id, [FromBody] TodoItem item)
		{
			if (item == null || item.Key != id)
			{
				return BadRequest();
			}

			var todo = TodoItems.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			TodoItems.Update(item);
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var todo = TodoItems.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			TodoItems.Remove(id);
			return new NoContentResult();
		}
	}
}
