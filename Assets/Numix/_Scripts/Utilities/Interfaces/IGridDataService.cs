using System.Collections.Generic;
using UnityEngine;
public interface IGridDataService
{
    public void MapTileToPosition(Dictionary<Vector3, GameObject> map);
    public int GetTileNumber(Vector3 position);
    public List<Vector3> GetNumberTilesPosition();
    public void Initialize(IEventBus eventBus);
}
