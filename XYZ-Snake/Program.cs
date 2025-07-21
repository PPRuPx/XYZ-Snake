using XYZ_Snake;

internal class Program
{
    public static void Main(string[] args)
    {
        var gameLogic = new SnakeGameLogic();
        var input = new ConsoleInput();
        
        gameLogic.InitializeInput(input);
        gameLogic.GotoGameplay();
        
        var lastFrameTime = DateTime.Now;

        while (true)
        {
            input.Update();
            var frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            gameLogic.Update(deltaTime);
            lastFrameTime = frameStartTime;
        }
    }
}
