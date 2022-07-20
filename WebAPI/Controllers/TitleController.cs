using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly UserManagementDBContext _dbContext;
        public TitleController(UserManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("title")]
        [HttpGet]
        public async Task<IActionResult> GetTitles()
        {
            try
            {
                var titles = await (from title in _dbContext.Titles
                                    select title).ToListAsync();

                return Ok(titles);

            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }
    }
}
