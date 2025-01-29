using Microsoft.AspNetCore.Mvc;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;
using System.Collections.Generic;

namespace Syntra.Frituurtje.Wasm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController :ControllerBase
    {
        IMenuService Service { get; init; } = default!;
        ILogger<MenuController> Log { get; init; } = default!;
        public MenuController(IMenuService service,ILogger<MenuController> log)
        {
            Service= service;
            Log = log;
        }
        [HttpGet]
        public async Task<IEnumerable<MenuTopic>> GetTopics() => await Service.GetTopics();
        [HttpGet("items/{topicId}")]
        public async Task<IEnumerable<MenuItem>> GetTopicItems(string topicId) {

            var result =await Service.GetTopicItems(topicId);
            return result;
        }
        [HttpPut("item/set")]
        public async Task<IActionResult> SetItem([FromBody] MenuItem selectedItem)
        {
            try
            {
                await Service.SetItem(selectedItem);
                return Ok();
            } catch(Exception ex)
            {
                Log.LogError(ex, "Error setting item");
                return BadRequest($"Error: {ex.ToString()}"); // Don't do this in production!!!!!!!!
            }
        }

        [HttpPut("topic/set")]
        public async Task<IActionResult> SetTopic([FromBody] MenuTopic topic)
        {
            return Ok();
        }
        [HttpPut("dummy/set")]
        public async Task<IActionResult> SetDummy([FromBody] DummyModel dummyText)
        {
            return Ok();
        }
    }
}
