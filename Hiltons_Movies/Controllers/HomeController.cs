using Hiltons_Movies.Models;
using Hiltons_Movies.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hiltons_Movies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MoviesContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, MoviesContext con)
        {
            _logger = logger;
            context = con;
        }

        public void RemoveMovieFromDb(int MovieId)
        {
            var movie = context.Movies.FirstOrDefault(x => x.MovieId == MovieId);
            context.Movies.Remove(movie);
            context.SaveChanges();
        }

        public IActionResult Index()
        {
            return View();
        }

        //This is what is returned when the form page is first loaded or when not all the required information is entered into the form
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movies addMovie)
        {
            //Verifying the form when submit button is hit, if all required data entered, go to confirmation page, else go return to form page with required
            //information posted under the form
            if (ModelState.IsValid)
            {
                context.Movies.Add(addMovie);
                context.SaveChanges();
                return View("Confirmation", addMovie);
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteMovie(int MovieId)
        {
            RemoveMovieFromDb(MovieId);
            return View("MovieList", new MoviesViewModel
            {
                Movies = context.Movies
            });
        }

        [HttpGet]
        public IActionResult EditMovie(int MovieId)
        {
            var film = context.Movies.Where(x => x.MovieId == MovieId).FirstOrDefault();
            return View(film);
        }

        [HttpPost]
        public IActionResult EditMovie(Movies movies, int MovieId)
        {
            var film = context.Movies.Where(x => x.MovieId == MovieId).FirstOrDefault();
            film.Category = movies.Category;
            film.Title = movies.Title;
            film.Year = movies.Year;
            film.Director = movies.Director;
            film.Rating = movies.Rating;
            film.Edited = movies.Edited;
            film.LentTo = movies.LentTo;
            film.Notes = movies.Notes;

            context.SaveChanges();

            return View("MovieList", new MoviesViewModel
            {
                Movies = context.Movies
            });
        }

        //Returning the podcast page, when the podcast navigation bar is clicked, or typed into URL
        public IActionResult MyPodcast()
        {
            return View();
        }

        //Returning the movie liest page, when the movie list navigation bar is clicked, or typed into URL
        public IActionResult MovieList()
        {
            //Returning the view for Movie List with the Data from the Model of TempStorage, which is currently saving move list until app is killed
            return View(new MoviesViewModel
            {
                Movies = context.Movies
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
