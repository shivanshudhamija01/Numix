using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tile : MonoBehaviour, IPointerClickHandler, ITile
{
    [SerializeField] private TextMeshPro tileNumberText;
    private int tileNumber = -1;
    private EventBus eventBus;
    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer enter and now spawn the ball on this tile");
        Debug.Log("Value of tile number is : " + tileNumber);
        eventBus.Publish(new Events.OnTileClicked(transform.position));
    }

    public int TileNumber
    {
        get => tileNumber;
        set
        {
            if (value > 0)
            {
                tileNumberText.text = value.ToString();
                tileNumber = value;
            }
            else
            {
                tileNumberText.gameObject.SetActive(false);
            }
        }
    }
}
