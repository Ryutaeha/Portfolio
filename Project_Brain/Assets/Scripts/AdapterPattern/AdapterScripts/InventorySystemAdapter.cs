using Define;
using System.Collections.Generic;
using UnityEngine;


#region Advantages
// 장점
// - 기존 클래스의 인터페이스를 변경하지 않고도 새로운 인터페이스로 변환할 수 있습니다.
// - 새로운 시스템이나 라이브러리와의 통합에 유연성을 제공합니다.
// - 기존 코드를 수정하지 않고 재사용할 수 있어 코드의 안정성을 유지할 수 있습니다.
// - 코드 중복을 줄이고, 유지보수성을 향상시킬 수 있습니다.
#endregion

#region Disadvantages
// 단점
// - 어댑터를 구현하기 위해 추가적인 코드가 필요하며, 이는 시스템의 복잡성을 증가시킬 수 있습니다.
// - 어댑터 패턴을 남용하면 코드가 난해해질 수 있으며, 특히 여러 어댑터가 중첩되는 경우 문제를 파악하기 어려울 수 있습니다.
// - 성능 오버헤드가 발생할 수 있습니다. 어댑터를 통해 호출되는 메서드는 직접 호출보다 성능이 떨어질 수 있습니다.
#endregion

#region When to Use
// 어댑터 패턴을 사용할 만한 경우
// - 기존 클래스가 필요한 인터페이스를 구현하지 않는 경우: 기존 클래스를 수정할 수 없거나 수정하지 않고 새로운 인터페이스를 제공해야 할 때.
// - 서로 다른 인터페이스를 가진 클래스들을 통합해야 하는 경우: 다양한 클래스들을 일관된 인터페이스로 사용해야 할 때.
// - 기존 시스템을 새로운 환경으로 전환할 때: 새로운 환경에서 기존 시스템을 재사용하고자 할 때.
// - 외부 라이브러리나 API를 기존 시스템과 통합해야 하는 경우: 외부 라이브러리의 인터페이스를 기존 시스템에 맞게 변환해야 할 때.
#endregion



public class InventorySystemAdapter : InventorySystem, IInventorySystem
{
    #region Fields
    // 클라우드에 저장된 인벤토리 아이템을 저장하는 리스트입니다.
    private List<InventoryItem> _cloudInventory;
    #endregion

    #region Methods
    // 이 메서드는 로컬과 클라우드 인벤토리를 동기화하는 역할을 합니다.
    // 클라우드 인벤토리를 가져와서 로컬 드라이브와 일치시키는 작업을 수행합니다.
    public void SyncInventories()
    {
        var _cloudInventory = GetInventory();

        Debug.Log("Synchronizing local drive and cloud inventories");
    }

    // 이 메서드는 지정된 저장 위치에 아이템을 추가하는 역할을 합니다.
    public void AddItem(InventoryItem item, SaveLocation location)
    {
        if (location == SaveLocation.Cloud)
            AddItem(item);

        if (location == SaveLocation.Local)
            Debug.Log("Adding item to local drive"); 

        if (location == SaveLocation.Both)
            Debug.Log("Adding item to local drive and on the cloud"); 
    }

    // 이 메서드는 지정된 저장 위치에서 아이템을 제거하는 역할을 합니다.
    public void RemoveItem(InventoryItem item, SaveLocation location)
    {
        Debug.Log("Remove from local/cloud/both");
    }

    // 이 메서드는 지정된 저장 위치에서 인벤토리 리스트를 가져오는 역할을 합니다.
    public List<InventoryItem> GetInventory(SaveLocation location)
    {
        Debug.Log("Get inventory from local/cloud/both");

        return new();
    }
    #endregion
}
