namespace XYZ_Snake;

public abstract class BaseGameLogic : ConsoleInput.IArrowListener
{
    protected BaseGameState? currentState { get; private set; }
    
    protected float time { get; private set; }
    protected int screenWidth { get; private set; }
    protected int screenHeight { get; private set; }
    
    public void InitializeInput(ConsoleInput consoleInput)
    {
        consoleInput.Subscribe(this);
    }

    public void ChangeState(BaseGameState gameState)
    {
        currentState?.Reset();
        currentState = gameState;
    }

    public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
    {
        time += deltaTime;
        screenWidth = renderer.width;
        screenHeight = renderer.height;
        
        currentState?.Update(deltaTime);
        currentState?.Draw(renderer);
        
        Update(deltaTime);
    }

    public abstract ConsoleColor[] CreatePalette();
    
    public abstract void Update(float deltaTime);

    public abstract void OnArrowUp();
    public abstract void OnArrowDown();
    public abstract void OnArrowLeft();
    public abstract void OnArrowRight();
}