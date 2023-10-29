using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;


// đã connect được Mongo,txt,csv,json
// 
namespace DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;
            string choice = Console.ReadLine(); ;
            KMP kmp = new KMP();
            string pattern = Console.ReadLine();
            KMP.LinkedList result = new KMP.LinkedList();

            switch (choice)
            {
                case "1":
                    string textFilePath = "a.txt";
                    string text = File.ReadAllText(textFilePath);
                    kmp.KMPSearch(pattern, text, result);
                    break;
                case "2": 
                     filePath = "a.json";
                    FileProcessor jsonFileProcessor = new JsonFileProcessor(filePath);
                    jsonFileProcessor.ReadFile();
                    jsonFileProcessor.WriteToFile();
                    break;

                case "3": // chỗ này đọc được file csv rồi nhma mỗi lần nó ghi thì nó sẽ ghi đè lên dữ liệu cũ, lười fix quá sáng dậy fix
                    filePath = "a.csv";
                    FileProcessor csvFileProcessor = new CsvFileProcessor(filePath);
                    csvFileProcessor.ReadFile();
                    csvFileProcessor.WriteToFile();
                    break;


                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    return;
            }

            if (result.length() > 0)
            {
                Console.WriteLine("Kết quả tìm kiếm trong tệp:");
                KMP.Node node = result.head;
                while (node != null)
                {
                    Console.WriteLine("Tìm thấy tại vị trí: " + node.element);
                    node = node.next;
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy mẫu trong tệp.");
            }

            Console.ReadLine();
        }
    }
}