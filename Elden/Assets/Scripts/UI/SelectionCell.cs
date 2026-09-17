using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SelectionCell : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    public Item moveItem;
    private int countItems;

    // ======= Referenses =======
    private Cell _firstCell;
    private Equipment _firstEquipment;


    [SerializeField] private Cell interactionCell;

    private void OnDisable()
    {
        CleenCell();
    }
    public void MoveItem(Item item, int count, Cell cell, Equipment __inventory)
    {
        if (_firstCell != cell && _firstCell == null && item != null)
        {
            _firstCell = cell;
            _firstEquipment = __inventory;
            moveItem = item;
            countItems = count;
            interactionCell.RefreshCell(item, count);
            //Debug.Log("Перший предмет добавлено");
        }
        else
        {
            if (_firstCell == cell)
            {
                CleenCell();
                //Debug.Log("Клітинка співпала з першою");
            }
            else
            {
                if (_firstCell != cell)
                {
                    for (int i = 0; i < _inventory.inventory.cells.Length; i++)
                    {
                        if (_inventory.inventory.cells[i] == cell)
                        {
                            for (int j = 0; j < _firstEquipment.cells.Length; j++)
                            {
                                _firstEquipment.items[i] = _inventory.inventory.items[i];
                                _inventory.inventory.counts[i] = _inventory.inventory.counts[i];
                                _inventory.inventory.cells[i].RefreshCell(_inventory.inventory.items[i], _inventory.inventory.counts[i]);
                            }
                            _inventory.inventory.items[i] = moveItem;
                            _inventory.inventory.counts[i] = countItems;
                            _inventory.inventory.cells[i].RefreshCell(moveItem, countItems);
                            CleenCell();
                            //Debug.Log("Перший предмет перенесенно");
                        }
                    }
                    for (int i = 0; i < _inventory.bagInventory.cells.Length; i++)
                    {
                        if (_inventory.bagInventory.cells[i] == cell)
                        {
                            _inventory.bagInventory.items[i] = moveItem;
                            _inventory.bagInventory.counts[i] = countItems;
                            _inventory.bagInventory.cells[i].RefreshCell(moveItem, countItems);
                            CleenCell();
                            //Debug.Log("Перший предмет перенесенно");
                        }
                    }

                }
            }
        }

    }

    //private int searchCell()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        return i;
    //    }
        
    //}

    private void CleenCell()
    {
        interactionCell.RefreshCell(null);
        _firstCell = null;
        interactionCell.gameObject.SetActive(false);
    }

}
