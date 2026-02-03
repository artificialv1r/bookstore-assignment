namespace BookstoreApplication.Services.DTOs;

public class BookDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishedDate{get; set;}
    public string ISBN { get; set; }
    public int AuthorId { get; set; }
    public string Author { get; set; }
    public int PublisherId { get; set; }
    public string Publisher { get; set; }
}