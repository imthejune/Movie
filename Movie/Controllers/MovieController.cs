using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movie.Data;
using Movie.Models;
using System.IO;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDBContext _db;

        public MovieController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<MovieList> Movie = _db.Movies;
            return View(Movie);
        }

        public IActionResult addMovie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addMovie(MovieList model, IFormFile fileUpload)
        {
            if (fileUpload == null)
            {
                ModelState.AddModelError("errFileUpload", "กรุณาเพิ่มรูปภาพภาพยนต์");
                return View();
            }

            string pathImgMovie = "/images/";
            string pathSave = $"wwwroot{pathImgMovie}";
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }
            string extFile = Path.GetExtension(fileUpload.FileName);
            string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileUpload.CopyToAsync(fileStream);
            }

            DateTime dateNow = DateTime.Now;
            model.MovieImg = pathImgMovie + fileName;
            model.CreateDate = dateNow;
            model.ModifyDate = dateNow;
            _db.Movies.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 00)
            {
                return NotFound();
            }
            MovieList movie = _db.Movies.Where(x => x.MovieID.Equals(id)).FirstOrDefault();
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieList model, IFormFile fileUpload)
        {
            if (fileUpload == null)
            {
                DateTime dateNow = DateTime.Now;
                model.ModifyDate = dateNow;
                _db.Movies.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (fileUpload != null)
            {
                string pathImgMovie = "/images/";
                string pathSave = $"wwwroot{pathImgMovie}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }

                DateTime dateNow = DateTime.Now;
                model.MovieImg = pathImgMovie + fileName;
                model.ModifyDate = dateNow;
                _db.Movies.Update(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 00)
            {
                return NotFound();
            }
            var movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
