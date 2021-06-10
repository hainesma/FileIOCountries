using System;
using System.Collections.Generic;
using System.IO;

namespace FileIOCountries
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // First we'll make a country class
            // Then we'll make a text file
            // Then we'll try to read in the text file
            // And convert our country.txt into objects
            Console.WriteLine("Hello World!");

            // This is an absolute filepath. It only works on my computer
            // string filePath = @"C:\Users\Stonefish\source\repos\FileIOCountries\FileIOCountries\Countries.txt";
            // Relative filepaths operate relative to the project
            // In order to use a relative path, place the file in \bin\Debug\netcoreapp3.1
            // /bin is technically where the project is running from
            string filePath = @"Countries.txt";
            StreamReader reader = new StreamReader(filePath);
            // Read to end grabs the whole file
            // In C# this is the preferred way of reading files
            string output = reader.ReadToEnd();

            Console.WriteLine(output);
            string[] lines = output.Split('\n');
            List<Country> countries = new List<Country>();

            // Convert each line into a country object
            foreach(string line in lines)
            {
                Country con = ConvertToCountry(line);
                if (con != null)
                {
                    countries.Add(con);
                }
            }

            //print each country name to prove conversion worked
            foreach(Country c in countries)
            {
                Console.WriteLine(c.Name);
            }
            reader.Close();
            AddCountry();
            
        }

        public static void AddCountry()
        {
            string filePath = @"Countries.txt";
            Country c = new Country();
            Console.WriteLine("Please input a name");
            c.Name = Console.ReadLine();

            Console.WriteLine("Please input an official language");
            c.OfficialLang = Console.ReadLine();

            Console.WriteLine("Please input a population");
            c.Population = int.Parse(Console.ReadLine());

            string line = CountryToString(c);
            Console.WriteLine(line);

            StreamReader reader = new StreamReader(filePath);
            string original = reader.ReadToEnd();
            reader.Close();

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(original);
            writer.WriteLine(line);
            writer.Close();
        }

        // This takes a country object and makes it into a string
        public static string CountryToString(Country c)
        {
            string output = $"{c.Name}, {c.OfficialLang}, {c.Population}";
            return output;
        }

        // This takes a string from our file and makes it into an object
        // Now we want to cut up our string into objects
        // We'll make a conversion method
        public static Country ConvertToCountry(string line)
        {
            string[] properties = line.Split(',');
            Country c = new Country();

            if (properties.Length == 3)
            {
                c.Name = properties[0];
                c.OfficialLang = properties[1];
                c.Population = int.Parse(properties[2]);

                return c;
            }
            else
            {
                return null;
            }
        }
    }
}
