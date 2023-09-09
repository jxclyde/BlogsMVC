using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        { 
            var tag = bloggieDbContext.Tags.FirstOrDefault(t => t.Id == id);

            if(tag != null )
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName 
            };

            var existingTag = bloggieDbContext.Tags.Find(tag.Id);

            if (existingTag != null)
            { 
                existingTag.Name = tag.Name;    
                existingTag.DisplayName = tag.DisplayName;

                bloggieDbContext.SaveChanges();

                //As for the future -> here we will show success message
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            //As for the future -> here we will show error message
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }


    }


}
