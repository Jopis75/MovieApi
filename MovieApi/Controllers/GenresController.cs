using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(IMapper mapper) : ControllerBase
    {
    }
}
