namespace BookstoreApplication.Services.DTOs;

public class BookSearchQuery
{
    public string? Title { get; set; }
    public DateTime? PublishedFrom { get; set; }
    public DateTime? PublishedTo { get; set; }
    public int? AuthorId { get; set; }
    public string? Author { get; set; }
    public DateTime? AuthorBirthFrom { get; set; }
    public DateTime? AuthorBirthTo { get; set; }
}