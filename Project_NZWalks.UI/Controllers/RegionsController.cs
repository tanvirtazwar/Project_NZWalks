using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.UI.Models;
using Project_NZWalks.UI.Models.DTO;
using System.Text.Json;

namespace Project_NZWalks.UI.Controllers
{
    public class RegionsController(IHttpClientFactory httpClientFactory) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = [];

            try
            {
                // Get All Regions From Web Api
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7192/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                IEnumerable<RegionDto>? httpResponse = await httpResponseMessage
                    .Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                response.AddRange(httpResponse!);

            }
            catch (Exception ex)
            {
                //Log the exception
                throw new Exception(ex.Message);
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7192/api/regions"),
                Content = new StringContent(JsonSerializer
                .Serialize(addRegionViewModel), System.Text.Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();  
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>
                ($"https://localhost:7192/api/regions/{id}");
            if(response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto regionDto)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7192/api/regions/{regionDto.Id}"),
                Content = new StringContent(JsonSerializer
                .Serialize(regionDto), System.Text.Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response != null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto regionDto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client
                    .DeleteAsync($"https://localhost:7192/api/regions/{regionDto.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {
                return View("Edit");
            }
        }

        
    }
}
