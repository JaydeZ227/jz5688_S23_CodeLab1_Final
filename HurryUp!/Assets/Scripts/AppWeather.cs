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
                case WeatherType.����:
                    //todayTemperature.text = "20�㡪��30��";
                    todayWeather.sprite = sunny;
                    break;
                case WeatherType.���:
                    //todayTemperature.text = "5�㡪��15��";
                    todayWeather.sprite = wind;
                    break;
                case WeatherType.����:
                   // todayTemperature.text = "10�㡪��20��";
                    todayWeather.sprite = rain;
                    break;

            }

            switch (GameManager.instance.tomorrowWeather)
            {
                case WeatherType.����:
                    //tomorrowTemperature.text = "20�㡪��30��";
                    tomorrowWeather.sprite = sunny;
                    break;
                case WeatherType.���:
                    //tomorrowTemperature.text = "5�㡪��15��";
                    tomorrowWeather.sprite = wind;
                    break;
                case WeatherType.����:
                    //tomorrowTemperature.text = "10�㡪��20��";
                    tomorrowWeather.sprite = rain;
                    break;
            }
        }
    }


}
