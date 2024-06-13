using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    #region Fields
    [SerializeField] private CharacterController controller;

    [SerializeField] private float drag = 0.3f;
    private float vertiaclVelocity;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    #endregion

    #region Properties
    public Vector3 Movement => impact + Vector3.up * vertiaclVelocity;
    #endregion

    #region Methods
    /// 이 메서드는 CharacterController 컴포넌트를 가져옵니다. 
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// 캐릭터가 땅에 있는지 확인하고, 중력에 따른 수직 속도를 업데이트합니다.
    /// 또한, 충격력을 점진적으로 감소시킵니다.
    void Update()
    {
        if (controller.isGrounded) vertiaclVelocity = Physics.gravity.y * Time.deltaTime;
        else vertiaclVelocity += Physics.gravity.y * Time.deltaTime;

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    /// 이 메서드는 수직 속도와 충격력을 초기화합니다.
    public void Reset()
    {
        vertiaclVelocity = 0;
        impact = Vector3.zero;
    }

    /// 이 메서드는 외부에서 전달된 힘을 충격력에 더합니다.
    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    /// 이 메서드는 주어진 점프 힘을 이용해 수직 속도를 증가시킵니다.
    public void Jump(float jumpForce)
    {
        vertiaclVelocity += jumpForce;
    }
    #endregion
}

