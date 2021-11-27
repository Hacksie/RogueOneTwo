using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private new Collider2D collider;
        [SerializeField] private int minLevel = 0;
        [SerializeField] private UnityEvent attackEvent;
        [SerializeField] private UnityEvent turnEvent;

        private bool offsetColor = false;
        //private Color currentColor;
        public Color CurrentColor { get { return sprite.color; } }

        public int MinLevel { get => minLevel;}

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        void Start()
        {
            offsetColor = Random.value > 0.5;
            UpdateColor();
        }

        public void UpdateBehaviour()
        {

        }

        public void NextTurn()
        {
            turnEvent.Invoke();
        }

        public void Attack()
        {
            attackEvent.Invoke();
        }

        private void UpdateColor()
        {
            sprite.color = offsetColor ? Game.Instance.OppositeColor : Game.Instance.CurrentColor;
        }

        private void Explode()
        {
            EnemyPool.Instance.KillEnemy(this);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player") && Game.Instance.Player.Attacking && sprite.color != Game.Instance.CurrentColor)
            {
                Game.Instance.AddHealth(-1);
                this.rigidbody.velocity = Vector2.zero;

                Game.Instance.Player.NextTurn();
            }
            else if (other.collider.CompareTag("Player") && Game.Instance.Player.Attacking && sprite.color == Game.Instance.CurrentColor)
            {
                Explode();
            }            
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player") && Game.Instance.Player.Attacking && sprite.color != Game.Instance.CurrentColor)
            {
                Game.Instance.AddHealth(-1);
                this.rigidbody.velocity = Vector2.zero;

                Game.Instance.Player.NextTurn();
            }
            else if (other.collider.CompareTag("Player") && Game.Instance.Player.Attacking && sprite.color == Game.Instance.CurrentColor)
            {
                Explode();
            }            
        }        
    }
}