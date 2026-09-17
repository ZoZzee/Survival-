using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DraggItems : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private inventoryController _inventoryController;
    [SerializeField] private Inventory _inventory;

    private Item _item;

    [SerializeField] SelectionCell _newMoveCell;

    private void MoveItem(GameObject cell)
    {
        Cell _cell = cell.GetComponent<Cell>();

        for (int i = 0; i < _inventory.inventory.cells.Length; i++)
        {
            if (_inventory.inventory.cells[i] == _cell)
            {
                    TransferCells(_inventory.inventory.items[i], _inventory.inventory.counts[i],_cell, _inventory.inventory);
                
            }
        }
        for (int i = 0; i < _inventory.bagInventory.cells.Length; i++)
        {
            if (_inventory.bagInventory.cells[i] == _cell)
            {
                    TransferCells(_inventory.bagInventory.items[i], _inventory.bagInventory.counts[i],_cell, _inventory.bagInventory);
                
            }
        }

    }

    private void TransferCells(Item firstItem, int firstItemCount,Cell _cell, Equipment inventory)
    {
        if(firstItem != null && _item == null)
        {
            Debug.Log("Запис предмету");
            _item = firstItem;
            _newMoveCell.gameObject.SetActive(true);
            _newMoveCell.MoveItem(firstItem, firstItemCount, _cell, inventory);
        }
        else if(_item != null)
        {
            Debug.Log("Перенос відбувається у іншу комірку. Інвентарі стільки ячейок- " + inventory.cells.Length);
            _item = null;
            _newMoveCell.MoveItem(firstItem, firstItemCount, _cell, inventory);
            _inventory.Refresh();
        }

    }



    //======== ======= ====== EVENTS ====== ======= ========
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        MoveItem(clickedObject);
    }
}
