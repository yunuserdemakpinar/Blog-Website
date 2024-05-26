using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Blog_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();

        [HttpPost("GenerateTitle")]
        public async Task<IActionResult> GenerateTitle([FromBody] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title is required");
            }

            string apiUrl = "https://api-inference.huggingface.co/models/EleutherAI/gpt-neo-2.7B";
            string apiKey = "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";

            var requestBody = new
            {
                inputs = title,
                parameters = new
                {
                    max_length = 140,
                    temperature = 0.5,
                }
            };

            var requestContent = new StringContent(
                JObject.FromObject(requestBody).ToString(),
                Encoding.UTF8,
                "application/json"
            );

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.PostAsync(apiUrl, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            string generatedContent;

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JArray.Parse(responseContent);
                generatedContent = jsonResponse[0]["generated_text"].ToString();
            }
            else
            {
                generatedContent = "AI not working";
            }


            return Ok(new { content = generatedContent });
        }
    }
}
