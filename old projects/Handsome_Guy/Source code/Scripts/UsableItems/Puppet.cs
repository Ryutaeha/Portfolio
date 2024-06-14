using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Puppet : UsableItemData
{
    [SerializeField] int damage = 20;

    protected override void OnUse(GameObject receiver)
    {
        // 허수아비를 실행하는 함수 
    }

    public class UsePuppet : MonoBehaviour
    {
        private float timeLeft = 4.0f;

        IEnumerator TimeToAggro()
        {
            while(timeLeft>0)
            {
                Aggro();
                yield return new WaitForSeconds(1.0f);
                timeLeft--;
            }
        }

        void Aggro()
        {
            // Enemy Target = Puppet
        }
    }
}
