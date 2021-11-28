using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Cultist : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform fireSprite;

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
            
            var hit = Physics2D.Raycast(this.transform.position, direction, 16.0f, layerMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                //var distance = (Game.Instance.Player.transform.position - this.transform.position).magnitude;
                fireSprite.gameObject.SetActive(true);
                var position = Quaternion.AngleAxis(Random.Range(-45.0f,45.0f), Vector3.back) * direction * Random.Range(3, 8);
                fireSprite.transform.position = this.transform.position + position;
                //rigidbody.AddForce(direction * movementForce, ForceMode2D.Impulse);
            }
        }
    }
}