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

    private Random _random = new Random();
    
    private SnakeDir _currentDir = SnakeDir.Right;
    private float _timeToMove;
    private List<Cell> _body = [];
    private Cell _apple;
    
    private char _snakeSymbol = '■';
    private char _appleSymbol = '0';

    public int fieldWidth;
    public int fieldHeight;
    public int level;
    
    public bool gameOver { get; private set; }
    public bool hasWon { get; private set; }

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
        if (gameOver || _timeToMove > 0f)
            return;

        _timeToMove = 1f / ((level + 4f) * 2);
        var head = _body[0];
        var nextCell = ShiftTo(head, _currentDir);

        if (nextCell.Equals(_apple))
        {
            _body.Insert(0, _apple);
            hasWon = _body.Count >= (level + 3) * 2;
            GenerateApple();
            return;
        }

        if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= fieldWidth || nextCell.Y >= fieldHeight)
        {
            gameOver = true;
            return;
        }
        
        _body.RemoveAt(_body.Count - 1);
        _body.Insert(0, nextCell);
    }

    public override void Reset()
    {
        _body.Clear();
        gameOver = false;
        hasWon = false;

        var middleY = fieldHeight / 2;
        var middleX = fieldWidth / 2;
        _body.Add(new Cell(middleX, middleY));
        _apple = new Cell(middleX + 5, middleY); 
        _currentDir = SnakeDir.Right;
        
        _timeToMove = 0f;
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        renderer.DrawString($"Level: {level}", 0, 0, ConsoleColor.White);
        renderer.DrawString($"Score: {_body.Count - 1}", 0, 1, ConsoleColor.White);
        
        foreach (var cell in _body)
            renderer.SetPixel(cell.X, cell.Y, _snakeSymbol, 2);

        renderer.SetPixel(_apple.X, _apple.Y, _appleSymbol, 1);
    }

    public override bool IsDone() => hasWon || gameOver;

    private void GenerateApple()
    {
        Cell cell;
        cell.X = _random.Next(fieldWidth);
        cell.Y = _random.Next(fieldHeight);

        if (_body[0].Equals(cell))
        {
            if (cell.Y > fieldHeight / 2)
                cell.Y--;
            else
                cell.Y++;
        }

        _apple = cell;
    }
}