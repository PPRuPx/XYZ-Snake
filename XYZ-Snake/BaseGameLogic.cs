namespace XYZ_Snake;

public abstract class BaseGameLogic : ConsoleInput.IArrowListener
{
    public void InitializeInput(ConsoleInput consoleInput)
    {
        consoleInput.Subscribe(this);
    }

    public abstract void Update(float deltaTime);

    public abstract void OnArrowUp();
    public abstract void OnArrowDown();
    public abstract void OnArrowLeft();
    public abstract void OnArrowRight();
}