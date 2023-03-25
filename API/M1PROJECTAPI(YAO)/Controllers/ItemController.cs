using M1PROJECTAPI_YAO_.Context;
using M1PROJECTAPI_YAO_.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M1PROJECTAPI_YAO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemContext itemDbContext;
        public ItemController(ItemContext itemDbContext)
        {
            this.itemDbContext = itemDbContext;
        }
        //Get All Items
        [HttpGet("")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await itemDbContext.Items.ToListAsync();
            return Ok(items);
        }
        //Get single Item
        [HttpGet("GetItem/{id}")]
        [ActionName("GetItem")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var item = await itemDbContext.Items.FirstOrDefaultAsync(x => x.id == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound("Item Not Found");
        }

        //Add item

        [HttpPost("Add")]

        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            await itemDbContext.Items.AddAsync(item);
            await itemDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.id }, item);
        }

        //updating a item
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] Item item)
        {
            var existingItem = await itemDbContext.Items.FirstOrDefaultAsync(x => x.id == id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Brand = item.Brand;
                existingItem.Code = item.Code;
                existingItem.UnitPrice = item.UnitPrice;
                existingItem.DateAdded = item.DateAdded;
                await itemDbContext.SaveChangesAsync();
                return Ok(existingItem);
            }
            return NotFound("Card Not Found ");
        }


        //delete a item
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var existingItem = await itemDbContext.Items.FirstOrDefaultAsync(x => x.id == id);
            if (existingItem != null)
            {
                itemDbContext.Remove(existingItem);
                await itemDbContext.SaveChangesAsync();
                return Ok(existingItem);
            }
            return NotFound("Card Not Found ");
        }
    }
}
