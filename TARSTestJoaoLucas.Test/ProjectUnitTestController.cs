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
    public class ProjectUnitTestController
    {
        private IUnitOfWork repository;
        private ILogger<ProjectController> logger = Mock.Of<ILogger<ProjectController>>();

        public static DbContextOptions<AppDbContext> dbContextOption { get; }

        static ProjectUnitTestController()
        {
            dbContextOption = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        public ProjectUnitTestController()
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
        public async Task GetProjectsReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.Get(new PaginationParameters { PageNumber = 1, PageSize = 2 });

            Assert.IsType<PagedList<Project>>(data.Value);
            Assert.True(controller.Response.Headers["X-Pagination"] != String.Empty);
        }

        // Method GET ID
        [Fact]
        public async Task GetIdProjectReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.GetId(1);

            Assert.IsType<Project>(data.Value);
            Assert.Equal("TARS T1", data.Value.Name);
        }

        // Method GET Worker
        [Fact]
        public async Task GetWorkerProjectReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.GetWorker(4);

            Assert.IsType<Worker>(data.Value);
            Assert.Equal("Samuel", data.Value.Name);
        }

        // Method PUT
        [Fact]
        public async Task PutProjectReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var project = (await controller.GetId(1)).Value;
            project.Name = "TARS T1 New";
            var data = await controller.Put(1, project);
            var newProject = (await controller.GetId(1)).Value;

            Assert.IsType<NoContentResult>(data);
            Assert.Equal("TARS T1 New", newProject.Name);
        }

        // Method POST
        [Fact]
        public async Task PostProjectReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var project = new Project { Name = "TARS T5", Description= "TARS Test", WorkedHours=1, WorkerId=3 };
            var data = await controller.Post(project);

            var result = Assert.IsType<CreatedAtActionResult>(data.Result);
            var newproject = Assert.IsType<Project>(result.Value);
            var projectName = (await controller.GetId(newproject.Id)).Value.Name;
            Assert.Equal("TARS T5", projectName);
        }

        // Method DELETE
        [Fact]
        public async Task DeleteProjectReturn()
        {
            var controller = new ProjectController(logger, repository);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var data = await controller.Delete(1);

            Assert.IsType<Project>(data.Value);
            Assert.Equal("TARS T1", data.Value.Name);
            var result = (await controller.GetId(1)).Result;
            Assert.IsType<NotFoundResult>(result);
        }
    }
}