using Define;
using UnityEngine;

public class ClientAdapter : MonoBehaviour
{
    #region Fields
    public InventoryItem item; // 인벤토리에 추가할 아이템입니다.
    private InventorySystem _inventorySystem; // 기본 인벤토리 시스템입니다.
    private IInventorySystem _inventorySystemAdapter; // 어댑터 패턴을 적용한 인벤토리 시스템입니다.
    #endregion

    #region Unity Methods
    //기존 인벤토리 시스템과 어댑터 패턴을 이용한 인벤토리 시스템의 인스턴시를 생성합니다.
    private void Start()
    {
        _inventorySystem = new InventorySystem(); 
        _inventorySystemAdapter = new InventorySystemAdapter();
    }
    // 어댑터를 사용하지 않고 아이템을 추가하는 버튼과 어댑터를 사용하여 아이템을 추가하는 버튼을 그립니다.
    private void OnGUI()
    {
        if (GUILayout.Button("Add item (no adapter)"))
            _inventorySystem.AddItem(item);

        if (GUILayout.Button("Add item (with adapter)"))
            _inventorySystemAdapter.AddItem(item, SaveLocation.Both);
    }
    #endregion 
}
