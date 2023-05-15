using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class TaxiDestination : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.AddMoney(new IncomeInfo(100f,IncomeType.工资));

                if (GameManager.instance.timer > 28800)
                {
                    GameManager.instance.yesterdayChiDao = true;
                    GameManager.instance.AddFeel(-2);
                    GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.迟到));

                    if (GameManager.instance.feelCount <= 0)
                    {

                        SceneManager.LoadScene("游戏失败");
                        return;
                    }
                }


                switch (GameManager.instance.todayWeather)
                {
                    case WeatherType.晴天:
                        GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.汽车));
                        break;
                    case WeatherType.大风:
                        GameManager.instance.AddMoney(new IncomeInfo(-25f, IncomeType.汽车));
                        break;
                    case WeatherType.下雨:
                        GameManager.instance.AddMoney(new IncomeInfo(-30f, IncomeType.汽车));
                        break;
                    default:
                        break;
                }



                SceneManager.LoadScene("结算");
            }
        }
    }

}
