using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour 
{
    #region Unity Methods
    // 매니저를 초기화하는 메서드 입니다.
    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
    }
    #endregion
}
