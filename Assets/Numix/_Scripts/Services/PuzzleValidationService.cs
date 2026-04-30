using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleValidationService : IPuzzleValidationService
{
    private readonly IGridDataService gridDataService;
    private readonly IStepTrackerService stepTrackerService;
    private readonly IEventBus eventBus;
    private readonly IGameServices gameService;
    private List<Vector3> numberTilesPositions;
    public PuzzleValidationService(IGridDataService gridDataService, IStepTrackerService stepTrackerService, IEventBus eventBus,IGameServices gameService)
    {
        this.gridDataService = gridDataService;
        this.stepTrackerService = stepTrackerService;
        this.eventBus = eventBus;
        this.gameService = gameService;
        numberTilesPositions = gridDataService.GetNumberTilesPosition();
    }
    public void EvaluateTile(Vector3 tilePosition)
    {
        int tileNumber = gridDataService.GetTileNumber(tilePosition);

        bool success = stepTrackerService.CurrentSteps == tileNumber;
        if (success)
        {
            numberTilesPositions.Remove(tilePosition);
            if (numberTilesPositions.Count == 0)
            {
                int currentLevel = gameService.CurrentLevel + 1;
                int unlockedLevel = PlayerPrefs.GetInt(Utility.LEVEL_KEY, 1);
                if (currentLevel > unlockedLevel)
                {
                    PlayerPrefs.SetInt(Utility.LEVEL_KEY, currentLevel);
                }
                eventBus.Publish(new Events.OnLevelComplete());
            }
        }
        else
        {
            // eventBus.Publish(new Events.OnLevelFailed());
        }
        eventBus.Publish(new Events.OnTileEvaluate(tilePosition, success));
    }
    public void RefreshTiles()
    {
        numberTilesPositions = gridDataService.GetNumberTilesPosition();
    }
    public void Initialize(IEventBus eventBus)
    {
        eventBus.Subscribe<Events.OnLevelInitialized>(OnLevelInitialized);
    }
    private void OnLevelInitialized(Events.OnLevelInitialized obj)
    {
        RefreshTiles();
    }
}

