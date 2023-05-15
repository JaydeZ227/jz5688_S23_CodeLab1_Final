using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{

    public class AppSetting : MonoBehaviour
    {
        [SerializeField] Slider bgmSlider;
        [SerializeField] Slider musicSlider;

        private void Awake()
        {
            bgmSlider.onValueChanged.AddListener(ChangeBgm);
            musicSlider.onValueChanged.AddListener(ChangeMusic);
        }

        private void ChangeMusic(float arg0)
        {
            GameManager.instance.audioManager.audioEffectVolume = arg0;
            GameManager.instance.audioManager.UpdateAllAudioSourceVolum();
        }

        private void ChangeBgm(float arg0)
        {
            GameManager.instance.audioManager.audioVolume = arg0;
            GameManager.instance.audioManager.UpdateAllAudioSourceVolum();
        }

        private void OnEnable()
        {

            bgmSlider.SetValueWithoutNotify(GameManager.instance.audioManager.audioVolume);

            musicSlider.SetValueWithoutNotify(GameManager.instance.audioManager.audioEffectVolume);
        }


        public void CloseGame() 
        {
            Application.Quit();
        }
    }

}
