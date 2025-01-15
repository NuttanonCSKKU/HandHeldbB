using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandHeldbB.Model;
using HandHeldbB.Page;
using HandHeldbB.Service;

namespace HandHeldbB.PageModel
{
    public partial class WhseReceiptListPageModel : ObservableObject
    {
 
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private ObservableCollection<DocumentModel> products;

        [ObservableProperty]
        private DocumentModel productSelected; 

        public IAsyncRelayCommand GetDocumentsCommand { get; }
        public IAsyncRelayCommand OpenWhseReceiptPageCommand { get; }

        public WhseReceiptListPageModel()
        {
            _httpClient = new HttpClient();
            Products = new ObservableCollection<DocumentModel>();
            GetDocumentsCommand = new AsyncRelayCommand(GetDocumentsAsync);
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyIsImtpZCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyJ9.eyJhdWQiOiJodHRwczovL2FwaS5idXNpbmVzc2NlbnRyYWwuZHluYW1pY3MuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvZmJiMTRhMDMtNDdjOC00ODIwLWExMzgtMzEwNWMxY2ZhNWMxLyIsImlhdCI6MTczNjkyMDQwOSwibmJmIjoxNzM2OTIwNDA5LCJleHAiOjE3MzY5MjQzMDksImFpbyI6ImsyUmdZRGpBTHl1cG1PU1JzdnJwSTQ3TlM1OXdBZ0E9IiwiYXBwaWQiOiI5NzRlOTc3OC1lMzZiLTQ3NzQtYjFhYS1hYmUzNzJkNDFhYmMiLCJhcHBpZGFjciI6IjEiLCJpZHAiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mYmIxNGEwMy00N2M4LTQ4MjAtYTEzOC0zMTA1YzFjZmE1YzEvIiwiaWR0eXAiOiJhcHAiLCJvaWQiOiIzY2VhZDE0OC0xYjdkLTQxNmMtYmY3MS00OWMzMTIxNGMyM2IiLCJyaCI6IjEuQVZRQUEwcXgtOGhISUVpaE9ERUZ3Yy1sd1QzdmJabHNzMU5CaGdlbV9Ud0J1Si1pQUFCVUFBLiIsInJvbGVzIjpbIkF1dG9tYXRpb24uUmVhZFdyaXRlLkFsbCIsImFwcF9hY2Nlc3MiLCJBZG1pbkNlbnRlci5SZWFkV3JpdGUuQWxsIiwiQVBJLlJlYWRXcml0ZS5BbGwiXSwic3ViIjoiM2NlYWQxNDgtMWI3ZC00MTZjLWJmNzEtNDljMzEyMTRjMjNiIiwidGlkIjoiZmJiMTRhMDMtNDdjOC00ODIwLWExMzgtMzEwNWMxY2ZhNWMxIiwidXRpIjoiR1JiRy1vSHBOVVdJSDczRXdra19BQSIsInZlciI6IjEuMCIsInhtc19pZHJlbCI6IjMwIDcifQ.LMJcIoJrZNN1790_x-8x44d7crqt4K7pebkrmZlUBzGEzip4RCDiav2E0_TJ6Lng490IkIBRJ7biKlA6pDME8FQqgnKWpCoudkpcZfHspczHjfCoXx0LdqS09XQBMPAt4zLtBphNEVa85Fxqpu-ETGVrwBcQts8vpKrAnNmf5N94ZLAO-Jcvg8elz2BxGFSjHQyVlSNv7XzTdQshWLTN5rIx8zOP3NnMXfqfLwfvDNnMYHjSRSTNRTrOa8in6y36jg5s1zacyLxLti4cvq0QCS-DV9yxxSFTzEYtjbwEUrGDeXmjPkcsieyyxGKri6J1Ly9SOt2y2DQ6KbmnMO2wNg");
        }


        private async Task GetDocumentsAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("https://api.businesscentral.dynamics.com/v2.0/fbb14a03-47c8-4820-a138-3105c1cfa5c1/AMDEV/ODataV4/WarehouseReceiptService_GetWhseReceiptDocument?Company=My%20Company", null);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Raw JSON Response: " + jsonResponse);

                    using var document = JsonDocument.Parse(jsonResponse);
                    var valueString = document.RootElement.GetProperty("value").GetString();

                    if (!string.IsNullOrEmpty(valueString))
                    {
                        var documents = JsonSerializer.Deserialize<List<DocumentModel>>(valueString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        Products.Clear();

                        foreach (var doc in documents)
                        {
                            Products.Add(doc);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading documents: {ex.Message}");
            }
        }

        //private async Task OpenWhseReceiptPageAsync()
        //{
        //    if (ProductSelected != null)
        //    {
        //        // นำทางไปยังหน้า WhseReceiptPage และส่งข้อมูล productSelected
        //        await Shell.Current.GoToAsync("WhseReceiptPage,product={ProductSelected}");
        //    }
        //}





    }
}
