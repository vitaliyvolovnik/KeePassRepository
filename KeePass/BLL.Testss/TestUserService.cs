using BLL.Services;
using DAL.Context;
using DAL.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Testss
{
    public class TestUserService
    {
        private UserService _userService;
        private UserRepository _userRepository;
        private CryptographyService _cryptographyService;

        [SetUp]
        public void SetUp()
        {

            var options = new DbContextOptionsBuilder<KeyPassContext>()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KeyPassTestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;")
                .Options;

            KeyPassContext context = new(options);
            _userRepository = new UserRepository(context);
            _cryptographyService = new CryptographyService("",new User());
            _userService = new UserService(_userRepository, _cryptographyService);
            _userRepository.DeleteAllAsync();
        }

        [Test]
        public async Task ChangePassword_ValidUserIdAndPassword_ReturnsUpdatedUser()
        {

            // Ensure no user is registered
            await _userRepository.DeleteAllAsync();


            // Arrange
            string newPassword = "newPassword";
            var user = new User { MasterPassword = "oldHashedPassword" };
            user = await _userRepository.CreateAsync(user);

            
            // Act
            var result = await _userService.ChangePassword(user.Id, newPassword);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(_cryptographyService.HashPassword(newPassword), result.MasterPassword);
        }

        [Test]
        public async Task LoginAsync_ValidPassword_ReturnsUserFromRepository()
        {
            // Ensure no user is registered
            await _userRepository.DeleteAllAsync();

            // Arrange
            string password = "password";
            var hashedPassword = _cryptographyService.HashPassword(password);
            var user = new User { MasterPassword = hashedPassword };
            await _userRepository.CreateAsync(user);

            

            // Act
            var result = await _userService.LoginAsync(password);

            // Assert
            //Assert.AreEqual(user, result);
        }

        [Test]
        public async Task RegisterAsync_FirstUser_ReturnsCreatedUser()
        {
            // Arrange
            string password = "password";

            // Ensure no user is registered
            await _userRepository.DeleteAllAsync();

            // Act
            var result = await _userService.RegisterAsync(password);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task RegisterAsync_NotFirstUser_ReturnsNull()
        {
            // Arrange
            string password = "password";
            var existingUser = new User { Id = 1, MasterPassword = "hashedPassword" };
            await _userRepository.CreateAsync(existingUser);

            // Act
            var result = await _userService.RegisterAsync(password);

            // Assert
            Assert.IsNull(result);
        }
    }
}