using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tile : MonoBehaviour, IPointerEnterHandler, ITile
{
    [SerializeField] private TextMeshPro tileNumberText;
    private int tileNumber = -1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer enter and now spawn the ball on this tile");
        Debug.Log("Value of tile number is : " + tileNumber);
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
