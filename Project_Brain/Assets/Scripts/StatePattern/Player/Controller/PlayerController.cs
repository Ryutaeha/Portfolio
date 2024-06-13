using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Properties
    public PlayerInputs PlayarInputs { get; private set; }
    public PlayerInputs.PlayerActions PlayerActions { get; private set; }
    #endregion

    #region Methods
    /// 이 메서드는 PlayerInputs 인스턴스를 초기화하고, PlayerActions를 설정합니다. 
    void Awake()
    {
        PlayarInputs = new();
        PlayerActions = PlayarInputs.Player;
    }

    /// 이 메서드는 PlayerInputs를 활성화합니다.
    private void OnEnable()
    {
        PlayarInputs.Enable();
    }

    /// 이 메서드는 PlayerInputs를 비활성화합니다.
    private void OnDisable()
    {
        PlayarInputs.Disable();
    }
    #endregion
}

