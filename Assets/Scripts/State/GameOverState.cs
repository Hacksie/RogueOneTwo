namespace HackedDesign
{
    public class GameOverState : IState
    {
        UI.AbstractPresenter gameoverPresenter;

        public GameOverState(UI.AbstractPresenter gameoverPresenter)
        {
            this.gameoverPresenter = gameoverPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.gameoverPresenter.Show();
        }

        public void End()
        {
            this.gameoverPresenter.Hide();
            Game.Instance.LevelRenderer.DestroyLevel();
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