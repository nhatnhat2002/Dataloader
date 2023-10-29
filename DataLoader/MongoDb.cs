using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLoader
{
    public class MongoDb
    {
        private IMongoCollection<Word> collection;

        public MongoDb(string databaseName = "DictionaryDB", string collectionName = "Words")
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            collection = database.GetCollection<Word>(collectionName);
        }

        public void InsertNewWordFromUserInput(string englishWord, string vietnameseTranslation, string example)
        {
            var word = new Word
            {
                English = englishWord,
                Vietnamese = vietnameseTranslation,
                Example = example
            };

            collection.InsertOne(word);
            Console.WriteLine($"Đã thêm từ '{englishWord}' vào từ điển.");
        }
        public Word SearchWordByEnglish(string englishWord)
        {
            var filter = Builders<Word>.Filter.Eq("English", englishWord);
            return collection.Find(filter).FirstOrDefault();
        }
    }
    public class Word
    {
        public string English { get; set; }
        public string Vietnamese { get; set; }
        public string Example { get; set; }

    }
}
