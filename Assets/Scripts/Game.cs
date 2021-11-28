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
        [SerializeField] private Pool pool = null;
        [SerializeField] private CameraShake cameraShake = null;
        [Header("Data")]
        [SerializeField] private GameData data;
        [Header("UI")]
        [SerializeField] private UI.HudPresenter hudCanvas = null;
        [SerializeField] private UI.MenuPresenter menuCanvas = null;
        [SerializeField] private UI.GameOverPresenter gameoverCanvas = null;
        [SerializeField] private UI.GameOverPresenter winCanvas = null;


        public Color CurrentColor { get { return settings.colors[Data.currentColor]; } }
        public Color OppositeColor { get { return settings.colors[(Data.currentColor + 1) % 2]; } }

        public static Game Instance { get; private set; }
        public PlayerController Player { get => player; set => player = value; }
        public GameData Data { get => data; set => data = value; }
        public CameraShake CameraShake { get { return cameraShake; } private set { cameraShake = value; } }
        public LevelRenderer LevelRenderer { get => levelRenderer; set => levelRenderer = value; }

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
            HideAllUI();
            SetMenu();
        }

        void Update() => state.Update();
        void FixedUpdate() => state.FixedUpdate();

        public void SetMenu() => State = new MenuState(player, menuCanvas);
        public void SetPlaying() => State = new PlayingState(Player, pool, hudCanvas);
        public void SetGameOver() => State = new GameOverState(gameoverCanvas);
        public void SetWin() => State = new WinState(winCanvas);

        public void Reset()
        {
            Game.Instance.LevelRenderer.DestroyLevel();
            Data.Reset(settings);
            Game.Instance.Player.Reset();
            pool.ClearEnemies();
        }

        public void NextLevel()
        {
            if (Data.level >= settings.maxLevels)
            {
                SetWin();
            }
            else
            {
                Game.Instance.Player.Reset();
                pool.ClearEnemies();
                AudioManager.Instance.StopAllMusic();
                Data.level++;
                var level = LevelGenerator.Generate(levelTemplate, Data.level, settings.baseLevelSize + Data.level, settings.baseLevelSize + Data.level, settings.baseLevelSize + Data.level, settings.baseEnemyCount + Data.level);
                LevelRenderer.Render(level);
                player.transform.position = level.ConvertLevelPosToWorld(level.playerSpawn.levelLocation) + Vector2.up;
                AudioManager.Instance.PlayPlayMusic();
            }
        }

        public void NextTurn()
        {
            Data.turn++;
            SwitchColor();
            Player.Stop();
            pool.UpdateEnemiesNextTurn();
        }

        public void SwitchColor()
        {
            Data.currentColor++;
            if (Data.currentColor > 1)
                Data.currentColor = 0;
        }

        public void AddHealth(int amount)
        {
            Data.health = Mathf.Clamp(Data.health + amount, 0, settings.maxHealth);
            if (Data.health <= 0)
            {
                SetGameOver();
            }
        }

        public void Attack()
        {
            pool.Attack();
        }

        private void HideAllUI()
        {
            hudCanvas.Hide();
            menuCanvas.Hide();
            gameoverCanvas.Hide();
            winCanvas.Hide();
        }

    }
}
