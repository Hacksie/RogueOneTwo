using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Fire: MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                Game.Instance.AddHealth(-1);
            }
        }
    }
}