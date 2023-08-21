namespace Catalog.API.DTOs;

public record CreateItemDto(string Name, string Description, decimal Price);

public record ItemDto(Guid Id, string Name, string? Description, decimal Price, DateTimeOffset CreatedDate, DateTimeOffset? UpdatedDate);

public record UpdateItemDto(string Name, string Description, decimal Price);