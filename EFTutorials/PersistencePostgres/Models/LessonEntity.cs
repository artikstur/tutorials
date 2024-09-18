using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistencePostgres.Models
{
    public class LessonEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LessonText { get; set; } = string.Empty;
        public Guid CourseId { get; set; } // по сути это айдишник курса
        public CourseEntity? Course { get; set; }
    }
}
