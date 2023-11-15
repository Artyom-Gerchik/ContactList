namespace ContactList.Models;

public class EditModalModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? MobilePhone { get; set; }
    public string? JobTitle { get; set; }
    public string? BirthDate { get; set; }
}