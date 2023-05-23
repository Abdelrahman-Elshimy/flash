using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlCash.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.OleDb;
using ExcelDataReader;

namespace FlCash.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionsController : Controller
    {
        private readonly DatabaseContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public QuestionsController(DatabaseContext context, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        // Import 
        private void GetQuestions(string fname)
        {

            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fname;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {

                while (reader.Read())
                {
                    var leave = reader.GetValue(5).ToString();
                    string image = reader?.GetValue(6)?.ToString();

                    if (reader.GetValue(5).ToString() == "Mode ( Easy, M, H)")
                    {

                    }
                    else
                    {
                        var questionId = _context.Categories.FirstOrDefault(x => x.Name == "Geo").Id;
                        long? levelId = _context.Levels.FirstOrDefault(x => x.Name == reader.GetValue(5).ToString()).Id;

                        if (levelId == null)
                        {
                            levelId = 1;
                        }

                        var question = new Question()
                        {
                            Name = reader.GetValue(0).ToString(),
                            Status = 2,
                            CategoryId = questionId,
                            LevelId = (long)levelId,
                            Image = image

                        };
                        _context.Questions.Add(question);
                        _context.SaveChanges();
                        var ans1 = new Answer
                        {
                            Name = reader?.GetValue(1)?.ToString(),
                            Status = true,
                            QuestionId = question.Id
                        };
                        _context.Answers.Add(ans1);
                        _context.SaveChanges();
                        var ans2 = new Answer
                        {
                            Name = reader?.GetValue(2)?.ToString(),
                            Status = false,
                            QuestionId = question.Id
                        };
                        _context.Answers.Add(ans2);
                        _context.SaveChanges();
                        var ans3 = new Answer
                        {
                            Name = reader?.GetValue(3)?.ToString(),
                            Status = false,
                            QuestionId = question.Id
                        };
                        _context.Answers.Add(ans3);
                        _context.SaveChanges();
                        var ans4 = new Answer
                        {
                            Name = reader?.GetValue(4)?.ToString(),
                            Status = false,
                            QuestionId = question.Id
                        };
                        _context.Answers.Add(ans4);
                        _context.SaveChanges();
                    }


                }


            }

        }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public ActionResult Import(IFormFile postedFile, [FromServices] IHostingEnvironment hostingEnvironment)

        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{postedFile.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                postedFile.CopyTo(fileStream);
                fileStream.Flush();
            }
            GetQuestions(postedFile.FileName);

            return RedirectToAction("Index");


        }



        // GET: Admin/Questions
        public async Task<IActionResult> Index()
        {
            //var q = _context.Questions;
            //foreach (var item in q)
            //{
            //    _context.Questions.Remove(item);
            //}
            //_context.SaveChanges();
            var databaseContext = _context.Questions.OrderByDescending(x => x.Id).Include(q => q.Category).Include(q => q.Level).Take(50);
            return View(await databaseContext.ToListAsync());
        }
        public async Task<IActionResult> Activate(long id)
        {
            var q = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (q != null)
            {
                q.Status = 2;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Active()
        {
            var qs = _context.Questions.ToList();
            foreach (var q in qs)
            {
                q.Status = 2;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Vote(long id)
        {
            var q = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (q != null)
            {
                q.Status = 1;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/Questions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Category)
                .Include(q => q.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Admin/Questions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            return View();
        }

        // POST: Admin/Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Questions",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    question.Image = ImageName;
                }
                question.Status = 2;
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", question.CategoryId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", question.LevelId);
            return View(question);
        }

        // GET: Admin/Questions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", question.CategoryId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", question.LevelId);
            return View(question);
        }

        // POST: Admin/Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Question question, IFormFile Image)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                        var path = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Questions",
                                    ImageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        question.Image = ImageName;
                    }
                    else
                    {
                        var q = _context.Questions.FirstOrDefault(x => x.Id == id);
                        question.Image = q.Image;
                    }
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", question.CategoryId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", question.LevelId);
            return View(question);
        }

        // GET: Admin/Questions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Category)
                .Include(q => q.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Admin/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(long id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
