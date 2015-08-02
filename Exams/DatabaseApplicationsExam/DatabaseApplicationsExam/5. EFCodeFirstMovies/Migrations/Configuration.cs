namespace _5.EFCodeFirstMovies.Migrations
{
    using _5.EFCodeFirstMovies.DTO;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MoviesContext context)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (!context.Countries.Any())
            {
                var countries = jsSerializer.Deserialize<List<Country>>(File.ReadAllText(@"../../countries.json"));
                foreach (var country in countries)
                {
                    context.Countries.Add(country);
                }

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = jsSerializer.Deserialize<List<UserDTO>>(File.ReadAllText(@"../../users.json"));
                foreach (var user in users)
                {
                    User newUser = new User(){ Username = user.Username };
                    if (user.Age != null)
                    {
                        newUser.Age = user.Age;
                    }

                    if (user.Email != null)
                    {
                        newUser.Email = user.Email;
                    }

                    if (user.Country != null)
                    {
                        newUser.Country = context.Countries.FirstOrDefault(c => c.Name == user.Country);
                    }

                    context.Users.Add(newUser);
                }

                context.SaveChanges();
            }

            if (!context.Movies.Any())
            {
                var movies = jsSerializer.Deserialize<List<Movie>>(File.ReadAllText(@"../../movies.json"));
                foreach (var movie in movies)
                {
                    context.Movies.Add(movie);
                }

                context.SaveChanges();

                var userFavMovies = jsSerializer.Deserialize<List<UsersFavotireMoviesDTO>>(File.ReadAllText(@"../../users-and-favourite-movies.json"));
                foreach (var userFavMovie in userFavMovies)
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == userFavMovie.Username);
                    foreach (var movieIsbn in userFavMovie.FavouriteMovies)
                    {
                        user.FavoriteMovies.Add(context.Movies.FirstOrDefault(m => m.Isbn == movieIsbn));
                    }
                }

                context.SaveChanges();
            }

            if (!context.Ratings.Any())
            {
                var ratings = jsSerializer.Deserialize<List<RatingDTO>>(File.ReadAllText(@"../../movie-ratings.json"));
                foreach (var rating in ratings)
                {
                    Rating newRating = new Rating()
                    {
                        User = context.Users.FirstOrDefault(u => u.Username == rating.User),
                        Movie = context.Movies.FirstOrDefault(m => m.Isbn == rating.Movie),
                        Stars = rating.Rating
                    };

                    context.Ratings.Add(newRating);
                }

                context.SaveChanges();
            }
        }
    }
}
