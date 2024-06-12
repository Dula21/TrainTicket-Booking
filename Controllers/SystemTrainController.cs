using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TrainTicketBookingUI.Models;
using System.Net;
using System.Text;
using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;



namespace TrainTicketBookingUI.Controllers
{
    public class SystemTrainController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7076/api");
        private readonly HttpClient _client;
        public SystemTrainController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TrainViewModel> trainlist = new List<TrainViewModel>();
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/SystemTrain/GetTrain");

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    trainlist = JsonConvert.DeserializeObject<List<TrainViewModel>>(data);
                }

                return View(trainlist);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TrainViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/SystemTrain/CreateTrain", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Train Added !.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {

            try
            {
                TrainViewModel train = new TrainViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/SystemTrain/GetTrain/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    train = JsonConvert.DeserializeObject<TrainViewModel>(data);
                }
                return View(train);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }
        [HttpPost]
        public IActionResult Edit(TrainViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/SystemTrain/EditTrain/edit/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Train details updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to update train details.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(long id)
        {
            try
            {
                TrainViewModel train = new TrainViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/SystemTrain/GetTrainById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    train = JsonConvert.DeserializeObject<TrainViewModel>(data);

                }
                return View(train);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/SystemTrain/DeleteTrain/delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Train details deleted successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchTrain(SearchViewModel model)
        {
            try
            {
                // Make sure _client is initialized and configured properly
                // Adjust the URL as necessary based on your API endpoint
                var response = await _client.PostAsJsonAsync("/SystemTrain/SearchTrains/Search", model);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    var searchData = await response.Content.ReadAsStringAsync();
                    var searchResults = JsonConvert.DeserializeObject<List<SearchViewModel>>(searchData);
                    return View("SearchResults", searchResults);
                }
                else
                {
                    TempData["errorMessage"] = $"Failed to retrieve train details. Status code: {response.StatusCode}";
                    return View("Index"); // Or return some other view
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        
        [HttpGet]
        public IActionResult Results()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchResults(SearchViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/SystemTrain/SearchResults/Search", content).Result;

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Train details updated successfully.";
                    return View("SearchResults");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to update train details.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
