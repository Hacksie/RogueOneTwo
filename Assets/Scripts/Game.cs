using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameSettings settings;
        [SerializeField] private LevelRenderer levelRenderer = null;
        [SerializeField] private LevelGenTemplate levelTemplate = null;
        [SerializeField] private PlayerController player = null;
        [SerializeField] private EnemyPool enemyPool = null;
        [Header("Data")]
        [SerializeField] private GameData data;
        [Header("UI")]
        [SerializeField] private UI.HudPresenter hudCanvas = null;


        public Color CurrentColor { get { return settings.colors[Data.currentColor]; } }
        public Color OppositeColor { get { return settings.colors[(Data.currentColor + 1) % 2]; } }

        public static Game Instance { get; private set; }
        public PlayerController Player { get => player; set => player = value; }
        public GameData Data { get => data; set => data = value; }

        private IState state = new EmptyState();

        public IState State
        {
            get
            {
                return state;
            }
            private set
            {
                state.End();
                state = value;
                state.Begin();
            }
        }        

        Game()
        {
            Instance = this;
        }

        void Start()
        {
            Reset();
            NextLevel();
            HideAllUI();
            SetPlaying();
        }

        void Update() => state.Update();
        void FixedUpdate() => state.FixedUpdate();             

        public void SetPlaying() => State = new PlayingState(Player, enemyPool, hudCanvas);
        public void SetGameOver() => State = new GameOverState();

        private void Reset()
        {
            Data.Reset(settings);
            Game.Instance.Player.Reset();
            Game.Instance.Player.Reset();
            enemyPool.ClearEnemies();
        }

        public void NextLevel()
        {
            Game.Instance.Player.Reset();
            enemyPool.ClearEnemies();
            var level = LevelGenerator.Generate(levelTemplate, Data.level, levelTemplate.levelLength, levelTemplate.levelHeight, levelTemplate.levelWidth, settings.baseEnemyCount + Data.level);
            levelRenderer.Render(level);
            player.transform.position = level.ConvertLevelPosToWorld(level.playerSpawn.levelLocation);
            Data.level++;
        }

        public void AddHealth(int amount)
        {
            Data.health = Mathf.Clamp(Data.health + amount, 0, settings.maxHealth);
            if(Data.health <= 0)
            {
                SetGameOver();
            }
        }

        public void NextTurn()
        {
            Data.turn++;
            Data.currentColor++;
            if (Data.currentColor > 1)
                Data.currentColor = 0;

            enemyPool.UpdateEnemiesNextTurn();
        }

        public void Attack()
        {
            enemyPool.Attack();   
        }

        private void HideAllUI()
        {
            hudCanvas.Hide();
        }

    }
}
