using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;
        private Pool enemyPool;

        public PlayingState(PlayerController player, Pool enemyPool, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            this.enemyPool = enemyPool;
        }

        public bool Playing => true;

        public void Begin()
        {
            this.hudPresenter.Show();
            AudioManager.Instance.PlayPlayMusic();
            Game.Instance.NextLevel();
            //Cursor.visible = false;
        }

        public void End()
        {
            //Cursor.visible = true;
            this.hudPresenter.Hide();
            AudioManager.Instance.StopPlayMusic();
            //this.player.Freeze();
        }

        public void Update()
        {
            this.player.UpdateBehaviour();
            this.hudPresenter.Repaint();
            this.enemyPool.UpdateEnemiesBehaviour();
        }

        public void FixedUpdate()
        {
            //this.player.FixedUpdateBehaviour();
        }

        public void Start()
        {
            Game.Instance.SetMenu();
        }
    }
}
