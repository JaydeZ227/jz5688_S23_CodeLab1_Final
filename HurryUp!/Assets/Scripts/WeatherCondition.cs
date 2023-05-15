using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HurryUp 
{
    public class WeatherCondition : MonoBehaviour
    {
        [SerializeField] GameObject sunnyDayObject;
        [SerializeField] UnityEvent OnSunnyDay;

        [SerializeField] GameObject rainDayObject;
        [SerializeField] UnityEvent OnRainDay;

        [SerializeField] GameObject windDayObject;
        [SerializeField] UnityEvent OnwindDay;

        private void Start()
        {

            switch (GameManager.instance.todayWeather)
            {
                case WeatherType.晴天:
                    sunnyDayObject.SetActive(true);
                    OnSunnyDay?.Invoke();
                    break;
                case WeatherType.大风:
                    windDayObject.SetActive(true);
                    OnwindDay?.Invoke();
                    break;
                case WeatherType.下雨:
                    rainDayObject.SetActive(true);
                    OnRainDay?.Invoke();
                    break;
            }
        }


    }

}
