using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Spear : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float movementForce = 20;

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        public void Attack()
        {
            //Debug.Log("Spear attack");
            var hit = Physics2D.Raycast(this.transform.position, this.transform.up, 100.0f, layerMask);

            // if(hit.collider != null)
            // {
            //     Debug.Log(hit.collider.name);
            // }

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                //Debug.Log("Attack!");
                rigidbody.AddForce(this.transform.up * movementForce, ForceMode2D.Impulse);
            }
        }
    }
}