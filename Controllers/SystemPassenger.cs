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



namespace TrainTicketBookingUI.Controllers
{
    public class SystemPassenger : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7076/api");
        private readonly HttpClient _client;
        public SystemPassenger()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<PassengerViewModel> passengerlist = new List<PassengerViewModel>();
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/SystemPassenger/GetPassenger");

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    passengerlist = JsonConvert.DeserializeObject<List<PassengerViewModel>>(data);
                }

                return View(passengerlist);
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
        public IActionResult Create(PassengerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/SystemPassenger/CreatePassenger/create", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Passenger Added !.";
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
                PassengerViewModel train = new PassengerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/SystemPassenger/EditPassenger/edit" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    train = JsonConvert.DeserializeObject<PassengerViewModel>(data);
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
        public IActionResult Edit(PassengerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/SystemPassenger/EditPassenger/edit/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Passenger details updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to update Passenger details.";
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
                PassengerViewModel train = new PassengerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/SystemPassenger/DeletePassenger/delete" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    train = JsonConvert.DeserializeObject<PassengerViewModel>(data);

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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/SystemPassenger/DeletePassenger/delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Passenger details deleted successfully.";
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
    }
}