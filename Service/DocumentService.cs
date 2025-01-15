using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HandHeldbB.Model;

namespace HandHeldbB.Service
{
    public class DocumentService
    {
        private readonly HttpClient _httpClient;

        public DocumentService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyIsImtpZCI6InoxcnNZSEhKOS04bWdndDRIc1p1OEJLa0JQdyJ9.eyJhdWQiOiJodHRwczovL2FwaS5idXNpbmVzc2NlbnRyYWwuZHluYW1pY3MuY29tIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvZmJiMTRhMDMtNDdjOC00ODIwLWExMzgtMzEwNWMxY2ZhNWMxLyIsImlhdCI6MTczNjg2NjMxNiwibmJmIjoxNzM2ODY2MzE2LCJleHAiOjE3MzY4NzAyMTYsImFpbyI6ImsyUmdZRERjVW4zRmMxc2VrNTJHamtIVkJ6MEdBQT09IiwiYXBwaWQiOiI5NzRlOTc3OC1lMzZiLTQ3NzQtYjFhYS1hYmUzNzJkNDFhYmMiLCJhcHBpZGFjciI6IjEiLCJpZHAiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mYmIxNGEwMy00N2M4LTQ4MjAtYTEzOC0zMTA1YzFjZmE1YzEvIiwiaWR0eXAiOiJhcHAiLCJvaWQiOiIzY2VhZDE0OC0xYjdkLTQxNmMtYmY3MS00OWMzMTIxNGMyM2IiLCJyaCI6IjEuQVZRQUEwcXgtOGhISUVpaE9ERUZ3Yy1sd1QzdmJabHNzMU5CaGdlbV9Ud0J1Si1pQUFCVUFBLiIsInJvbGVzIjpbIkF1dG9tYXRpb24uUmVhZFdyaXRlLkFsbCIsImFwcF9hY2Nlc3MiLCJBZG1pbkNlbnRlci5SZWFkV3JpdGUuQWxsIiwiQVBJLlJlYWRXcml0ZS5BbGwiXSwic3ViIjoiM2NlYWQxNDgtMWI3ZC00MTZjLWJmNzEtNDljMzEyMTRjMjNiIiwidGlkIjoiZmJiMTRhMDMtNDdjOC00ODIwLWExMzgtMzEwNWMxY2ZhNWMxIiwidXRpIjoicXp6UWJfTlVzVS1DUVZyQUFyNUZBQSIsInZlciI6IjEuMCIsInhtc19pZHJlbCI6IjcgMTAifQ.mHdee0d9kuKSsVe93lwz-Gpl4f9Oa3PeY0pokIzWGlIBHy9QzKbq3re3UTcE7mJRfo8fye-_Xo-xWbUlhIDsN63twAei8M9tnVws37MfsQlhpaTnl_KYnLothmmO9uRwOOo4u1cT1iA0phg8YcufgWj8rAP4hR_hFAmCOBvSS9CFlL5LuHSc9-dPH6KAfaJZ1rwpFnywQvEEfer9ZJSDpKxRzYDOXZWrOvR7hyskGMqm54Rk-JpnnSTCn_0R_1g71hSNKOd-utdCOE8q8WXGQRbQuecs_njNaMwXuATVRiQfWOGk_HmS3_49RwuOoYiUbpRw-2IY2mJMnwpjLnpd6A");
        }

        public async Task<List<DocumentModel>> GetDocumentsAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("https://api.businesscentral.dynamics.com/v2.0/fbb14a03-47c8-4820-a138-3105c1cfa5c1/AMDEV/ODataV4/WarehouseReceiptService_GetWhseReceiptDocument?Company=My%20Company", null);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Raw JSON Response: " + jsonResponse);

                    // Deserialize Root Object
                    using var document = JsonDocument.Parse(jsonResponse);
                    var valueString = document.RootElement.GetProperty("value").GetString();

                    if (!string.IsNullOrEmpty(valueString))
                    {
                        // Deserialize Nested JSON Array
                        var documents = JsonSerializer.Deserialize<List<DocumentModel>>(valueString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return documents ?? new List<DocumentModel>();
                    }

                    return new List<DocumentModel>();
                }
                else
                {
                    Console.WriteLine($"API Error: {response.StatusCode}");
                    return new List<DocumentModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching documents: {ex.Message}");
                return new List<DocumentModel>();
            }
        }
    }

}
