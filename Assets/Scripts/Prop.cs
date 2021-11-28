using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class Prop : MonoBehaviour
    {
        [SerializeField] private float chanceOfDrop = 0.0f;

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                this.gameObject.SetActive(false);
                Game.Instance.NextTurn();
                Game.Instance.CameraShake.SmallShake();
                AudioManager.Instance.PlayPropSFX();
                Pool.Instance.SpawnSmallExplosion(this.transform.position);
                CheckDrop();
            }
        }

        private void CheckDrop()
        {
            if (Random.value <= chanceOfDrop)
            {
                Pool.Instance.SpawnRandomPickup(this.transform.position);
            }
        }
    }
}