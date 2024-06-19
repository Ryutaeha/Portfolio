using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    #region Fields
    private RaycastHit _hit;
    private Vector3 _rayDirection;
    private float _rayAngle = -45.0f;
    private float _rayDistance = 15.0f;

    public float speed = 1.0f;
    public float maxHeight = 5.0f;
    public float weavingDistance = 1.5f;
    public float fallbackDistance = 20.0f;
    #endregion

    #region Unity Methods
    // 레이를 초기화 하는 메서드 입니다.
    private void Start()
    {
        _rayDirection = transform.TransformDirection(Vector3.back) * _rayDistance;

        _rayDirection = Quaternion.Euler(_rayAngle, 0.0f, 0.0f) * _rayDirection;
    }

    // 레이를 시각적으로 디버그하는 메서드 입니다.
    private void Update()
    {
        Debug.DrawRay(transform.position, _rayDirection, Color.blue);

        if (Physics.Raycast(transform.position, _rayDirection, out _hit, _rayDistance))
        {
            if (_hit.collider) Debug.DrawRay(transform.position, _rayDirection, Color.green);
        }
    }
    #endregion

    #region Methods
    // 전략을 적용하는 메서드입니다.
    public void ApplyStrategy(IManeuverBehaviour strategy)
    {
        strategy.Maneuver(this);
    }
    #endregion
}
