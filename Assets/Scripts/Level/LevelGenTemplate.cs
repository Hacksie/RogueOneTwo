using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HackedDesign
{

    [CreateAssetMenu(fileName = "LevelTemplate", menuName = "Rogue12/Level/Level Template")]
    public class LevelGenTemplate : ScriptableObject
    {
        public int levelLength = 7;
        public int levelWidth = 10;
        public int levelHeight = 10;
        public float spanHorizontal = 4;
        public float spanVertical = 4;
        public int enemyCount = 0;
        
        public bool generateDoors = true;
        
        public string startingRoomString = "wnww_entry";
 
        public GameObject doornsElement;
        public GameObject doorewElement;
        public GameObject entryElement;
        public GameObject exitElement;
        public List<GameObject> levelElements;

        public List<Enemy> enemies;
    }
}