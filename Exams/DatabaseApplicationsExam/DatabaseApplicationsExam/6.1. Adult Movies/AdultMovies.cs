namespace _6._1.Adult_Movies
{
    using _5.EFCodeFirstMovies;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;

    class AdultMovies
    {
        static void Main()
        {
            var context = new MoviesContext();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var adultMovies = context.Movies
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Ratings.Select(r => r.Stars).Count())
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.Ratings.Select(r => r.Stars).Count()
                });

            var json = js.Serialize(adultMovies);
            File.WriteAllText(@"..\..\adult-movies.json", json);
        }
    }
}
