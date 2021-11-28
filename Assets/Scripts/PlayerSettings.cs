using System.Collections.Generic;
using UnityEngine;


namespace HackedDesign
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Rogue12/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public float maxForceDistance = 3.0f;
        public float forceMultiplier = 20.0f;
        public float chargeMultiplier = 2.0f;
        public float rampageTime = 20.0f;
        public float invulvTime = 20.0f;
        public float rampageRadius = 2.0f;
    }
}