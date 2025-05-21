using Play.Inventory.Service.Entities;
using static Play.Inventory.Service.Dtos;

namespace Play.Inventory.Service
{
    public static class Extensions
    {

        public static InventoryItemsDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemsDto(item.CatalogItemId, name, description, item.Quantity, item.AcquiredDate);
        }

    }
}
