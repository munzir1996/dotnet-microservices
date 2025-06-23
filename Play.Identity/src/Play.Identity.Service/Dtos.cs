using System.ComponentModel.DataAnnotations;

namespace Play.Identity.Service
{
    public class Dtos
    {

        public record UserDto(
            Guid Id,
            string Username,
            string Email,
            decimal Gil,
            DateTimeOffset? CreatedDate = null
        );

        public record UpdateUserDto(
            [Required][EmailAddress] string Email,
            [Range(0, 1000000)] decimal Gil
        );

    };
}
