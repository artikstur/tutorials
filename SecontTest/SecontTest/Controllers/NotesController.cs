using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecontTest.Contracts;
using SecontTest.DataAccess;
using SecontTest.Models;

namespace SecontTest.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NotesController : Controller
    {
        private readonly NotesDbContext _dbContext;
        public NotesController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        // по факту мы могли передать сразу в Create просто аргументы, но хорошей практикой будет, создать новый класс
        public async Task<IActionResult> Create([FromBody]CreateNoteRequest request, CancellationToken ct) 
        // FromBody - указывает на то, что аргумент должен быть привязан к телу запроса
        {
            var note = new Note(request.Title, request.Description);

            await _dbContext.Notes.AddAsync(note, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetNotesRequest request, CancellationToken ct)
        {
            var notesQuery = _dbContext.Notes
                .Where(n => !string.IsNullOrWhiteSpace(request.Search) &&
                            n.Title.ToLower().Contains(request.Search.ToLower()));

            var selectedKey = GetSelectorKey(request.SortItem?.ToLower());

            if (request.SortOrder == "desc")
            {
                notesQuery = notesQuery.OrderByDescending(selectedKey);
            }
            else
            {
                notesQuery = notesQuery.OrderBy(selectedKey);
            }

            var noteDtos = await notesQuery.Select(n => new NoteDTO(n.Id, n.Title, n.Description, n.CreatedAt)).ToListAsync(cancellationToken: ct);

            return Ok(new GetNotesResponse(noteDtos));
        }   

        private Expression<Func<Note, object>> GetSelectorKey(string sortItem)
        {
            switch (sortItem)
            {
                case "date":
                    Expression<Func<Note, object>> selectorDateKey = note => note.CreatedAt;
                    return selectorDateKey;
                case "title":
                    Expression<Func<Note, object>> selectorTitleKey = note => note.Title;
                    return selectorTitleKey;
                default:
                    return note => note.Id;
            }  
        }
    }
}
