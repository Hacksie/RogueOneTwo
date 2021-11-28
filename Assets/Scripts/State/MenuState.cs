using UnityEngine;

namespace HackedDesign
{
    public class MenuState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter menuPresenter;

        private float colourSwitchTimer = 0;

        public MenuState(PlayerController player, UI.AbstractPresenter menuPresenter)
        {
            this.player = player;
            this.menuPresenter = menuPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            Game.Instance.Reset();
            this.menuPresenter.Show();      
            this.player.AlertOn();
            Game.Instance.CameraShake.ShakeOn(1);
            AudioManager.Instance.PlayMenuMusic();
        }

        public void End()
        {
            this.menuPresenter.Hide();   
            this.player.AlertOff();
            Game.Instance.CameraShake.ShakeOff();
            AudioManager.Instance.StopMenuMusic();
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
            Application.Quit();
        }

        public void Update()
        {
            if(Time.time >= colourSwitchTimer)   
            {
                Game.Instance.SwitchColor();
                player.UpdateColor();
                colourSwitchTimer = Time.time + 1.0f;
            }   
        }
    }
}