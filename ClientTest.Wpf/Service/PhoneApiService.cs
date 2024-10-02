using Newtonsoft.Json;
using ProductDB.Entitys;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace ClientTest.Wpf.Service
{
    internal class PhoneApiService : IPhoneApiService
    {
        private HttpClient _httpClient;

        public PhoneApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7164");
        }

        public async Task<bool> AddPhoneAsync(Phone phone)
        {
            var json = JsonConvert.SerializeObject(phone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Phone", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeletePhoneAsync(int phoneId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Phone/{phoneId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Phone> GetPhoneByIdAsync(int phoneId)
        {
            Phone phone = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Phone/{phoneId}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                phone = JsonConvert.DeserializeObject<Phone>(json);
            }

            return phone;
        }

        public async Task<ObservableCollection<Phone>> GetPhonesAsync()
        {
            ObservableCollection<Phone> phones = new ObservableCollection<Phone>();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Phone");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var phonesFromApi = JsonConvert.DeserializeObject<List<Phone>>(data);
                phones = new ObservableCollection<Phone>(phonesFromApi);
            }

            return phones;
        }

        public async Task<bool> UpdatePhoneAsync(Phone phone)
        {
            var json = JsonConvert.SerializeObject(phone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"api/Phone/{phone.Id}", content);

            return response.IsSuccessStatusCode;
        }
    }
}
