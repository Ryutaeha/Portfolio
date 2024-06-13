using UnityEngine;

public class Util
{
    // 특정 GameObject의 자식 오브젝트에서 원하는 컴포넌트를 찾는 메서드입니다.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (recursive == false) // 직속 자식만 찾는 경우
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name) return component;
            }
        }

        return null;
    }

    // 특정 GameObject의 자식 오브젝트에서 원하는 GameObject를 찾는 메서드입니다.
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null) return null;

        return transform.gameObject;
    }

    // 특정 GameObject에 원하는 컴포넌트가 있는지 확인하고, 없으면 추가하는 메서드입니다.
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();

        return component;
    }
}
