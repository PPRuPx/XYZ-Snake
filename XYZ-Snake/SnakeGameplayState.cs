namespace XYZ_Snake;

public enum SnakeDir
{
    Up,
    Down,
    Left,
    Right
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

    private char _snakeSymbol = '■';

    public int FieldWidth;
    public int FieldHeight;

    public void SetDirection(SnakeDir dir)
    {
        _currentDir = dir;
    }

    private Cell ShiftTo(Cell pos, SnakeDir dir)
    {
        return dir switch
        {
            SnakeDir.Up    => new Cell(pos.X, pos.Y - 1),
            SnakeDir.Down  => new Cell(pos.X, pos.Y + 1),
            SnakeDir.Left  => new Cell(pos.X - 1, pos.Y),
            SnakeDir.Right => new Cell(pos.X + 1, pos.Y),
            _ => pos
        };
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
    }

    public override void Reset()
    {
        _body.Clear();

        var middleY = FieldHeight / 2;
        var middleX = FieldWidth / 2;
        _body.Add(new Cell(middleX, middleY));

        _timeToMove = 0f;
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        foreach (var cell in _body)
            renderer.SetPixel(cell.X, cell.Y, _snakeSymbol, 2);
    }
}