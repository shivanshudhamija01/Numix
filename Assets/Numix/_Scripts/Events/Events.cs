using  UnityEngine;

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
}
