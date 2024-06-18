using System.Collections;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    #region Fields
    // Observer 객체들을 저장하기 위한 ArrayList 필드입니다.
    private readonly ArrayList _observers = new();
    #endregion

    #region Methods
    // Observer 객체를 등록하는 메서드입니다.
    // 매개변수 observer는 등록할 Observer 객체입니다.
    public void Attach(Observer observer)
    {
        _observers.Add(observer);
    }

    // Observer 객체를 등록 해제하는 메서드입니다.
    // 매개변수 observer는 등록 해제할 Observer 객체입니다.
    public void Detach(Observer observer)
    {
        _observers.Remove(observer);
    }

    // 등록된 모든 Observer 객체들에게 알림을 보내는 메서드입니다.
    public void NotifyObservers()
    {
        foreach (Observer observer in _observers)
        {
            observer.Notify(this);
        }
    }
    #endregion
}
