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
    public struct OnLevelFailed : IGameEvent
    {
    }
    public struct OnGamePaused : IGameEvent
    {
    }

    public struct OnExitButtonClicked : IGameEvent
    {
    }
    public struct OnSettingButtonClicked : IGameEvent
    {
    }
    public struct OnNextLevelLoaded : IGameEvent
    {
    }
    public struct OnLevelInitialized : IGameEvent
    {
    }
    public struct OnHomeClicked : IGameEvent
    {
    }
    public struct OnLevelRestart : IGameEvent
    {

    }
    #region  HINT SYSTEM
    public struct OnHintRequested : IGameEvent { }
    public struct OnHintModeStarted : IGameEvent { }
    public struct OnHintUsed : IGameEvent { }
    public struct OnHintModeEnded : IGameEvent { }
    #endregion
}
