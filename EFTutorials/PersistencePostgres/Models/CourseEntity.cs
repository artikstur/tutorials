using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PersistencePostgres.Models
{
    public class CourseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public List<LessonEntity> Lessons { get; set; } = new();
        public AuthorEntity Author { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CourseId { get; set; }
        public List<StudentEntity> Students { get; set; } = new();
    }
}
