using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HurryUp 
{
    public class GamePanelController : MonoBehaviour
    {

        [SerializeField] TMP_Text money;

        [SerializeField] TMP_Text xinQiNG;

        [SerializeField] TMP_Text timeText;

        private void Start()
        {
            StartCoroutine(BeginTimer());
        }

        IEnumerator BeginTimer()
        {

            timeText.text = GameTime.GetTimeFourStyleContent(GameManager.instance.timer);
            while (true)
            {
                yield return new WaitForSeconds(1f);

                GameManager.instance.timer += 60f;

                timeText.text = GameTime.GetTimeFourStyleContent(GameManager.instance.timer);

                //dataAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dataContent);
            }
        }

        public void Update()
        {

            if (GameManager.instance != null)
            {
                xinQiNG.text = GameManager.instance.feelCount.ToString();
                money.text = $"${GameManager.instance.currentMoneyCount}";
            }
        }

        public void Pause() 
        {
            Time.timeScale = 0;
        }

        public void GoOnGame() 
        {
            Time.timeScale = 1f;
        }
    }


}

