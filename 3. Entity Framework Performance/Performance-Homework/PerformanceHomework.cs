using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace PerformanceHomework
{
    class PerformanceHomework
    {
        static void Main()
        {
            var contex = new AdsEntities();

            //Problem 1.Show Data from Related Tables

            foreach (var ad in contex.Ads)
            {
                Console.WriteLine("Title: {0}, status: {1}, category: {2}, town: {3}, user: {4}",
                    ad.Title,
                    ad.AdStatus.Status,
                    ad.CategoryId != null ? ad.Category.Name : "None",
                    ad.TownId != null ? ad.Town.Name : "None",
                    ad.AspNetUser.Name);
            }

            foreach (var ad in contex.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AspNetUser))
            {
                Console.WriteLine("Title: {0}, status: {1}, category: {2}, town: {3}, user: {4}",
                    ad.Title,
                    ad.AdStatus.Status,
                    ad.CategoryId != null ? ad.Category.Name : "None",
                    ad.TownId != null ? ad.Town.Name : "None",
                    ad.AspNetUser.Name);
            }

            //Problem 2.Play with ToList()

            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (int i = 0; i < 10; i++)
            {

            var ads2 = contex.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .OrderBy(a => a.Date)
                .Select(a => new
                    {
                        Title = a.Title,
                        Category = a.CategoryId != null ? a.Category.Name : "None",
                        Town = a.TownId != null ? a.Town.Name : "None"
                    })
                .ToList();

                Console.WriteLine(sw.Elapsed);
                sw.Restart();
            }

            for (int i = 0; i < 10; i++)
            {

                var ads3 = contex.Ads
                    .Where(a => a.AdStatus.Status == "Published")
                    .OrderBy(a => a.Date)
                    .Select(a => new
                        {
                            Title = a.Title,
                            Category = a.CategoryId != null ? a.Category.Name : "None",
                            Town = a.TownId != null ? a.Town.Name : "None"
                        })
                    .ToList();

                Console.WriteLine(sw.Elapsed);
                sw.Restart();
            }

            //Problem 3.Select Everything vs. Select Certain Columns

            for (int i = 0; i < 10; i++)
            {
                foreach (var ad in contex.Ads)
                {
                    Console.WriteLine(ad.Title);
                }

                Console.WriteLine(sw.Elapsed);
                sw.Restart();
            }

            var ads5 = contex.Ads.Select(a => a.Title);
            for (int i = 0; i < 10; i++)
            {
                foreach (var ad in ads5)
                {
                    Console.WriteLine(ad);
                }

                Console.WriteLine(sw.Elapsed);
                sw.Restart();
            }
           
        }
    }
}
