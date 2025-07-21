namespace XYZ_Snake;

public class SnakeGameLogic : BaseGameLogic
{
    private SnakeGameplayState _gameplayState = new SnakeGameplayState();

    public override ConsoleColor[] CreatePalette()
    {
        return
        [
            ConsoleColor.White,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Blue,
        ];
    }

    public override void Update(float deltaTime)
    {
        if (_currentState != _gameplayState) 
            GotoGameplay();
    }

    public override void OnArrowUp()
    {
        if (_currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Up);
    }

    public override void OnArrowDown()
    {
        if (_currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Down);
    }

    public override void OnArrowLeft()
    {
        if (_currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Left);
    }

    public override void OnArrowRight()
    {
        if (_currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Right);
    }

    public void GotoGameplay()
    {
        _gameplayState.FieldWidth = _screenWidth;
        _gameplayState.FieldHeight = _screenHeight;
        ChangeState(_gameplayState);
        _gameplayState.Reset();
    }
}