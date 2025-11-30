using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elektrijaam_ORM_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TootajadController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TootajadController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetTootajad(int page = 1)
        {
            var url = $"https://reqres.in/api/users?page={page}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Viga töötajate API päringul");
            }

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}