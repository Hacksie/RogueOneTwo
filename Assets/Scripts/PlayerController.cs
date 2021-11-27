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
        [SerializeField] private Transform forceArrow;
        [SerializeField] private SpriteRenderer arrowSprite;

        [Header("Settings")]
        [SerializeField] private PlayerSettings settings;

        private Vector2 mousePosition;
        private Vector3 direction;

        private PlayingState state = PlayingState.Waiting;

        private float chargeStartTime = 0;

        public bool Attacking { get { return state == PlayingState.Attacking; } }

        void Awake()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        void Start()
        {
            UpdateColor();
        }

        // Update is called once per frame
        public void UpdateBehaviour()
        {
            UpdateArrow();
            CheckAttackOver();
        }

        public void OnFire(InputValue value)
        {
            if (state == PlayingState.Waiting && value.isPressed)
            {
                Charge();
            }
            if (state == PlayingState.Charging && !value.isPressed)
            {
                Shoot();
            }
        }

        public void OnPosition(InputValue value)
        {
            mousePosition = value.Get<Vector2>();
        }

        public void Reset()
        {
            rigidbody.velocity = Vector2.zero;
            state = PlayingState.Waiting;
            UpdateColor();
        }

        private void UpdateColor()
        {
            sprite.color = Game.Instance.CurrentColor;
            arrowSprite.color = Game.Instance.CurrentColor;
        }

        private void Charge()
        {
            state = PlayingState.Charging;
            chargeStartTime = Time.time;
        }

        private void Shoot()
        {
            state = PlayingState.Attacking;
            var force = Mathf.Clamp((Time.time - chargeStartTime) * settings.chargeMultiplier, 0, settings.maxForceDistance);
            rigidbody.AddForce(direction * force * settings.forceMultiplier, ForceMode2D.Impulse);
            Game.Instance.Attack();
            chargeStartTime = 0;
        }

        private void CheckAttackOver()
        {
            if (state == PlayingState.Attacking && rigidbody.velocity.sqrMagnitude < 2.0f)
            {
                NextTurn();
            }
        }

        public void NextTurn()
        {
            Game.Instance.NextTurn();
            Reset();
        }

        private void UpdateArrow()
        {
            direction = (mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -1 * mainCamera.transform.position.z)) - this.transform.position).normalized;

            //this.transform.up = direction;
            forceArrow.gameObject.SetActive(state == PlayingState.Charging);
            forceArrow.position = this.transform.position + (direction * Mathf.Clamp((Time.time - chargeStartTime) * settings.chargeMultiplier, 0, settings.maxForceDistance));
            forceArrow.up = direction;
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
