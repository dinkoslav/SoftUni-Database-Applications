using System.Linq;

namespace _6._2.RatedMoviesByUser
{
    using _5.EFCodeFirstMovies;
    using System;
    using System.IO;
    using System.Web.Script.Serialization;

    class RatedMoviesByUser
    {
        static void Main()
        {
            var context = new MoviesContext();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var ratedMoviesByJmeyery = context.Users
                .Where(u => u.Username == "jmeyery")
                .Select(u => new
                {
                    username = u.Username,
                    ratedMovies = u.Ratings.Select(r => new
                    {
                        title = r.Movie.Title,
                        userRating = r.Stars,
                        averageRating = r.Movie.Ratings.Average(mr => mr.Stars)
                    })
                });

            var json = js.Serialize(ratedMoviesByJmeyery);
            File.WriteAllText(@"..\..\rated-movies-by-jmeyery.json", json);
        }
    }
}
