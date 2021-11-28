using UnityEngine;

namespace HackedDesign.UI
{
    public class GameOverPresenter : AbstractPresenter
    {

        public override void Repaint()
        {

        }

        public void QuitEvent()
        {
            Game.Instance.SetMenu();
        }
    }
}