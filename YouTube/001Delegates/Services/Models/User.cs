namespace _001Delegates.Services.Models;

public class User
{
    public int Id { get; init; }
    public int Age { get; set; }
    public string Name { get; set; }
}

public class UserViewModel
{
    public int Age { get; set; }
    public string Name { get; set; }
}