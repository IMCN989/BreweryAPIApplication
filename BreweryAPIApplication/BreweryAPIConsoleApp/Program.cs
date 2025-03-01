using BreweryAPIClassLibrary.DataAccess;
using BreweryAPIClassLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Step 1: Build IConfiguration manually
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Step 2: Create an instance of SqlDataAccess
        var sqlDataAccess = new SqlDataAccess(configuration);

        // Step 3: Create an instance of PeopleData
        var brewerData = new BrewerData(sqlDataAccess);

        // Step 4: Test PeopleData methods
        try
        {
            // Test GetEntries
            var entries = await brewerData.GetAllBeers();
            Console.WriteLine("All Beer Entries:");
            foreach (var entry in entries)
            {
                Console.WriteLine($"ID: {entry.Id}, Name: {entry.Name}, Price: {entry.Price}");
            }

            // Test InsertEntry
            var newEntry = new Beer { Name = "John", Price = 12345, BrewerId = 99 };
            await brewerData.AddBeer(newEntry);
            Console.WriteLine("New Beer entry inserted.");

            // Test GetEntry
            var singleEntry = await brewerData.GetBeerById(1); // Replace 1 with an actual ID
            if (singleEntry != null)
            {
                Console.WriteLine($"Retrieved Entry: ID={singleEntry.Id}, Name={singleEntry.Name}, Price={singleEntry.Price}, BrewerId={singleEntry.BrewerId}");
            }
            else
            {
                Console.WriteLine("Entry not found.");
            }

            // Test UpdateEntry
            if (singleEntry != null)
            {
                singleEntry.Name = "UpdatedBeerName";
                await brewerData.UpdateBeer(singleEntry);
                Console.WriteLine("Entry updated.");
            }

            // Test DeleteEntry
            await brewerData.DeleteBeer(1); // Replace 1 with an actual ID
            Console.WriteLine("Entry deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadLine();
    }
}
