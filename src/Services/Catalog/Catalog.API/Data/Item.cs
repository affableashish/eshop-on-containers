using Catalog.API.DTOs;

namespace Catalog.API.Data;

public class Item
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public DateTimeOffset? UpdatedDate { get; set; }

	public ItemDto ToItemDto()
	{
		return new ItemDto(Id, Name, Description, Price, CreatedDate, UpdatedDate);
	}
}
