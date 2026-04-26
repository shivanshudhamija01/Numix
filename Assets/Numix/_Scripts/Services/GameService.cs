

public class GameService : IGameServices
{
    private int currentLevel = 1;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
 
}
