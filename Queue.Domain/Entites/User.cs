namespace Queue.Domain.Entites;

public class User
{
    public User(Guid id, string iin, string firstName, string lastName, DateTime creationDate, bool isDeleted)
    {
        Id = id;
        Iin = iin;
        FirstName = firstName;
        LastName = lastName;
        CreationDate = creationDate;
        IsDeleted = isDeleted;
    }
    public User()
    {
        
    }

    public Guid Id { get; set; }
    public string Iin { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

}
