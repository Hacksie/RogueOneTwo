namespace HackedDesign
{
    public class WinState : IState
    {
        UI.AbstractPresenter winPresenter;

        public WinState(UI.AbstractPresenter winPresenter)
        {
            this.winPresenter = winPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.winPresenter.Show();
        }

        public void End()
        {
            this.winPresenter.Hide();
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
    }
}