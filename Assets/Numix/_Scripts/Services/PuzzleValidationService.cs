using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleValidationService : IPuzzleValidationService
{
    private readonly IGridDataService gridDataService;
    private readonly IStepTrackerService stepTrackerService;
    private readonly IEventBus eventBus;
    private List<Vector3> numberTilesPositions;
    public PuzzleValidationService(IGridDataService gridDataService, IStepTrackerService stepTrackerService, IEventBus eventBus)
    {
        this.gridDataService = gridDataService;
        this.stepTrackerService = stepTrackerService;
        this.eventBus = eventBus;
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

