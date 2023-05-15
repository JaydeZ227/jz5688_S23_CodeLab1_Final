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
                GameManager.instance.AddMoney(new IncomeInfo(100f,IncomeType.����));

                if (GameManager.instance.timer > 28800)
                {
                    GameManager.instance.yesterdayChiDao = true;
                    GameManager.instance.AddFeel(-2);
                    GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.�ٵ�));

                    if (GameManager.instance.feelCount <= 0)
                    {

                        SceneManager.LoadScene("��Ϸʧ��");
                        return;
                    }
                }


                switch (GameManager.instance.todayWeather)
                {
                    case WeatherType.����:
                        GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.����));
                        break;
                    case WeatherType.���:
                        GameManager.instance.AddMoney(new IncomeInfo(-25f, IncomeType.����));
                        break;
                    case WeatherType.����:
                        GameManager.instance.AddMoney(new IncomeInfo(-30f, IncomeType.����));
                        break;
                    default:
                        break;
                }



                SceneManager.LoadScene("����");
            }
        }
    }

}
