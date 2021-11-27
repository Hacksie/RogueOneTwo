using UnityEngine;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text currentLevel;
        [SerializeField] UnityEngine.UI.Text currentTurn;
        //[SerializeField] UnityEngine.UI.Text health;
        [SerializeField] UnityEngine.UI.Text enemies;
        [SerializeField] UnityEngine.UI.Image health1;
        [SerializeField] UnityEngine.UI.Image health2;
        [SerializeField] UnityEngine.UI.Image health3;

        public override void Repaint()
        {
            currentLevel.text = Game.Instance.Data.level.ToString();
            currentTurn.text = Game.Instance.Data.turn.ToString();
            //health.text = Game.Instance.Data.health.ToString();
            enemies.text = (EnemyPool.Instance.MaxEnemyCount - EnemyPool.Instance.EnemyCount).ToString() + "/" + EnemyPool.Instance.MaxEnemyCount.ToString();

            health1.gameObject.SetActive(Game.Instance.Data.health >= 1);
            health2.gameObject.SetActive(Game.Instance.Data.health >= 2);
            health3.gameObject.SetActive(Game.Instance.Data.health >= 3);
        }
    }
}