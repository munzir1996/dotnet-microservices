using AspNetCore.Identity.Mongo.Model;
//using AspNetCore.Identity.MongoDbCore.Models;
//using MongoDbGenericRepository.Attributes;

namespace Play.Identity.Service.Entities
{
    //[CollectionName("Roles")]
    public class ApplicationRole : MongoRole<Guid>
    {

        public ApplicationRole() : base()
        {
            // Base constructor handles initialization
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            // Constructor for creating named roles
        }

        // Add any custom properties you need below
        // Example:
        // public string Description { get; set; }
        // public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
