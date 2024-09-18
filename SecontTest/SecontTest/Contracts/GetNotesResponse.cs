namespace SecontTest.Contracts;

// здесь только те данные, которые мы хотим вернуть, лучше разбивать отдельно на DTO и Response
public record GetNotesResponse(List<NoteDTO> notes);

//здесь только те данные, которые мы хотим вернуть фронтенду
public record NoteDTO(Guid Id, string Title, string Description, DateTime CreatedAt);