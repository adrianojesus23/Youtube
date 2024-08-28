using _001Delegates.Services.Models;

namespace _001Delegates.Services;

public static class Extensions
{
    public static UserViewModel ToViewModel(this User user)
    {
        if (user is null)
           return default;

        return new UserViewModel()
        {
            Age = user.Age,
            Name = user.Name
        };
    }



    public static bool IsNull<T>(this ICollection<T> sources, Func<T, bool> isNull)
    {
        return sources.Any(isNull);
    }
}