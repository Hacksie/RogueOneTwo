using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer sprite;
        [Header("Settings")]
        [SerializeField] private PlayerSettings settings;

        private Vector2 mousePosition;
        private Vector3 direction;

        private PlayingState state = PlayingState.Waiting;
        private bool charge = false;
        private float chargeStartTime = 0;

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        void Start()
        {
            sprite.color = Game.Instance.CurrentColor;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateArrow();
            CheckAttackOver();
        }

        public void OnFire(InputValue value)
        {
            if (state == PlayingState.Waiting)
            {
                Debug.Log("X");
                if (value.isPressed)
                {
                    Charge();
                }
            }
            if(state == PlayingState.Charging)
            {
                if(!value.isPressed)
                {
                    Shoot();
                }
            }
        }

        private void Charge()
        {
            state = PlayingState.Charging;
            //charge = true;
            chargeStartTime = Time.time;
        }


        public void Shoot()
        {
            state = PlayingState.Attacking;
            var force = Mathf.Clamp((Time.time - chargeStartTime) * settings.chargeMultiplier, 0, settings.maxForceDistance);
            //Debug.Log("Force: " + force);
            chargeStartTime = 0;
            rigidbody.AddForce(transform.up * force * settings.forceMultiplier, ForceMode2D.Impulse);
        }

        private void CheckAttackOver()
        {
            if(state == PlayingState.Attacking && rigidbody.velocity.sqrMagnitude < Vector2.kEpsilonNormalSqrt)
            {
                Game.Instance.NextTurn();
                state = PlayingState.Waiting;
            }
        }

        public void OnPosition(InputValue value)
        {
            mousePosition = value.Get<Vector2>();
            //Debug.Log(mousePosition);
        }

        private void UpdateArrow()
        {
            direction = (mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -1 * mainCamera.transform.position.z)) - this.transform.position).normalized;

            this.transform.up = direction;
        }

        [System.Serializable]
        public enum PlayingState
        {
            Waiting,
            Charging,
            Attacking
        }
    }
}
