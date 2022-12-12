using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Entity;

namespace Q2.Pages.Movies
{
    public class Producer_MovieModel : PageModel
    {
        PE_PRN_Fall22B1Context context = new PE_PRN_Fall22B1Context();

        public void OnGet(int producerId)
        {
            if(producerId == 0)
            {
                List<Movie> movies = context.Movies.Include(m => m.Genres).Include(m => m.Director).Include(m => m.Producer).ToList();
                List<Producer> producers = context.Producers.ToList();

                ViewData["movies"] = movies;
                ViewData["producers"] = producers;
                ViewData["producerId"] = producerId;
            }
            else
            {
                List<Movie> movies = context.Movies.Include(m => m.Genres).Include(m => m.Director).Include(m => m.Producer).Where(m => m.ProducerId == producerId).ToList();
                List<Producer> producers = context.Producers.ToList();

                ViewData["movies"] = movies;
                ViewData["producers"] = producers;
                ViewData["producerId"] = producerId;
            }
        }

        public void OnPostDelete(int producerId, int movieId)
        {
            Movie movie = context.Movies.Include(m => m.Stars).Include(m => m.Genres).FirstOrDefault(m => m.Id == movieId);

            movie.Stars.Clear();
            movie.Genres.Clear();
            context.Movies.Remove(movie);
            context.SaveChanges();

            List<Movie> movies = new List<Movie>();

            if (producerId == 0)
            {
                movies = context.Movies.Include(m => m.Genres).Include(m => m.Director).Include(m => m.Producer).ToList();
            }
            else
            {
                movies = context.Movies.Include(m => m.Genres).Include(m => m.Director).Include(m => m.Producer).Where(m => m.ProducerId == producerId).ToList();
            }
                
            List<Producer> producers = context.Producers.ToList();

            ViewData["movies"] = movies;
            ViewData["producers"] = producers;
            ViewData["producerId"] = producerId;
        }
    }
}
