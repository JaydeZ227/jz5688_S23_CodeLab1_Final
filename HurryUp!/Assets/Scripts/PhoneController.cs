using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HurryUp 
{
    public class PhoneController : MonoBehaviour
    {
        [SerializeField] DataAndTime dayAndTime;
        //[SerializeField] HealthUIController healthUIController;
        [SerializeField] Image healthSlider;
        [SerializeField] TMP_Text xingQin;

        public List<GameObject> appPanels = new List<GameObject>();

        public static PhoneController instance;

        float timer = 0f;

        [SerializeField] AudioClip bgm;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            //HomeButtton();

            //GameManager.instance.GetRandomTwoDayWeather();

            //healthUIController.UpdateTmpHealth(GameManager.instance.healthCount);
            healthSlider.fillAmount = GameManager.instance.healthCount / 100.0f;

            timer = GameManager.instance.beginTimer;
            var data = GameManager.instance.currentDay.GetDayContent();
            var timeContent = GameTime.GetTimeContentThreeStyle(timer);
            dayAndTime.UpdateTimeTexT(timeContent,data);

            GameManager.instance.audioManager.PlayBGMAudio(bgm);

            StartCoroutine(WaitTimeToUpdateData());

            xingQin.text = GameManager.instance.feelCount.ToString();
        }

        IEnumerator WaitTimeToUpdateData() 
        {

            while (true)
            {
                yield return new WaitForSeconds(5);
                timer += 60;
                var data = GameManager.instance.currentDay.GetDayContent();
                var timeContent = GameTime.GetTimeContentThreeStyle(timer);
                dayAndTime.UpdateTimeTexT(timeContent, data);
            }
        }


        private void OnDestroy()
        {
            StopAllCoroutines();
            instance = null;
        }

        public void ShowAimAppPanle(string name) 
        {
            foreach (var item in appPanels)
            {
                if (item.name == name)
                {
                    item.SetActive(true);
                }
                else 
                {
                    item.SetActive(false);
                }
            }
        }


        #region App按钮方法

        public void BtnClock(bool value) 
        {
            if (value)
            {
                ShowAimAppPanle("闹钟");
            }
        }

        public void BtnWeather(bool value) 
        {
            if (value)
                ShowAimAppPanle("天气");
        }

        public void BtnRoad(bool value) 
        {
            if (value)
                ShowAimAppPanle("路况");
        }

        public void BtnMoney(bool value) 
        {
            if (value)
                ShowAimAppPanle("钱包");
        }

        public void BtnSetting(bool value) 
        {
            if (value)
                ShowAimAppPanle("设置");

        }

        public void BtnShop() 
        {
            
        }

        public void BtnGameBike(bool value) 
        {
            if (value)
                SceneManager.LoadScene("自行车");
        }

        public void BtnGameSubway(bool value) 
        {
            if (value)
                SceneManager.LoadScene("地铁");
        }

        public void BtnGameTaxi(bool value) 
        {
            if (value)
                SceneManager.LoadScene("出租车");
        }

        public void HomeButtton() 
        {
            ShowAimAppPanle("Apps");
        }

        #endregion
    }
}
