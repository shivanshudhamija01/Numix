using UnityEngine;

public class Events
{
    public struct OnTileClicked : IGameEvent
    {
        public Vector3 position;
        public OnTileClicked(Vector3 position)
        {
            this.position = position;
        }
    }

    public struct OnTileEvaluate : IGameEvent
    {
        public Vector3 position;
        public bool success;

        public OnTileEvaluate(Vector3 position, bool success)
        {
            this.position = position;
            this.success = success;
        }
    }
    public struct OnLoadLevel : IGameEvent
    {
        public int levelIndex;
        public OnLoadLevel(int levelIndex)
        {
            this.levelIndex = levelIndex;
        }
    }
    public struct OnGameStarted : IGameEvent
    {
        
    }   
    public struct OnLevelComplete : IGameEvent
    {
    }
    public struct OnGameLost : IGameEvent
    {
    }
     public struct OnGamePaused : IGameEvent
    {
    }
     public struct OnGameResumed : IGameEvent
    {
    }
    public struct OnExitButtonClicked : IGameEvent
    {
    }
    public struct OnSettingButtonClicked : IGameEvent
    {
    }
}
