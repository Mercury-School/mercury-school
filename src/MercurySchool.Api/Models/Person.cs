namespace MercurySchool.Api.Models;

public record Person
{
    public int Id { get; init; }

    public string FirstName { get; init; }

    public string MiddleName { get; init; }

    public string LastName { get; init; }
}
