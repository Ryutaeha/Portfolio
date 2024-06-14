using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private int _order;
    private List<UI_Popup> _popupList = new List<UI_Popup>();
    UI_Scene _sceneUI = null;
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

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }

        GameObject go = Main.ResourceManager.Instantiate($"{name}.prefab");

        T popupComponent = Util.GetOrAddComponent<T>(go);
        _popupList.Add(popupComponent);
        go.transform.SetParent(Root.transform);


        return popupComponent;
    }

    public void ClosePopupUI()
    {
        if (_popupList.Count == 0) return;

        UI_Popup uIPopup = _popupList[_popupList.Count - 1];
        _popupList.RemoveAt(_popupList.Count - 1);

        //Main.ResourceManager.Destroy(uIPopup.gameObject.name);
        uIPopup = null;
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Main.ResourceManager.Instantiate($"{name}.prefab");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        return sceneUI;
    }
    public void Appear(GameObject go)
    {
        //go.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(go.GetComponent<RectTransform>().DOScale(1.1f, 0.2f));
        seq.Append(go.GetComponent<RectTransform>().DOScale(1f, 0.1f));

        seq.Play();
    }
    public void Hide(GameObject go)
    {
        Sequence seq = DOTween.Sequence();
        go.transform.localScale = Vector3.one * 0.2f;

        seq.Append(go.GetComponent<RectTransform>().DOScale(1.1f, 0.1f));
        seq.Append(go.GetComponent<RectTransform>().DOScale(0.1f, 0.2f));

        seq.Play().OnComplete(() => {
            go.SetActive(false);
        });
    }
}
