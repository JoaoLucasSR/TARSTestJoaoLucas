using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;
using TARSTestJoaoLucas.Repository;
using TARSTestJoaoLucas.Pagination;
using TARSTestJoaoLucas.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TARSTestJoaoLucas.Test
{
    public class WorkerUnitTestController
    {
        private IUnitOfWork repository;
        private ILogger<WorkerController> logger = Mock.Of<ILogger<WorkerController>>();

        public static DbContextOptions<AppDbContext> dbContextOption { get; }

        static WorkerUnitTestController()
        {
            dbContextOption = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        public WorkerUnitTestController()
        {
            var context = new AppDbContext(dbContextOption);
            DbUnitTestsMockInitializer db = new DbUnitTestsMockInitializer();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            db.Seed(context);
            repository = new UnitOfWork(context);
        }

        // Method GET
        [Fact]
        public async Task GetWorkersReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.Get(new PaginationParameters { PageNumber=1, PageSize=2});

            Assert.IsType<PagedList<Worker>>(data.Value);
            Assert.True(controller.Response.Headers["X-Pagination"] != String.Empty);
        }

        // Method GET ID
        [Fact]
        public async Task GetIdWorkerReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.GetId(1);

            Assert.IsType<Worker>(data.Value);
            Assert.Equal("Pedro", data.Value.Name);
        }

        // Method GET Project
        [Fact]
        public async Task GetWorkerProjectReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.GetProject(1, new PaginationParameters { PageNumber = 1, PageSize = 2 });

            Assert.IsType<PagedList<Project>>(data.Value);
            Assert.Equal("TARS T1", data.Value[0].Name);
            Assert.True(controller.Response.Headers["X-Pagination"] != String.Empty);
        }

        // Method PUT
        [Fact]
        public async Task PutWorkerReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var worker = (await controller.GetId(1)).Value;
            worker.Name = "Jorge";
            var data = await controller.Put(1, worker);
            var newWorker = (await controller.GetId(1)).Value;

            Assert.IsType<NoContentResult>(data);
            Assert.Equal("Jorge", newWorker.Name);
        }

        // Method POST
        [Fact]
        public async Task PostWorkerReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var worker = new Worker { Name="Gabriel" };
            var data = await controller.Post(worker);

            var result = Assert.IsType<CreatedAtActionResult>(data.Result);
            var newworker = Assert.IsType<Worker>(result.Value);
            var workerName = (await controller.GetId(newworker.Id)).Value.Name;
            Assert.Equal("Gabriel", workerName);
        }

        // Method DELETE
        [Fact]
        public async Task DeleteWorkerReturn()
        {
            var controller = new WorkerController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.Delete(1);

            Assert.IsType<Worker>(data.Value);
            Assert.Equal("Pedro", data.Value.Name);
            var result = (await controller.GetId(1)).Result;
            Assert.IsType<NotFoundResult>(result);
        }
    }
}