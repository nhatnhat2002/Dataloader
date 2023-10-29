using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;


namespace DataLoader
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public abstract class FileProcessor
    {
        protected string FilePath;

        public FileProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public abstract void ReadFile();
        public abstract void WriteToFile();
    }

    public class CsvFileProcessor : FileProcessor
    {
        public CsvFileProcessor(string filePath) : base(filePath)
        {
        }

        public override void ReadFile()
        {
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<Word>();
                foreach (var record in records)
                {
                    Console.WriteLine($"English: {record.English}, Vietnamese: {record.Vietnamese}, Example: {record.Example}");
                }
            }
        }

        public override void WriteToFile() // cái này để test
        {
            var data = new List<Word>
            {
                new Word { English = "Mango", Vietnamese = "Xoai", Example = "Tao an xoai" }
            };

            using (var writer = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(data);
            }
        }
    }
    public class JsonFileProcessor : FileProcessor
    {
        public JsonFileProcessor(string filePath) : base(filePath)
        {
        }

        public override void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string json = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<Word>(json);
                    Console.WriteLine("Data from JSON:");
                    Console.WriteLine("English: " + data.English);
                    Console.WriteLine("Vietnamese: " + data.Vietnamese);
                    Console.WriteLine("Example: " + data.Example);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading the JSON file: " + e.Message);
            }
        }
        public override void WriteToFile()
        {
            var data = new Word 
            {
                
            };

            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.Write(json);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error writing the JSON file: " + e.Message);
            }
        }
    }
    public class TextFileProcessor : FileProcessor
    {
        public TextFileProcessor(string filePath) : base(filePath)
        {
        }

        public override void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string text = reader.ReadToEnd();
                    Console.WriteLine("Text from file: " + text);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading the file: " + e.Message);
            }
        }

        public override void WriteToFile()
        {
            string text = "This is a sample text.";
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.Write(text);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error writing the file: " + e.Message);
            }
        }
    }
}
