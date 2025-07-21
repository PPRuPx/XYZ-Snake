namespace XYZ_Snake;

public class ConsoleInput
{
    public interface IArrowListener
    {
        void OnArrowUp();
        void OnArrowDown();
        void OnArrowLeft();
        void OnArrowRight();
    }

    private List<IArrowListener> _arrowListeners = [];

    public void Subscribe(IArrowListener arrowListener)
    {
        _arrowListeners.Add(arrowListener);
    }

    public void Update()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey();
            switch (key.Key)
            {                   
                case ConsoleKey.UpArrow or ConsoleKey.W:
                    foreach (var al in _arrowListeners) al.OnArrowUp();
                    break;
                case ConsoleKey.DownArrow or ConsoleKey.S:
                    foreach (var al in _arrowListeners) al.OnArrowDown();
                    break;
                case ConsoleKey.LeftArrow or ConsoleKey.A:
                    foreach (var al in _arrowListeners) al.OnArrowLeft();
                    break;
                case ConsoleKey.RightArrow or ConsoleKey.D:
                    foreach (var al in _arrowListeners) al.OnArrowRight();
                    break;
            }
        }
    }
}