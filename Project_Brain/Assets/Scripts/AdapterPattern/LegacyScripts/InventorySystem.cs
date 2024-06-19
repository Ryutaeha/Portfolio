
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    #region Methods
    // 이 메서드는 인벤토리에 아이템을 추가하는 역할을 합니다.
    public void AddItem(InventoryItem item)
    {
        Debug.Log("Adding item to the cloud");
    }

    // 이 메서드는 인벤토리에서 아이템을 제거하는 역할을 합니다.
    public void RemoveItem(InventoryItem item) 
    {
        Debug.Log("Removing item from the cloud");
    }

    // 이 메서드는 클라우드에 저장된 인벤토리 리스트를 반환하는 역할을 합니다.
    public List<InventoryItem> GetInventory()
    {
        Debug.Log("Returning an inventory list stored in the cloud");

        return new();
    }
    #endregion
}
