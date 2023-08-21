using Microsoft.EntityFrameworkCore;
using Catalog.API.Data;
using Catalog.API.DTOs;

namespace Catalog.API.Endpoints
{
	public static class ItemEndpoints
	{
		public static void MapItemEndpoints(this WebApplication app)
		{
			var itemsRouteGroup = app.MapGroup("/items");

			itemsRouteGroup.MapGet("/", GetAllItems);
			itemsRouteGroup.MapGet("/{id}", GetItem);
			itemsRouteGroup.MapPost("/", CreateItem);
			itemsRouteGroup.MapPut("/{id}", UpdateItem);
			itemsRouteGroup.MapDelete("/{id}", DeleteItem);
		}

		private static async Task<IResult> GetAllItems(ApplicationDbContext db)
			=> TypedResults.Ok(await db.Items.Select(i => i.ToItemDto()).ToListAsync());

		private static async Task<IResult> GetItem(Guid id, ApplicationDbContext db)
			=> await db.Items.FindAsync(id)
					is Item item
					? TypedResults.Ok(item.ToItemDto())
					: TypedResults.NotFound();

		private static async Task<IResult> CreateItem(CreateItemDto inputItem, ApplicationDbContext db)
		{
			var item = new Item()
			{
				Name = inputItem.Name,
				Description = inputItem.Description,
				Price = inputItem.Price,
				CreatedDate = DateTimeOffset.Now
			};
			await db.Items.AddAsync(item);
			await db.SaveChangesAsync();
			return TypedResults.Created($"/items/{item.Id}", item.ToItemDto());
		}

		private static async Task<IResult> UpdateItem(Guid id, UpdateItemDto inputItem, ApplicationDbContext db)
		{
			var searchedItem = await db.Items.FindAsync(id);
			if (searchedItem is null) return TypedResults.NotFound();

			searchedItem.Name = inputItem.Name;
			searchedItem.Description = inputItem.Description;
			searchedItem.Price = inputItem.Price;
			searchedItem.UpdatedDate = DateTimeOffset.Now;

			await db.SaveChangesAsync();

			return TypedResults.NoContent();
		}

		private static async Task<IResult> DeleteItem(Guid id, ApplicationDbContext db)
		{
			var searchedItem = await db.Items.FindAsync(id);
			if (searchedItem is null) return TypedResults.NotFound();

			db.Items.Remove(searchedItem);
			await db.SaveChangesAsync();
			return TypedResults.NoContent();
		}
	}
}
