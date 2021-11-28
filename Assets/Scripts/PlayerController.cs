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
        [SerializeField] private GameObject alert;
        [SerializeField] private ParticleSystem rampage;
        [SerializeField] private GameObject invuln;

        [Header("Settings")]
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private LayerMask enemies;

        private Vector2 mousePosition;
        private Vector3 direction;

        private PlayingState state = PlayingState.Waiting;

        private float chargeStartTime = 0;
        private float rampageTime = 0;
        private float invulnTime = 0;
        private bool rampaging = true;
        private bool invulnerable = true;

        public bool Attacking { get { return state == PlayingState.Attacking; } }

        public bool Rampaging { get => rampaging; private set => rampaging = value; }
        public bool Invulnerable { get => invulnerable; private set => invulnerable = value; }

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
            CheckRampageOver();
            CheckInvulnOver();
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

        public void OnEscape()
        {
            Game.Instance.State.Start();
        }

        public void Reset()
        {
            Stop();
            AlertOff();
            Invulnerable = false;
            invulnTime = 0;
            invuln.SetActive(false);
            rampaging = false;
            rampageTime = 0;
            rigidbody.MovePosition(Vector2.zero);
            direction = Vector2.zero;
        }

        public void Stop()
        {
            rigidbody.velocity = Vector2.zero;
            state = PlayingState.Waiting;
            chargeStartTime = 0;
            UpdateColor();
        }

        public void AlertOn() => alert.SetActive(true);
        public void AlertOff() => alert.SetActive(false);

        public void RampageOn()
        {
            Rampaging = true;
            rampageTime = Time.time + settings.rampageTime;
            InvulnOff();
            RampagePlay();
            AudioManager.Instance.StopPlayMusic();
            AudioManager.Instance.PlayRampageMusic();
        }
        public void RampageOff()
        {
            rampaging = false;
            rampage.Stop();
            AudioManager.Instance.StopRampageMusic();
            AudioManager.Instance.PlayPlayMusic();
            AlertOff();
        }

        public void RampagePlay() => rampage.Play();
        
        public void InvulnOn()
        {
            RampageOff();
            Invulnerable = true;
            invulnTime = Time.time + settings.invulvTime;
            invuln.SetActive(true);
            AudioManager.Instance.StopPlayMusic();
            AudioManager.Instance.PlayInvulnMusic();
        }
        public void InvulnOff()
        {
            Invulnerable = false;
            invuln.SetActive(false);
            AudioManager.Instance.StopInvulnMusic();
            AudioManager.Instance.PlayPlayMusic();
            AlertOff();
        }


        public void UpdateColor()
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
                if (Rampaging)
                {
                    RampageAttack();
                    RampagePlay();
                }
                Game.Instance.NextTurn();
            }
        }

        private void RampageAttack()
        {
            var hits = Physics2D.OverlapCircleAll(this.transform.position, settings.rampageRadius, enemies);

            foreach(var h in hits)
            {
                var enemy = h.gameObject.GetComponent<Enemy>();

                if(enemy != null)
                {
                    enemy.Explode();
                }
            }
        }

        private void CheckRampageOver()
        {
            if (Rampaging && Time.time > rampageTime)
            {
                RampageOff();
                AlertOff();
            }
            else if (Rampaging)
            {
                AlertOn();
            }
        }

        private void CheckInvulnOver()
        {
            if (Invulnerable && Time.time > invulnTime)
            {
                InvulnOff();
                AlertOff();
            }
            else if (Invulnerable)
            {
                AlertOn();
            }
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
