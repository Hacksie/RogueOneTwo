using UnityEngine;
using Cinemachine;

namespace HackedDesign
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private float shakeTimer = 0;
        //private float intensity = 0;

        private CinemachineBasicMultiChannelPerlin perlinNoise;

        void Awake()
        {
            perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0)
                {
                    ShakeOff();
                }
            }
        }

        public void ShakeOn(float intensity)
        {
            perlinNoise.m_AmplitudeGain = intensity;
        }

        public void ShakeOff()
        {
            ShakeOn(0);
            shakeTimer = 0;
        }

        public void Shake(float intensity, float time)
        {
            ShakeOn(intensity);
            shakeTimer = time;
        }

        public void SmallShake()
        {
            Shake(2f, 0.15f);
        }

        public void BigShake()
        {
            Shake(5f, 0.2f);
        }
    }
}