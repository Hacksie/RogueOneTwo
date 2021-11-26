using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class ExitTrigger : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") && EnemyPool.Instance.EnemyCount == 0)
            {
                Game.Instance.NextLevel();
            }
        }
    }
}