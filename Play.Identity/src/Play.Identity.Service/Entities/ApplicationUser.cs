
using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Play.Identity.Service.Entities
{
    //[CollectionName("Users")]
    public class ApplicationUser : MongoUser<Guid>
    {
        public ApplicationUser() : base()
        {
            // Base constructor handles initialization
        }

        public ApplicationUser(string userName, string email) 
        {
            // Constructor for creating users with basic info
        }

        [BsonElement("gil")]  // Optional: Custom field name in MongoDB
        public decimal Gil { get; set; }

        // You can add more custom properties as needed
        // Example:
        // public DateTime LastLogin { get; set; }
        // public string ProfileImageUrl { get; set; }


    }
}
