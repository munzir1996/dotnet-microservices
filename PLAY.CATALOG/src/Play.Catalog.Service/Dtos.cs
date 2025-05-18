﻿using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service
{
    public class Dtos
    {

        public record ItemDto(Guid Id, string Name, string Description, Decimal Price, DateTimeOffset CreatedDate);

        public record CreateItemDto([Required] string Name, string Description, [Range(0, 1000)] Decimal Price);

        public record UpdateItemDto([Required] string Name, string Description, [Range(0, 1000)] Decimal Price);

    }
}
