
using UnityEngine;


namespace HackedDesign
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Rogue12/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Color[] colors=new Color[2];
        public int baseEnemyCount = 4;
        //public int startingHealth = 3;
        public int maxHealth = 3;
    }
}