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
                case WeatherType.����:
                    sunnyDayObject.SetActive(true);
                    OnSunnyDay?.Invoke();
                    break;
                case WeatherType.���:
                    windDayObject.SetActive(true);
                    OnwindDay?.Invoke();
                    break;
                case WeatherType.����:
                    rainDayObject.SetActive(true);
                    OnRainDay?.Invoke();
                    break;
            }
        }


    }

}
