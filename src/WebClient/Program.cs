using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WebClient
{
    static class Program
    {
private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        
        static async Task Main(string[] args)
        {
            if (args.Length == 0)        
            {
                Console.WriteLine("Where is arguments?.");
                return;
            }
            string id = args[0];

            HttpResponseMessage response = await client.GetAsync("customers/" + id);
            if (response.IsSuccessStatusCode)
            {
                string dataStr = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Customer>(dataStr);
                Console.WriteLine($"Given Client ID: {data.id}, FirstName: {data.firstname}, LastName: {data.lastname}");

                var randomCustomer = RandomCustomer();
                var jsonContent = JsonSerializer.Serialize(randomCustomer);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                response = await client.PostAsync("customers", content);

                if (response.IsSuccessStatusCode)
                {
                    dataStr = await response.Content.ReadAsStringAsync();
                    data = JsonSerializer.Deserialize<Customer>(dataStr);
                    Console.WriteLine($"Client created successfuly with ID: {data.id}");

                    response = await client.GetAsync("customers/" + data.id);

                    if (response.IsSuccessStatusCode)
                    {
                        dataStr = await response.Content.ReadAsStringAsync();
                        data = JsonSerializer.Deserialize<Customer>(dataStr);
                        Console.WriteLine($"Given Client ID: {data.id}, FirstName: {data.firstname}, LastName: {data.lastname}");

                    }
                }
                else
                {
                    Console.WriteLine($"Failed with status code: {response.StatusCode}");
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            string[] firstNames = { "John", "Jane", "Michael", "Sarah", "David", "Emily" };
            string[] lastNames = { "Smith", "Doe", "Johnson", "Brown", "Davis", "Wilson" };

            Random rand = new Random();

            return new CustomerCreateRequest
            {
                Firstname = firstNames[rand.Next(firstNames.Length)],
                Lastname = lastNames[rand.Next(lastNames.Length)]
            };
        }
    }
}