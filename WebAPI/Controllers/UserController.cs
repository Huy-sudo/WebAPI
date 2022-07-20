using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly UserManagementDBContext _dbContext;

        public UserController(UserManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //add User
        [Route("user")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        //getUsers
        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await (from user in _dbContext.Users
                                   join title in _dbContext.Titles on user.Title equals title.TitleId 
                                   select new
                                   {
                                       firstName = user.FirstName,
                                       lastName = user.LastName,
                                       dateOfBirth = user.DateOfBirth,
                                       email = user.Email,
                                       createdDate = user.CreatedDate,
                                       gender = user.Gender,
                                       img = user.Img,
                                       organization = user.Organization,
                                       title = title.TitleName,
                                       id = user.UserId,
                                       status = user.Status
                                   }
                                   ).ToListAsync();
                return Ok(users);
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        //getUser
        [Route("user/{user_id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int user_id)
        {
            try
            {
                var userDetail = await (from user in _dbContext.Users
                                        join title in _dbContext.Titles on user.Title equals title.TitleId
                                        where user.UserId == user_id
                                        select user).FirstAsync();

                return Ok(userDetail);
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }

        //updateUser
        [Route("user/{user_id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int user_id, User updateUser)
        {

            try
            {
                var usr = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId == user_id);

                if (usr == null)
                    return NotFound();

                if (updateUser.FirstName != null)
                    usr.FirstName = updateUser.FirstName;

                if (updateUser.LastName != null)
                    usr.LastName = updateUser.LastName;

                if (updateUser.Title != null)
                    usr.Title = updateUser.Title;

                if (updateUser.DateOfBirth != null)
                    usr.DateOfBirth = updateUser.DateOfBirth;

                if (updateUser.Gender != null)
                    usr.Gender = updateUser.Gender;

                if (updateUser.Organization != null)
                    usr.Organization = updateUser.Organization;

                _dbContext.SaveChangesAsync();
                return Ok(usr);
            }
            catch (Exception error) {  
                return StatusCode(500, error.Message); 
            }
        }

        //deleteUser
        [Route("user/{user_id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int user_id)
        {
            try
            {
                var userToDelete = await (from user in _dbContext.Users
                                          where user.UserId == user_id
                                          select user).FirstOrDefaultAsync();
                if (userToDelete == null)
                    return NotFound($"User with id = {user_id} not found!");
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();

                return Ok(userToDelete);
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }
    }
}
