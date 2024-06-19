using System.Collections;
using System.Collections.Generic;
using Define;

public interface IInventorySystem
{
    #region Methods
    // 이 메서드는 모든 인벤토리를 동기화하는 역할을 합니다.
    // 여러 저장소에 분산되어 있는 인벤토리 데이터를 일관성 있게 유지하기 위해 사용됩니다.
    public void SyncInventories();

    // 이 메서드는 특정 아이템을 지정된 저장소 위치에 추가하는 기능을 합니다.
    public void AddItem(InventoryItem item, SaveLocation location);

    // 이 메서드는 특정 아이템을 지정된 저장소 위치에서 제거하는 기능을 합니다.
    public void RemoveItem(InventoryItem item, SaveLocation location);

    // 이 메서드는 지정된 저장소 위치에서 모든 인벤토리 아이템을 가져오는 역할을 합니다.
    public List<InventoryItem> GetInventory(SaveLocation location);
    #endregion
}
