using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class PanelFinalResult : MonoBehaviour
    {
        [SerializeField] string nextScene = "Æð´²";

        [SerializeField] TMP_Text yuEText;
        [SerializeField] TMP_Text feelText;

        private void Start()
        {
            GameManager.instance.audioManager.StopBGMAudio();    

            yuEText.text = $"<color=#83FAFE><size=300>$</size></color>{GameManager.instance.currentMoneyCount}";

            feelText.text = $"{GameManager.instance.feelCount}";
           
        }

        public void Sleep() 
        {
            GameManager.instance.currentDay.MoveToNextDay();
            SceneManager.LoadScene(nextScene);
        }

    }

}

