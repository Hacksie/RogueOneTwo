using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HackedDesign
{

    [CreateAssetMenu(fileName = "LevelTemplate", menuName = "Rogue12/Level/Level Template")]
    public class LevelGenTemplate : ScriptableObject
    {
        public string corp;
        public string location;
        public string level;

        public int levelLength = 7;
        public int levelWidth = 10;
        public int levelHeight = 10;
        public float spanHorizontal = 4;
        public float spanVertical = 4;
        public int enemyCount = 0;
        
        public bool generateDoors = true;
        
        public string startingRoomString = "wnww_entry";
 
        // public List<GameObject> floors;
        // public List<GameObject> mainChainFloor;
        public List<GameObject> levelElements;
        // public List<GameObject> endProps;
        // public List<GameObject> startProps;
        // public List<GameObject> randomProps;
        // public List<GameObject> fixedProps;
        // public List<GameObject> lineOfSightProps;

        //public List<string> traps;
        public List<string> enemies;
    }

}