using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIManager
{
    private int _order = 1;
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    // UIManager를 초기화하고, UI 서비스가 생성될 때 OnUIServiceAdd 메서드를 호출하도록 이벤트 핸들러를 설정합니다.
    public void Initialize()
    {
        Main.Service.ServiceCreated += OnUIServiceAdd;
    }

    // 모든 팝업이 닫혀 있는지 여부를 반환합니다.
    public bool _isAllClosed
    {
        get { return _popupStack.Count == 0; }
    }

    // UI 서비스가 추가될 때 호출되는 이벤트 핸들러입니다.
    // Binder 인스턴스를 생성하고 서비스를 등록합니다.
    private void OnUIServiceAdd(object sender, EventArgs e)
    {
        Binder binder = new();
        Main.Service.RegisterService(binder);
    }

    // UI의 최상위 루트 오브젝트를 반환합니다.
    // 만약 @UI_Root라는 이름의 오브젝트가 존재하지 않으면 새로 생성합니다.
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    // 주어진 GameObject에 Canvas 컴포넌트를 추가하고 설정합니다.
    // sort 매개변수가 true이면 캔버스의 정렬 순서를 증가시키고, false이면 정렬 순서를 0으로 설정합니다.
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    // 주어진 타입의 팝업 UI를 화면에 표시합니다.
    // name 매개변수가 null이면 타입 이름을 사용하여 프리팹을 로드합니다.
    // 생성된 팝업 UI를 _popupStack에 추가하고, Root 오브젝트의 자식으로 설정합니다.
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }

        GameObject go = Main.Resource.Instantiate($"{name}.prefab");

        T popupComponent = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popupComponent);
        go.transform.SetParent(Root.transform);

        return popupComponent;
    }

    // 가장 최근에 표시된 팝업 UI를 닫습니다.
    // _popupStack에서 팝업 UI를 제거하고, 해당 오브젝트를 파괴합니다.
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0) return;

        UI_Popup uiPopup = _popupStack.Pop();
        if (uiPopup == null) return;
        Main.Resource.Destroy(uiPopup.gameObject);
        uiPopup = null;
    }

    // 주어진 타입의 씬 UI를 화면에 표시합니다.
    // name 매개변수가 null이면 타입 이름을 사용하여 프리팹을 로드합니다.
    // 생성된 씬 UI를 _sceneUI에 저장하고, Root 오브젝트의 자식으로 설정합니다.
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Main.Resource.Instantiate($"{name}.prefab");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        return sceneUI;
    }
}
