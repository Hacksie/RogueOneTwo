using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    [System.Serializable]
    public class GameData 
    {
        
        public int level = 0;
        public int turn = 0;
        public int health = 3;
        public int currentColor = 0;

        public void Reset(GameSettings settings)
        {
            level = 0;
            turn = 0;
            health = settings.maxHealth;
        }
    }
}