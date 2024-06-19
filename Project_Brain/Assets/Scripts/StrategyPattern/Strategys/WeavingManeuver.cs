using System.Collections;
using UnityEngine;

public class WeavingManeuver : MonoBehaviour, IManeuverBehaviour
{
    #region Methods
    // 이 메서드는 적 큐브(EnemyCube)가 좌우로 움직이는 동작을 하도록 하는 메서드입니다.
    // Maneuver 메서드는 적 큐브 객체를 받아서 해당 큐브가 지정된 거리를 좌우로 반복적으로 움직이는 동작을 구현합니다.
    // 이 메서드는 StartCoroutine 메서드를 통해 코루틴을 시작합니다.
    public void Maneuver(EnemyCube enemyCube)
    {
        StartCoroutine(Weave(enemyCube));
    }
    #endregion

    #region Coroutine
    // 이 코루틴은 적 큐브가 좌우로 움직이는 동작을 구현합니다.
    // Weave 코루틴은 적 큐브의 초기 위치와 이동할 목표 위치를 설정한 후, 큐브가 두 위치 사이를 반복적으로 이동하도록 합니다.
    private IEnumerator Weave(EnemyCube enemyCube)
    {
        float time;
        bool isReverse = false;
        float speed = enemyCube.speed; 
        Vector3 startPosition = enemyCube.transform.position;
        Vector3 endPosition = startPosition;
        endPosition.x = enemyCube.weavingDistance; 

        while (true)
        {
            time = 0;
            Vector3 start = enemyCube.transform.position; 
            Vector3 end = isReverse ? startPosition : endPosition; 

            while (time < speed)
            {
                enemyCube.transform.position = Vector3.Lerp(start, end, time / speed);
                time += Time.deltaTime;
                yield return null; 
            }

            yield return new WaitForSeconds(1); 
            isReverse = !isReverse; 
        }
    }
    #endregion
}
