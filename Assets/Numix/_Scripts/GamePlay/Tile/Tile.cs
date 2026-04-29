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
    private Coordinate tileIndex;
    private EventBus eventBus;
    private IPathHintService pathHintService;
    private bool isNumberedTile;
    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
        pathHintService = ServiceLocator.Get<IPathHintService>();
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
        // Debug.Log("Pointer enter and now spawn the ball on this tile");
        // Debug.Log("Value of tile number is : " + tileNumber);
        Debug.Log("Coordinate of tile is : " + tileIndex.x + " " + tileIndex.z);
        eventBus.Publish(new Events.OnTileClicked(transform.position));
        int hintIndex = pathHintService.GetHintIndex(tileIndex);
        if (hintIndex > 0)
        {
            tileNumberText.text = hintIndex.ToString();
            tileNumberText.gameObject.SetActive(true);
        }
        else
        {

        }
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
    public Coordinate index { get => tileIndex; set => tileIndex = value; }
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
