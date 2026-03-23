using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using usersAPI.Model;
using usersAPI.Services;
using usersAPI;

namespace usersAPI.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Test]
        public async Task Get_ShouldReturnOkResultWithUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" },
                new User { Id = 2, Name = "Jane", Age = 28, City = "Mumbai", State = "MH", Pin = "400001" }
            };

            _mockUserService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var returnedUsers = okResult!.Value as IEnumerable<User>;
            Assert.That(returnedUsers, Is.Not.Null);
            Assert.That(returnedUsers!.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Get_ById_WithValidId_ShouldReturnOkResultWithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" };
            _mockUserService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value!.Name, Is.EqualTo("John"));
        }

        [Test]
        public async Task Get_ById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetUserByIdAsync(999)).ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Get(999);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Post_WithValidUser_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var newUser = new User { Name = "Alice", Age = 25, City = "Bangalore", State = "KA", Pin = "560001" };
            var createdUser = new User { Id = 3, Name = "Alice", Age = 25, City = "Bangalore", State = "KA", Pin = "560001" };

            _mockUserService.Setup(s => s.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(createdUser);

            // Act
            var result = await _controller.Post(newUser);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult!.ActionName, Is.EqualTo(nameof(_controller.Get)));
            Assert.That(((User)createdResult.Value!).Id, Is.EqualTo(createdUser.Id));
        }

        [Test]
        public async Task Put_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "Jonathan", Age = 31, City = "Delhi", State = "DL", Pin = "110001" };
            _mockUserService.Setup(s => s.UpdateUserAsync(1, It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Put(1, updatedUser);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Put_WithMismatchedId_ShouldReturnBadRequest()
        {
            // Arrange
            var updatedUser = new User { Id = 2, Name = "Jonathan", Age = 31, City = "Delhi", State = "DL", Pin = "110001" };
            _mockUserService.Setup(s => s.UpdateUserAsync(1, It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Put(1, updatedUser);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task Delete_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            _mockUserService.Setup(s => s.DeleteUserAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Delete_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockUserService.Setup(s => s.DeleteUserAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}