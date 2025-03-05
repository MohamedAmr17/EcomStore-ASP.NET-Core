using AutoMapper;
using Ecom.DTOS;
using Ecom.InterFaces.Ecom.InterFaces;
using Ecom.Models;
using Ecom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepo.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public IActionResult Register(UserCreateDto userCreateDto)
        {
            
            if (!_userRepo.IsUsernameUnique(userCreateDto.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return BadRequest(ModelState);
            }

            
            if (!_userRepo.IsEmailUnique(userCreateDto.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return BadRequest(ModelState);
            }

            // Map DTO to User entity
            var userEntity = _mapper.Map<User>(userCreateDto);

            // Hash the password
            userEntity.PasswordHash = PasswordHasher.HashPassword(userCreateDto.Password);

            var newUser = _userRepo.AddUser(userEntity);

            // Return the created user
            var userToReturn = _mapper.Map<UserDto>(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, userToReturn);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto loginDto)
        {
            // Find user by username
            var user = _userRepo.GetUserByUsername(loginDto.Username);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Verify password
            if (!PasswordHasher.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Return user info (in a real app, you'd return a JWT token here)
            var userToReturn = _mapper.Map<UserDto>(user);
            return Ok(new { user = userToReturn, message = "Login successful" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserCreateDto userUpdateDto)
        {
            var existingUser = _userRepo.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Check username uniqueness if it's changed
            if (existingUser.Username != userUpdateDto.Username &&
                !_userRepo.IsUsernameUnique(userUpdateDto.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return BadRequest(ModelState);
            }

            // Check email uniqueness if it's changed
            if (existingUser.Email != userUpdateDto.Email &&
                !_userRepo.IsEmailUnique(userUpdateDto.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return BadRequest(ModelState);
            }

            // Map and update fields
            var userEntity = _mapper.Map<User>(userUpdateDto);
            userEntity.Id = id;

            // Hash the password if provided
            if (!string.IsNullOrEmpty(userUpdateDto.Password))
            {
                userEntity.PasswordHash = PasswordHasher.HashPassword(userUpdateDto.Password);
            }

            var updatedUser = _userRepo.UpdateUser(id, userEntity);

            var userToReturn = _mapper.Map<UserDto>(updatedUser);
            return Ok(userToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userRepo.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userRepo.Delete(existingUser);
            return NoContent();
        }
    }
}