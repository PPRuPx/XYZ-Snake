namespace XYZ_Snake;

public enum SnakeDir
{
    Up, Down, Left, Right
}

public class SnakeGameplayState : BaseGameState
{
    private struct Cell(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }

    private SnakeDir _currentDir = SnakeDir.Right;
    private float _timeToMove;
    private List<Cell> _body = new();

    public void SetDirection(SnakeDir dir)
    {
        _currentDir = dir;
    }

    private Cell ShiftTo(Cell pos, SnakeDir dir)
    {
        switch (dir)
        {
            case SnakeDir.Up:
                return new Cell(pos.X, pos.Y + 1);
            case SnakeDir.Down:
                return new Cell(pos.X, pos.Y - 1);
            case SnakeDir.Left:
                return new Cell(pos.X - 1, pos.Y);
            case SnakeDir.Right:
                return new Cell(pos.X + 1, pos.Y);
        }

        return pos;
    }

    public override void Update(float deltaTime)
    {
        _timeToMove -= deltaTime;
        if (_timeToMove > 0f)
            return;

        _timeToMove = 1f / 5;
        var head = _body[0];
        var nextCell = ShiftTo(head, _currentDir);          

        _body.RemoveAt(_body.Count - 1);
        _body.Insert(0, nextCell);

        Console.WriteLine($"X:{_body[0].X}; Y:{_body[0].Y}");
    }

    public override void Reset()
    {
        _body.Clear();                  
        _currentDir = SnakeDir.Left;
        _body.Add(new Cell(0, 0));
        _timeToMove = 0f;
    }
}