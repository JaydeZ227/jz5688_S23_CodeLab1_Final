using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HurryUp 
{
    public class PanelStartGame : MonoBehaviour
    {
        [SerializeField] string nextScene = "Æð´²";

        [SerializeField] AudioClip bgm;

        [SerializeField] Sprite muteSpr;
        [SerializeField] Sprite unMuteSpr;
        [SerializeField] Image music;

        public void Quit() 
        {
            Application.Quit();
        }

        private void Start()
        {

            GameManager.instance.audioManager.PlayBGMAudio(bgm);

            GameManager.instance.feelCount = 0;


            if (GameManager.instance.audioManager.isMute)
            {
                music.sprite = muteSpr;
            }
            else
            {
                music.sprite = unMuteSpr;
            }
        }



        public void SetMuteMusic ()
        {
            if (GameManager.instance.audioManager.isMute)
            {
                GameManager.instance.audioManager.isMute = false;

                music.sprite = unMuteSpr;
            }
            else 
            {
                GameManager.instance.audioManager.isMute = true;

                music.sprite = muteSpr;
            }

            GameManager.instance.audioManager.UpdateAllAudioSourceVolum();
        }

        public void StartGame() 
        {
            SceneManager.LoadScene(nextScene);

            GameManager.instance.audioManager.StopBGMAudio();

            GameManager.instance.StartGame();
        }

    }


}
