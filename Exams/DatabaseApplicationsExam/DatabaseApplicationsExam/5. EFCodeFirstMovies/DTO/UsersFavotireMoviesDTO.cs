using System.Collections.Generic;
namespace _5.EFCodeFirstMovies.DTO
{
    public class UsersFavotireMoviesDTO
    {
        private List<string> favouriteMovies = new List<string>();

        public UsersFavotireMoviesDTO()
        {
            this.favouriteMovies = new List<string>();
        }

        public string Username { get; set; }
        public List<string> FavouriteMovies { get; set; }
    }
}
