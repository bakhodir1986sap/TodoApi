using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Models;

namespace TodoApi.Tests
{
    public class TodoItemsControllerTests
    {
        [Fact]
        public async Task GetTodoItems_ReturnsAllTodoItems()
        {
            // Arrange
            var dbContextMock = new Mock<TodoContext>(new DbContextOptions<TodoContext>());
            var testItems = Enumerable.Range(1, 5).Select(n => new TodoItem { Title = $"Test Item {n}", Description = "Test Description" }).ToList();
            dbContextMock.Setup(db => db.TodoItems).Returns(GetQueryableMockDbSet(testItems));
            var controller = new TodoItemsController(dbContextMock.Object);

            // Act
            var result = await controller.GetTodoItems();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
            var model = Assert.IsType<List<TodoItem>>(actionResult.Value);
            Assert.Equal(5, model.Count());
        }

        [Fact]
        public async Task GetTodoItem_ReturnsTodoItem()
        {
            // Arrange
            var dbContextMock = new Mock<TodoContext>(new DbContextOptions<TodoContext>());
            var testItem = new TodoItem { Id = 1, Title = "Test Item", Description = "Test Description" };
            dbContextMock.Setup(db => db.TodoItems.Find(1)).Returns(testItem);
            dbContextMock.Setup(db => db.TodoItems).Returns(GetQueryableMockDbSet(new List<TodoItem> { testItem }));
            var controller = new TodoItemsController(dbContextMock.Object);

            // Act
            var result = await controller.GetTodoItem(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TodoItem>>(result);
            var model = Assert.IsType<TodoItem>(actionResult.Value);
            Assert.Equal("Test Item", model.Title);
            Assert.Equal("Test Description", model.Description);
        }

        private DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}
