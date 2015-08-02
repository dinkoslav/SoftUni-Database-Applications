using System.Linq;

namespace _5.EFCodeFirstMovies
{
    class EFCodeFirstMovies
    {
        static void Main()
        {
            var context = new MoviesContext();

            context.Countries.Count();
        }
    }
}
