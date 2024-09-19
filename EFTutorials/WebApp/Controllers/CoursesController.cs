using Microsoft.AspNetCore.Mvc;
using PersistencePostgres.Repositories;
using WebApp.Contracts;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController: ControllerBase
    {
        private readonly CoursesRepository? _coursesRepository ;

        public CoursesController(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _coursesRepository.Get();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest course)
        {
            await _coursesRepository.Add(Guid.NewGuid(), Guid.NewGuid(), course.Title, course.Description, course.Price);
            return Ok();
        }
    }
}
