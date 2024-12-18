namespace Queue.Domain.Entites;

public class User
{
    public Guid Id { get; set; }
    public string Iin { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
}
