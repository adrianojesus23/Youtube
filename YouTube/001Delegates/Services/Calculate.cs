namespace _001Delegates.Services;

public delegate int Sum(int positionX, int positionY);
public class Calculate
{
    public int Somar(int positionX, int positionY) => positionY + positionX;

    public Func<int, int, int> FuncSum = (a, b) => a + b;

    public Action<string> ActionShow = msg => Console.WriteLine(msg);
}