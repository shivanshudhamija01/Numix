using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;


public class Tile : MonoBehaviour, IPointerClickHandler, ITile
{
    [SerializeField] private TextMeshPro tileNumberText;
    [SerializeField] private Material visitedTile;
    [SerializeField] private Material red;
    [SerializeField] private Material green;
    [SerializeField] private Material emissionYellow;
    // [SerializeField] private Material emissionBlue;
    [SerializeField] private Renderer tileRenderer;
    private Material defaultMaterial;
    private int tileNumber = -1;
    private Coordinate tileIndex;
    private EventBus eventBus;
    private IPathHintService pathHintService;
    private bool isNumberedTile = false;
    private IHintService hintService;
    private IStepTrackerService stepTrackerService;
    private bool isVisited = false;
    private void Awake()
    {
        defaultMaterial = tileRenderer.material;
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
        pathHintService = ServiceLocator.Get<IPathHintService>();
        hintService = ServiceLocator.Get<IHintService>();
        stepTrackerService = ServiceLocator.Get<IStepTrackerService>();
    }
    private void OnEnable()
    {
        eventBus.Subscribe<Events.OnTileEvaluate>(UpdateTileMaterial);
        eventBus.Subscribe<Events.OnHintModeStarted>(EnableGlow);
        eventBus.Subscribe<Events.OnHintModeEnded>(DisableGlow);
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnTileEvaluate>(UpdateTileMaterial);
        eventBus.Unsubscribe<Events.OnHintModeStarted>(EnableGlow);
        eventBus.Unsubscribe<Events.OnHintModeEnded>(DisableGlow);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int hintIndex = pathHintService.GetHintIndex(tileIndex);
        if (!hintService.IsHintActive)
        {
            eventBus.Publish(new Events.OnTileClicked(transform.position));
        }
        if (hintService.IsHintActive && !isNumberedTile)
        {
            StartCoroutine(PlayHintAnimation(hintIndex));
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
                isNumberedTile = true;
            }
            else
            {
                tileNumberText.gameObject.SetActive(false);
                isNumberedTile = false;
            }
        }
    }
    public Coordinate index { get => tileIndex; set => tileIndex = value; }
    // Here i goin to update the material of tile.
    private void UpdateTileMaterial(Events.OnTileEvaluate evt)
    {
        if (tileNumber <= 0 && evt.position == transform.position)
        {
            isVisited = true;
            tileRenderer.material = visitedTile;
            tileNumberText.text = stepTrackerService.CurrentSteps.ToString();
            tileNumberText.gameObject.SetActive(true);
            return;
        }
        if (transform.position != evt.position)
        {
            return;
        }
        tileRenderer.material = evt.success ? green : red;
        if (!evt.success)
        {
            eventBus.Publish(new Events.OnLevelFailed());
        }
    }
    private IEnumerator PlayHintAnimation(int number)
    {
        tileNumberText.text = number.ToString();
        tileNumberText.gameObject.SetActive(true);

        Transform textTransform = tileNumberText.transform;

        Vector3 localScale = textTransform.localScale;

        Vector3 originalScale = Vector3.zero;
        Vector3 targetScale = new Vector3(0.75f, 0.75f, 0.75f);

        float duration = 0.2f;
        float time = 0f;

        // Scale UP (pop effect)
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            textTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        textTransform.localScale = targetScale;

        // Wait
        yield return new WaitForSeconds(0.4f);

        // Scale DOWN
        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            textTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        textTransform.localScale = originalScale;

        // Hide
        textTransform.localScale = localScale;
        tileNumberText.gameObject.SetActive(false);

        // FIRE EVENT AFTER ANIMATION
        eventBus.Publish(new Events.OnHintUsed());
    }
    private void EnableGlow(Events.OnHintModeStarted evt)
    {
        if (isNumberedTile || isVisited) return;
        tileRenderer.material = emissionYellow;
    }
    private void DisableGlow(Events.OnHintModeEnded evt)
    {
        if (isNumberedTile || isVisited) return;
        tileRenderer.material = defaultMaterial;
        hintService.IsHintActive = false;
    }
}
