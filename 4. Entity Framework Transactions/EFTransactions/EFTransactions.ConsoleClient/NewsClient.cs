namespace EFTransactions.ConsoleClient
{
    using EFTransactions.Data;
    using System;
    using System.Linq;

    class NewsClient
    {
        static void Main(string[] args)
        {
            var context = new EFTransactionsContext();
            var allNews = context.News.Select(n => new
            {
                Content = n.Content
            });

            foreach (var news in allNews)
            {
                Console.WriteLine("Text from DB: " + news.Content);
            }

            bool error = false;
            Console.Write("Enter the corrected text: ");

            while (!error)
            {
                string newNews = Console.ReadLine();
                try
                {
                    var oldNews = context.News.Find(1);
                    oldNews.Content = newNews;
                    context.SaveChanges();
                    error = true;
                    Console.WriteLine("Changes successfully saved in the DB.");
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException("There is no old news.");
                    error = false;
                }
                catch (Exception)
                {
                    throw new Exception("Conflict! Text from DB: EF 7 Beta To Be Delayed. Enter the corrected text:");
                    error = false;
                }
            }
        }
    }
}
