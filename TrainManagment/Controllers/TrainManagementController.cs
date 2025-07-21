using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainManagment.Data.Entities;
using TrainManagment.DTOs;
using TrainManagment.Interfaces;

namespace TrainManagment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainManagementController(
        IItemRepository _itemRepository,
        IMapper _mapper
        ) : ControllerBase
    {
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _itemRepository.GetItemAsync(id);

            if (item == null) return NotFound();

            return item;
        }

        [HttpGet]
        [Route("getbynumber/{uniquenumber}")]
        public async Task<ActionResult<Item>> GetItem(string uniquenumber)
        {
            var item = await _itemRepository.GetItemAsync(uniquenumber);

            if (item == null) return NotFound();

            return item;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateItem([FromBody] ItemDTO dto)
        {
            if (await _itemRepository.IsExistsAsync(dto.UniqueNumber))
                return BadRequest($"Item with unique number - {dto.UniqueNumber} already exists");

            var recordToAdd = _mapper.Map<Item>(dto);

            await _itemRepository.CreateItemAsync(recordToAdd);
            if (await _itemRepository.SaveChangesAsync()) return Created();

            return BadRequest("Failed to create user");
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateItem([FromBody] ItemIdDTO dto)
        {
            var item = await _itemRepository.GetItemAsync(dto.Id);

            if (item == null)
                return BadRequest($"Item with unique number - {dto.UniqueNumber} does not exists");

            _mapper.Map(dto, item);

            if (!item.CanAssignQuantity)
                await _itemRepository.RemoveQualityAsync(item.Id);

            if (await _itemRepository.SaveChangesAsync()) return Ok();

            return BadRequest("Failed to update user");
        }

        [HttpPatch]
        [Route("setquality")]
        public async Task<ActionResult> SetQuality([FromBody]QuantityDTO dto)
        {
            var item = await _itemRepository.GetItemAsync(dto.Id);

            if (item == null)
                return BadRequest($"Item with Id - {dto.Id} does not exists");

            if (!item.CanAssignQuantity)
                return BadRequest("Cannot change quantity for this item");

            await _itemRepository.AddQualityAsync(item.Id, dto.Quantity);

            if (await _itemRepository.SaveChangesAsync()) return Ok();

            return BadRequest("Failed to change quantity");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemAsync(id);

            if (item == null)
                return BadRequest($"Item with Id - {id} does not exists");

            _itemRepository.RemoveItem(item);

            if (await _itemRepository.SaveChangesAsync()) return Ok();

            return BadRequest("Failed to delete user");
        }
    }
}
