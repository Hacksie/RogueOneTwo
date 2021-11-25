using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameSettings settings;
        [SerializeField] private LevelRenderer levelRenderer = null;
        [SerializeField] private LevelGenTemplate levelGenTemplate = null;
        [SerializeField] private PlayerController player = null;

        private int currentColor = 0;
        public Color CurrentColor { get { return settings.colors[currentColor];} }

        public static Game Instance { get; private set; }

        Game()
        {
            Instance = this;
        }

        void Start()
        {
            var level = LevelGenerator.Generate(levelGenTemplate);
            //levelRenderer.Initialize(this.playerController, this.levelParent);
            levelRenderer.Render(level);
            player.transform.position = level.ConvertLevelPosToWorld(level.playerSpawn.levelLocation) - new Vector2(4,4);
        }

        public void NextTurn()
        {
            currentColor++;
            if(currentColor > 1)
                currentColor = 0;
        }
   
    }
}
