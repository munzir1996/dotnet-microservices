namespace Play.Inventory.Service
{
    public class Dtos
    {

        public record GrantItemsDto(Guid UserId, Guid CatalogItemId, int Quantity);
        public record InventoryItemsDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AccuiredDate);
        public record CatalogItemsDto(Guid Id, string Name, string Description);

    }
}
