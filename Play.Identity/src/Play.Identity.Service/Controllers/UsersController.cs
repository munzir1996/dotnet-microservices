using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Play.Identity.Service.Entities;
using static Duende.IdentityServer.IdentityServerConstants;
using static Play.Identity.Service.Dtos;

namespace Play.Identity.Service.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(Policy = LocalApi.PolicyName, Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var users = userManager.Users.ToList().Select(user => user.AsDto());

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return user.AsDto();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updateUserDto)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Email;
            user.Gil = updateUserDto.Gil;

            await userManager.UpdateAsync(user);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NoContent();
            }

            await userManager.DeleteAsync(user);

            return NoContent();
        }

    }
}
