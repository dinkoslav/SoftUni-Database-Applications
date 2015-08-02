namespace _5.EFCodeFirstMovies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<Movie> favoriteMovies;
        private ICollection<Rating> ratings;

        public User()
        {
            this.favoriteMovies = new HashSet<Movie>();
            this.ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<Movie> FavoriteMovies
        {
            get { return this.favoriteMovies; }
            set { this.favoriteMovies = value; }
        }
    }
}
