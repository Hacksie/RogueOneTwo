using UnityEngine;

namespace HackedDesign.UI
{
    public class MenuPresenter : AbstractPresenter
    {
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;

        public override void Repaint()
        {
            Debug.Log("Toggle " + AudioManager.Instance.GetMasterVolume());
            soundOn.SetActive(AudioManager.Instance.GetMasterVolume() == 0);
            soundOff.SetActive(AudioManager.Instance.GetMasterVolume() < 0);
        }

        public void PlayEvent()
        {
            Game.Instance.SetPlaying();
        }

        public void SoundToggleEvent()
        {
            
            AudioManager.Instance.SetMasterVolume(AudioManager.Instance.GetMasterVolume() == 0 ? -80.0f : 0.0f);
            
            Repaint();
        }
    }
}