using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using System.Text.Json;

namespace Yelp_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var options = new RestClientOptions("https://api.yelp.com/v3/businesses/search?location=Boston&sort_by=best_match&limit=5");
            const string API_KEY = "UgxhmmHGcl7thxLmbLl_sPdLUDfDsKr7nah69IjkZ0dJQYlo9J1hII9qM5-8v5ZL6KE8Q8FW7mknEF1q-kE4WDu1KlKhbisS-Phk6RJeumCcHQMukQzsdY8QeMn8ZXYx";
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Authorization", $"Bearer {API_KEY}");
            request.AddHeader("accept", "application/json");


            // Making a GET request to the Yelp API
            var response = await client.GetAsync(request);

            // Checking if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Reading the response content
                var responseBody = response.Content;
                // Deserialize JSON response into an object representing the entire response
                YelpApiResponse yelpApiResponse = JsonSerializer.Deserialize<YelpApiResponse>(responseBody);

                // Access the list of restaurants from the YelpApiResponse object
                List<Restaurant> restaurants = yelpApiResponse.businesses;


                foreach (var restaurant in restaurants)
                {
                    Console.WriteLine($"Name: {restaurant.name}\n Url: {restaurant.url}\n\n");
                }
            }
            else
            {
                Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
            }

            // Dispose the HTTP client
            client.Dispose();

            Console.ReadLine();
        }
    }
    class YelpApiResponse
    {
        public List<Restaurant> businesses { get; set; }
    }
    class Restaurant
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

    }
}