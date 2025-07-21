namespace XYZ_Snake;

public class SnakeGameLogic : BaseGameLogic
{
    private SnakeGameplayState _gameplayState = new SnakeGameplayState();
    private ShowTextState _showTextState = new ShowTextState(2);

    private bool _newGamePending = false;
    private int _currLevel = 0;

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
        if (currentState != null && !currentState.IsDone())
            return;
        
        if (currentState == null || currentState == _gameplayState && !_gameplayState.gameOver)
            GotoNextLevel();
        else if (currentState == _gameplayState && _gameplayState.gameOver)
            GotoGameOver();
        else if (currentState != _gameplayState && _newGamePending)
            GotoNextLevel();
        else if (currentState != _gameplayState && !_newGamePending)
            GotoGameplay();
    }

    public override void OnArrowUp()
    {
        if (currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Up);
    }

    public override void OnArrowDown()
    {
        if (currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Down);
    }

    public override void OnArrowLeft()
    {
        if (currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Left);
    }

    public override void OnArrowRight()
    {
        if (currentState != _gameplayState) 
            return;
        
        _gameplayState.SetDirection(SnakeDir.Right);
    }

    private void GotoGameplay()
    {
        _gameplayState.level = _currLevel;
        _gameplayState.fieldWidth = screenWidth;
        _gameplayState.fieldHeight = screenHeight;
        ChangeState(_gameplayState);
        _gameplayState.Reset();
    }
    
    private void GotoGameOver()
    {
        _currLevel = 0;
        _newGamePending = true;
        _showTextState.text = "Game Over!";
        ChangeState(_showTextState);
    }
    
    private void GotoNextLevel()
    {
        _currLevel++;
        _newGamePending = false;
        _showTextState.text = $"Level {_currLevel}";
        ChangeState(_showTextState);
    }
}