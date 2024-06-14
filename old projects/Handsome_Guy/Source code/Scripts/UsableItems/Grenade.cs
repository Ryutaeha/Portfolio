using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grenade : UsableItemData
{
    [SerializeField] int damage = 20;

    protected override void OnUse(GameObject receiver)
    {
        // 수류탄을 실행시키는 함수
    }

    public class GrenadeExplode : MonoBehaviour
    {
        private float timeLeft = 3.0f;

        void Start()
        {
            StartCoroutine(TimeExplode());    
        }

        IEnumerator TimeExplode()
        {
            while(timeLeft>0)
            {
                yield return new WaitForSeconds(1.0f);
                timeLeft--;
            }
            Explode();
        }

        void Explode()
        {
            // Damage = 20
        }
    }
}
