using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Bow : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Rigidbody2D arrowRb;
        //[SerializeField] private SpriteRenderer arrowSprite;

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        public void Turn()
        {
            Debug.Log("Turn");
            arrowRb.transform.position = this.transform.position + this.transform.up;
        }

        public void Attack()
        {
            Debug.Log("Bow attack");
            var hit = Physics2D.Raycast(this.transform.position, this.transform.up, 100.0f, layerMask);

            // if(hit.collider != null)
            // {
            //     Debug.Log(hit.collider.name);
            // }

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Debug.Log("Attack!");
                arrowRb.AddForce(this.transform.up * 40, ForceMode2D.Impulse);
                //rigidbody.AddForce(this.transform.up * movementForce, ForceMode2D.Impulse);
            }
        }
    }
}