using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class RampagePickup : MonoBehaviour
    {
        float countdown = 0;
        bool activated = false;

        void Update()
        {
            if(activated && Time.time > countdown)
            {
                Game.Instance.Player.RampageOn();
                Game.Instance.CameraShake.SmallShake();
                this.gameObject.SetActive(false);
                
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // FIXME: Activate a timer
            if (!activated && other.collider.CompareTag("Player"))
            {
                countdown = Time.time + 0.5f;
                activated = true;
                AudioManager.Instance.PlayPickupSFX();
            }
        }
    }
}