namespace Domain.Entities;

public class TodoModel
{
    public TodoModel(string description)
    {
        Description = description;
        Id = Guid.NewGuid();
    }

    public TodoModel(Guid id, string description)
    {
        Id = id;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Description { get; private set; }
}

public class TodoViewModel
{
    public string? Description { get; set; }
    public bool ShouldCommit { get; set; } = true;
}
