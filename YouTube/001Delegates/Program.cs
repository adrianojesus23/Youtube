
using _001Delegates.Services;
using _001Delegates.Services.Models;


//002Singleton

//*****************************************************************

var lisUser = new List<User>()
{
    new User() { Id = 1, Age = 12, Name = "Jesus" },
    new User() { Id = 1, Age = 2, Name = "Gabriel" },
    new User() { Id = 1, Age = 22, Name = "Laura" },
};
//*******************************************************************

Show();

 void Show()
{
   if(lisUser.IsNull(x=> x.Name.Contains("Manuel")))
   {
       Console.WriteLine("Não existe Manuel!!");
   }
   
   
   if(lisUser.IsNull(x=> x.Name.Contains("Jesus")))
   {
       var users = lisUser.Select(x => x.ToViewModel());
       foreach (var user in users)
       {
           Console.WriteLine($"{user.Name}: {user.Age}");
       }
       Console.WriteLine("Existe Jesus!!");
   }
}

var calculate = new Calculate();

Sum  delSum = calculate.Somar;

int result = delSum(1, 2);

Console.WriteLine(result); 

//****************************************************************************

Console.WriteLine(calculate.FuncSum(1,6));

calculate.ActionShow("My first message");

var numbers = new List<int>{1,2,4,5,5,6};

Func<int, bool> Filter = n => n % 2 == 0;

var numbersFilter = numbers.Where(Filter);

bool isFilter = numbersFilter.IsEmpty();

Console.WriteLine(isFilter? "Is empty": "No empty");


public static class EnumerableFilter
{
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        return !source.Any();
    }
}