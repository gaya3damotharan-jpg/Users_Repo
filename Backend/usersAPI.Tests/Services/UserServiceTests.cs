using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using usersAPI.Model;
using usersAPI.Services;

namespace usersAPI.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private DbContextOptions<AppDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" });
                context.Users.Add(new User { Id = 2, Name = "Jane", Age = 28, City = "Mumbai", State = "MH", Pin = "400001" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.GetAllUsersAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
                var users = result.ToList();
                Assert.That(users.Count, Is.EqualTo(2));
                Assert.That(users[0].Name, Is.EqualTo("John"));
                Assert.That(users[1].Name, Is.EqualTo("Jane"));
            }
        }

        [Test]
        public async Task GetUserByIdAsync_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.GetUserByIdAsync(1);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Name, Is.EqualTo("John"));
                Assert.That(result.Age, Is.EqualTo(30));
            }
        }

        [Test]
        public async Task GetUserByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.GetUserByIdAsync(999);

                // Assert
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public async Task CreateUserAsync_WithValidUser_ShouldAddUser()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var newUser = new User { Name = "Alice", Age = 25, City = "Bangalore", State = "KA", Pin = "560001" };

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.CreateUserAsync(newUser);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.GreaterThan(0));
                Assert.That(result.Name, Is.EqualTo("Alice"));
            }
        }

        [Test]
        public async Task UpdateUserAsync_WithValidId_ShouldUpdateUser()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);
                var updatedUser = new User { Id = 1, Name = "Jonathan", Age = 31, City = "Delhi", State = "DL", Pin = "110001" };

                // Act
                var result = await service.UpdateUserAsync(1, updatedUser);

                // Assert
                Assert.That(result, Is.True);
            }
        }

        [Test]
        public async Task UpdateUserAsync_WithMismatchedId_ShouldReturnFalse()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);
                var updatedUser = new User { Id = 2, Name = "Jonathan", Age = 31, City = "Delhi", State = "DL", Pin = "110001" };

                // Act
                var result = await service.UpdateUserAsync(1, updatedUser);

                // Assert
                Assert.That(result, Is.False);
            }
        }

        [Test]
        public async Task DeleteUserAsync_WithValidId_ShouldDeleteUser()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { Id = 1, Name = "John", Age = 30, City = "Chennai", State = "TN", Pin = "600001" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.DeleteUserAsync(1);

                // Assert
                Assert.That(result, Is.True);
                Assert.That(context.Users.Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public async Task DeleteUserAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);

                // Act
                var result = await service.DeleteUserAsync(999);

                // Assert
                Assert.That(result, Is.False);
            }
        }
    }
}