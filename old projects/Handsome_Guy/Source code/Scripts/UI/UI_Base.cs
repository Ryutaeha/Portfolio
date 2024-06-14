using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI
{
    public class UI_Base : MonoBehaviour
    {
        protected Dictionary<Type, UnityEngine.Object[]> _objects = new();

        private bool _initialized;

        // 이 메서드는 UIBase 객체가 활성화될 때 호출됩니다.
        // UI 요소들을 초기화하는 Init 메서드를 호출합니다.
        protected virtual void OnEnable()
        {
            Init();
        }

        // 이 메서드는 UI 요소들을 초기화합니다.
        // 이미 초기화된 경우에는 false를 반환하고, 그렇지 않으면 초기화 작업을 수행한 뒤 true를 반환합니다.
        public virtual bool Init()
        {
            if (_initialized) return false;

            _initialized = true;
            return true;
        }

        // 이 제네릭 메서드는 UI 요소들을 바인딩합니다.
        // 주어진 타입의 이름을 이용해 자식 객체를 찾고, 해당 객체를 배열에 저장합니다.
        // 재귀적으로 탐색할지 여부는 매개변수 recursive로 결정됩니다.
        private void Binding<T>(Type type, bool recursive = false) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type);
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                objects[i] = typeof(T) == typeof(GameObject) ? Util.FindChild(gameObject, names[i], recursive) : Util.FindChild<T>(gameObject, names[i], recursive);

                if (objects[i] == null)
                    Debug.Log($"Failed to bind({names[i]})");
            }

            _objects.Add(typeof(T), objects);
        }

        // 이 메서드들은 각각의 타입의 UI 요소들을 바인딩합니다.
        // 매개변수로 전달된 타입의 이름을 이용해 자식 객체를 찾습니다.
        protected void BindButton(Type type, bool recursive = false) => Binding<Button>(type, recursive);

        protected void BindImage(Type type, bool recursive = false) => Binding<Image>(type, recursive);

        protected void BindObject(Type type, bool recursive = false) => Binding<GameObject>(type, recursive);

        protected void BindText(Type type, bool recursive = false) => Binding<TextMeshProUGUI>(type, recursive);

        // 이 메서드는 특정 게임 오브젝트에 UI 이벤트를 추가합니다.
        // 매개변수로 전달된 게임 오브젝트에 UIEventHandler 컴포넌트를 추가하거나 가져옵니다.
        // 지정된 UI 이벤트 타입에 따라 해당 이벤트에 액션을 추가합니다.
        protected void AddUIEvent(GameObject go, Action<PointerEventData> action = null, Define.UIEvent uIEvent = Define.UIEvent.Click)
        {
            UIEventHandler uiEventHandler = Util.GetOrAddComponent<UIEventHandler>(go);

            switch (uIEvent)
            {
                case Define.UIEvent.Click:
                    uiEventHandler.ClickAction -= action;
                    uiEventHandler.ClickAction += action;
                    break;
            }
        }

        // 이 제네릭 메서드는 바인딩된 UI 요소를 가져옵니다.
        // 매개변수로 전달된 인덱스를 이용해 해당 UI 요소를 반환합니다.
        private T Getter<T>(int index) where T : UnityEngine.Object
        {
            if (!_objects.TryGetValue(typeof(T), out UnityEngine.Object[] objs)) return null;
            return objs[index] as T;
        }

        // 이 메서드들은 인덱스를 이용해 바인딩된 UI 요소를 가져옵니다.
        protected GameObject GetObject(int index) => Getter<GameObject>(index);

        protected TextMeshProUGUI GetText(int index) => Getter<TextMeshProUGUI>(index);

        protected Button GetButton(int index) => Getter<Button>(index);

        protected Image GetImage(int index) => Getter<Image>(index);
    }
}