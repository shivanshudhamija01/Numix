using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleValidationService : IPuzzleValidationService
{
    private readonly IGridDataService gridDataService;
    private readonly IStepTrackerService stepTrackerService;
    private readonly IEventBus eventBus;
    public PuzzleValidationService(IGridDataService gridDataService, IStepTrackerService stepTrackerService, IEventBus eventBus)
    {
        this.gridDataService = gridDataService;
        this.stepTrackerService = stepTrackerService;
        this.eventBus = eventBus;
    }
    public void EvaluateTile(Vector3 tilePosition)
    {
        int tileNumber = gridDataService.GetTileNumber(tilePosition);
        // if (tileNumber <= 0)
        // {
        //     return;
        // }
        bool success = stepTrackerService.CurrentSteps == tileNumber;
        eventBus.Publish(new Events.OnTileEvaluate(tilePosition, success));
    }
}
