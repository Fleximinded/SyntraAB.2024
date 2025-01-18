using Microsoft.AspNetCore.Mvc;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController :ControllerBase
    {
        IMenuService Service { get; init; } = default!;
        public MenuController(IMenuService service)
        {
            Service= service;
        }
        [HttpGet]
        public async Task<IEnumerable<MenuTopic>> GetTopics() => await Service.GetTopics();
    }
}
