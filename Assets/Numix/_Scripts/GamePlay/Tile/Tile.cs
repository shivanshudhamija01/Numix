using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tile : MonoBehaviour, IPointerClickHandler, ITile
{
    [SerializeField] private TextMeshPro tileNumberText;
    [SerializeField] private Material black;
    [SerializeField] private Material red;
    [SerializeField] private Material green;
    [SerializeField] private Renderer tileRenderer;
    private int tileNumber = -1;
    private EventBus eventBus;
    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
    }
    private void OnEnable()
    {
        eventBus.Subscribe<Events.OnTileEvaluate>(UpdateTileMaterial);
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnTileEvaluate>(UpdateTileMaterial);
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
    // Here i goin to update the material of tile.
    private void UpdateTileMaterial(Events.OnTileEvaluate evt)
    {
        if (tileNumber <= 0 && evt.position == transform.position)
        {
            tileRenderer.material = black;
            return;
        }
        if (transform.position != evt.position)
        {
            return;
        }
        tileRenderer.material = evt.success ? green : red;
    }
}
