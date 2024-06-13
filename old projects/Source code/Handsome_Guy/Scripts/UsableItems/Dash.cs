using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Dash : UsableItemData
{
    [SerializeField] int distance = 10;
    Vector2 mousePos, transPos, targetPos;

    //Player.Position = playerPosition;

    protected override void OnUse(GameObject receiver)
    {
        // 마우스 커서 방향으로 10만큼 이동
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * distance);
    }
}
