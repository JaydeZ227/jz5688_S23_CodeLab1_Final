using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class AppWeather : MonoBehaviour
    {
        [SerializeField] Image todayWeather;
        [SerializeField] Image tomorrowWeather;

        [SerializeField] TMP_Text todayTemperature;
        [SerializeField] TMP_Text tomorrowTemperature;

        [SerializeField] Sprite sunny;
        [SerializeField] Sprite wind;
        [SerializeField] Sprite rain;

        private void OnEnable()
        {
            switch (GameManager.instance.todayWeather)
            {
                case WeatherType.晴天:
                    //todayTemperature.text = "20°——30°";
                    todayWeather.sprite = sunny;
                    break;
                case WeatherType.大风:
                    //todayTemperature.text = "5°——15°";
                    todayWeather.sprite = wind;
                    break;
                case WeatherType.下雨:
                   // todayTemperature.text = "10°——20°";
                    todayWeather.sprite = rain;
                    break;

            }

            switch (GameManager.instance.tomorrowWeather)
            {
                case WeatherType.晴天:
                    //tomorrowTemperature.text = "20°——30°";
                    tomorrowWeather.sprite = sunny;
                    break;
                case WeatherType.大风:
                    //tomorrowTemperature.text = "5°——15°";
                    tomorrowWeather.sprite = wind;
                    break;
                case WeatherType.下雨:
                    //tomorrowTemperature.text = "10°——20°";
                    tomorrowWeather.sprite = rain;
                    break;
            }
        }
    }


}
