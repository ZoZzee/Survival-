using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DraggItems : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Inventory _inventory;
    private GameObject _startPos;
    private GameObject _endPos;

    [SerializeField] Cell _newMoveCell;

    private void MoveItem(GameObject cell)
    {
        Cell _cell = cell.GetComponent<Cell>();
        if (_cell.itemInCell == null)
        {
            return;
        }
        
        if(_startPos == null )
        {
            _newMoveCell.gameObject.SetActive(true);
            TransferCells(_cell, _newMoveCell);
        }
        else if (_endPos == null && _startPos != null && _newMoveCell != _cell)
        {
            _endPos = _cell.gameObject;

        }
        Debug.Log(_cell.itemInCell.name + " в клітинці"); 
        
    }

    private void TransferCells(Cell first, Cell second)
    {
        second.icon = first.icon;
        second.countText = first.countText;
        second.itemInCell = first.itemInCell;
    }


    //======== ======= ====== EVENTS ====== ======= ========
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        MoveItem(clickedObject);
    }
}
