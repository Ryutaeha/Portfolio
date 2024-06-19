using System.Collections;
using UnityEngine;

public class FallbackManeuver : MonoBehaviour, IManeuverBehaviour
{
    #region Methods
    // 이 메서드는 적 큐브(EnemyCube)가 후퇴 동작을 하도록 하는 메서드입니다.
    // Maneuver 메서드는 적 큐브 객체를 받아서 해당 큐브가 지정된 거리만큼 후퇴하는 동작을 구현합니다.
    // 이 메서드는 StartCoroutine 메서드를 통해 코루틴을 시작합니다.
    public void Maneuver(EnemyCube enemyCube)
    {
        StartCoroutine(Fallback(enemyCube));
    }
    #endregion

    #region Coroutine
    // 이 코루틴은 적 큐브가 지정된 거리만큼 후퇴하는 동작을 구현합니다.
    // Fallback 코루틴은 적 큐브의 초기 위치와 후퇴할 목표 위치를 설정한 후, 큐브가 두 위치 사이를 일정 속도로 이동하도록 합니다.
    private IEnumerator Fallback(EnemyCube enemyCube)
    {
        float time = 0; 
        float speed = enemyCube.speed; 
        Vector3 startPosition = enemyCube.transform.position;
        Vector3 endPosition = startPosition;
        endPosition.z = enemyCube.fallbackDistance; 

        while (time < speed)
        {
            enemyCube.transform.position = Vector3.Lerp(startPosition, endPosition, time / speed);

            time += Time.deltaTime;

            yield return null;
        }
    }
    #endregion
}
