namespace _6._3.Top10FavouriteMovies
{
    using _5.EFCodeFirstMovies;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;

    class Top10FavouriteMovies
    {
        static void Main(string[] args)
        {
            var context = new MoviesContext();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var top10FavoriteMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Teen)
                .OrderByDescending(m => m.Users.Count())
                .ThenBy(m => m.Title)
                .Select(m => new
                {
                    isbn = m.Isbn,
                    title = m.Title,
                    favouritedBy = m.Users.Select(u => u.Username)
                })
                .Take(10);
                

            var json = js.Serialize(top10FavoriteMovies);
            File.WriteAllText(@"..\..\top-10-favourite-movies.json", json);
        }
    }
}
