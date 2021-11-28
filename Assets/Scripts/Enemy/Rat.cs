using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Rat : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float movementForce = -10;

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        public void Attack()
        {
            var direction = (Game.Instance.Player.transform.position - this.transform.position).normalized;
            var hit = Physics2D.Raycast(this.transform.position, direction, 8.0f, layerMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                rigidbody.AddForce(direction * movementForce, ForceMode2D.Impulse);
            }
        }
    }
}