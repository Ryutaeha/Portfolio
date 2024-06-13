
using UnityEngine;

public class Util
{

    // 특정 GameObject에 원하는 컴포넌트가 있는지 확인하고, 없으면 추가하는 메서드입니다.
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();

        return component;
    }
}
