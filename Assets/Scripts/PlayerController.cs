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
        [Header("Settings")]
        [SerializeField] private PlayerSettings settings;
        

        

        private Vector2 mousePosition;
        private Vector3 direction;
        

        private bool charge = false;
        private float chargeStartTime = 0;

        void Awake()
        {
            if(rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateArrow();
        }

        public void OnFire(InputValue value)
        {
            if(value.isPressed)
            {
                charge = true;
                chargeStartTime = Time.time;
                //Debug.Log("On");
            }
            else
            {
                charge = false;
                //Debug.Log("Off");
                Shoot();
            }
        }

        public void Shoot()
        {
            var force = Mathf.Clamp((Time.time - chargeStartTime) * settings.chargeMultiplier, 0, settings.maxForceDistance);
            //Debug.Log("Force: " + force);
            chargeStartTime = 0;
            rigidbody.AddForce(transform.up * force * settings.forceMultiplier, ForceMode2D.Impulse);
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
    }
}
