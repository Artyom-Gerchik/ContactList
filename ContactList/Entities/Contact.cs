namespace ContactList.Entities;

/// <summary>
/// Contact entity
/// </summary>
public class Contact
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? MobilePhone { get; set; }
    public string? JobTitle { get; set; }
    public DateOnly BirthDate { get; set; }
}