namespace Play.Catalog.Service
{
    public class Dtos
    {

        public record ItemDto(Guid Id, string Name, string Description, Decimal Price, DateTimeOffset CreatedDate);

        public record CreateItemDto(string Name, string Description, Decimal Price);

        public record UpdateItemDto(string Name, string Description, Decimal Price);

    }
}
