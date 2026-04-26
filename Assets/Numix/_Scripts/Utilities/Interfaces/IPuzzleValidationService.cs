using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleValidationService
{
    void EvaluateTile(Vector3 tilePosition);
    void RefreshTiles();
    void Initialize(IEventBus eventBus);
}
