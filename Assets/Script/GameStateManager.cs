public class GameStateManager
{
    private static GameStateManager _instace;

    public static GameStateManager Instance
    {
        get
        {
            if (_instace == null)
                _instace = new GameStateManager();

            return _instace;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;
    private GameStateManager()
    {

    }
    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
