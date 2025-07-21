namespace XYZ_Snake;

public abstract class BaseGameLogic : ConsoleInput.IArrowListener
{
    protected BaseGameState? _currentState;
    
    protected float _time;
    protected int _screenWidth;
    protected int _screenHeight;
    
    public void InitializeInput(ConsoleInput consoleInput)
    {
        consoleInput.Subscribe(this);
    }

    public void ChangeState(BaseGameState gameState)
    {
        _currentState?.Reset();
        _currentState = gameState;
    }

    public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
    {
        _time += deltaTime;
        _screenWidth = renderer.width;
        _screenHeight = renderer.height;
        
        _currentState?.Update(deltaTime);
        _currentState?.Draw(renderer);
        
        Update(deltaTime);
    }

    public abstract ConsoleColor[] CreatePalette();
    
    public abstract void Update(float deltaTime);

    public abstract void OnArrowUp();
    public abstract void OnArrowDown();
    public abstract void OnArrowLeft();
    public abstract void OnArrowRight();
}