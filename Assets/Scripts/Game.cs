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

        public static Game Instance { get; private set; }

        Game()
        {
            Instance = this;
        }

        void Awake()
        {
            var level = LevelGenerator.Generate(levelGenTemplate);
            //levelRenderer.Initialize(this.playerController, this.levelParent);
            levelRenderer.Render(level);
        }
   
    }
}
