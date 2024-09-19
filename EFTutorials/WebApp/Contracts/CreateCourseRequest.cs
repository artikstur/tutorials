namespace WebApp.Contracts
{
    public record class CreateCourseRequest(string Title, string Description, decimal Price);
}
