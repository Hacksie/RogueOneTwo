using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;
        private EnemyPool enemyPool;

        public PlayingState(PlayerController player, EnemyPool enemyPool, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            this.enemyPool = enemyPool;
        }

        public bool Playing => true;

        public void Begin()
        {
            this.hudPresenter.Show();
            //Cursor.visible = false;
        }

        public void End()
        {
            //Cursor.visible = true;
            this.hudPresenter.Hide();
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
            //GameManager.Instance.SetPaused();
        }
    }
}
