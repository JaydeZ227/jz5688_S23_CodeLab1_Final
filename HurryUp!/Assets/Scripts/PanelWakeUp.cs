using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HurryUp 
{

    public class PanelWakeUp : MonoBehaviour
    {

        [SerializeField] AudioClip bgmPanel;

        //[SerializeField] DataAndTime dataAndTime;
        //[SerializeField] HealthUIController healthUIController;
        [SerializeField] Image healthBar;
        [SerializeField] Image monthImg;
        [SerializeField] Image dayImage;
            
        [SerializeField] TMP_Text alarmClockText;

        [SerializeField] GameObject sleepBtn;

        [SerializeField] string nextSceneName = "手机";

        [SerializeField] List<ClockTimeController> clockTimeList = new List<ClockTimeController>();

        [SerializeField] List<Sprite> monthSpriteList = new List<Sprite>();

        [SerializeField] List<Sprite> daySpriteList = new List<Sprite>();

        private void Start()
        {
            GameManager.instance.audioManager.PlayBGMAudio(bgmPanel);

            GameManager.instance.beginTimer = GameTime.GetTime(GameManager.instance.alarmClockType);
            UpdateCurrentInfo();
        }

        public void UpdateCurrentInfo() 
        {
            var data = GameManager.instance.currentDay.GetDayContent();
            var timeContent = GameTime.GetTimeContent(GameManager.instance.beginTimer);
            //dataAndTime.UpdateTimeTexT(timeContent, data);

            UpdateDataAndTime();

            alarmClockText.text = GameTime.GetTimeContentTwoStyle(GameManager.instance.beginTimer);

            //healthUIController.UpdateTmpHealth( GameManager.instance.healthCount);

            healthBar.fillAmount = GameManager.instance.healthCount / 100.0f;
        }


        private void UpdateDataAndTime() 
        {

            var mouth = GameManager.instance.currentDay.month;
            var day = GameManager.instance.currentDay.day;

            monthImg.sprite = monthSpriteList[mouth - 1];
            dayImage.sprite = daySpriteList[day - 1];

            var timeContent = GameTime.GetTimeContent(GameManager.instance.beginTimer);

            var charArray = timeContent.ToCharArray();
            var charlist = charArray.ToList();

            if (charlist.Contains('时'))
            {
                charlist.Remove('时');
            }

            if (charlist.Contains('分'))
            {
                charlist.Remove('分');
            }

            for (int i = 0; i < 4; i++)
            {
                clockTimeList[i].UpdateTime(int.Parse(charlist[i].ToString()));
            }
        }


        public void WakeUp() 
        {
            switch (GameManager.instance.alarmClockType)
            {
                case WakeUpTime.六点半:
                    GameManager.instance.feelCount = 8;
                    break;
                case WakeUpTime.七点:
                    GameManager.instance.feelCount = 9;
                    break;
                case WakeUpTime.七点半:
                    GameManager.instance.feelCount = 10;
                    break;
            }

            GameManager.instance.GetTomorrowWeather();


            if (GameManager.instance.yesterdayChiDao)
            {
                GameManager.instance.feelCount -= 2;
            }

            if (GameManager.instance.yesterdayBikeZhuangLeTwice)
            {
                GameManager.instance.feelCount -= 1;
            }

            if (GameManager.instance.todayWeather == WeatherType.大风 || GameManager.instance.todayWeather == WeatherType.下雨)
            {
                GameManager.instance.feelCount -= 1;
            }

            if (GameManager.instance.yesterdayTaxiOver10Min)
            {
                GameManager.instance.feelCount -= 1;
            }


            SceneManager.LoadScene(nextSceneName);
        }


        public void SleepAndWakeUp() 
        {
            GameManager.instance.beginTimer += 1800f;

            UpdateCurrentInfo();

            if (GameManager.instance.beginTimer > 43200f)
            {
                sleepBtn.SetActive(false);
            }

            //感受+1
            GameManager.instance.AddFeel(1);
        }
    }


}
