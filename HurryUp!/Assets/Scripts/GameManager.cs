using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HurryUp
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [RuntimeInitializeOnLoadMethod]
        public static void LoadSystem() 
        {
            if (instance != null)
            {
                return;
            }
            Instantiate(Resources.Load<GameObject>("AudioManager"));
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else 
            {
                Destroy(gameObject);
                return;
            }

           DontDestroyOnLoad(gameObject);
        }

        public AudioManager audioManager;

        public GameDay currentDay = new GameDay();

        public int feelCount = 1;

        public int healthCount = 100;

        public float beginTimer = 0f;

        public float timer = 0f;

        public WakeUpTime alarmClockType = WakeUpTime.�����;

        public List<NegativeSituation> previousDayNevigation = new List<NegativeSituation>();

        public void AddNevigation(NegativeSituation type) 
        {
            if (!previousDayNevigation.Contains(type))
            {
                previousDayNevigation.Add(type);
            }
        }
        public void StartGame() 
        {
            currentDay = new GameDay();

            GetRandomTwoDayWeather();
        }


        public void AddFeel(int addFeel) 
        {
            feelCount += addFeel;
        }

        public void AddHealth(int health) 
        {
            healthCount += health;
        }

        public int GetNavigationCount() 
        {
            int result = 0;
            if (previousDayNevigation.Count != 0 && previousDayNevigation != null)
            {
                foreach (var item in previousDayNevigation)
                {
                    switch (item)
                    {
                        case NegativeSituation.�ٵ�����ֵ��2:
                            result += 2;
                            break;
                        case NegativeSituation.���г�����ײ��2���ϰ�������ֵ��1:
                            result += 1;
                            break;
                        case NegativeSituation.�����������ֵ��1:
                            result += 1;
                            break;
                        case NegativeSituation.������������������30����ֵ��1:
                            result += 1;
                            break;
                        case NegativeSituation.���⳵ָ�ӳ���10min����ֵ��1:
                            result += 1;
                            break;
                    }
                }
            }
            return result;
        }


        #region ��������

        public WeatherType todayWeather;
        public WeatherType tomorrowWeather;

        public List<WeatherRate> weatherRates = new List<WeatherRate>();

        public void GetRandomTwoDayWeather() 
        {
            var rate = weatherRates[Random.Range(0,weatherRates.Count)];

            float randomNumber = Random.Range(0f,1.0f);

            if (randomNumber < rate.rainDay)
            {
                todayWeather = WeatherType.����;
            }
            else if (randomNumber < rate.rainDay + rate.sunnyDay)
            {
                todayWeather = WeatherType.����;
            }
            else 
            {
                todayWeather = WeatherType.���;
            }

            float randomNumber2 = Random.Range(0f, 1.0f);

            if (randomNumber2 < rate.rainDay)
            {
                tomorrowWeather = WeatherType.����;
            }
            else if (randomNumber2 < rate.rainDay + rate.sunnyDay)
            {
                tomorrowWeather = WeatherType.����;
            }
            else
            {
                tomorrowWeather = WeatherType.���;
            }
        }

        public void GetTomorrowWeather() 
        {
            todayWeather = tomorrowWeather;

            var rate = weatherRates[Random.Range(0, weatherRates.Count)];

            float randomNumber2 = Random.Range(0f, 1.0f);

            if (randomNumber2 < rate.rainDay)
            {
                tomorrowWeather = WeatherType.����;
            }
            else if (randomNumber2 < rate.rainDay + rate.sunnyDay)
            {
                tomorrowWeather = WeatherType.����;
            }
            else
            {
                tomorrowWeather = WeatherType.���;
            }
        }

        #endregion


        #region ·��

        public RoadSituationType todayRoadSituation;

        public void RandomRoadSituation() 
        {
            int randomOffset = Random.Range(0,3);

            switch (randomOffset)
            {
                case 0:
                    todayRoadSituation = RoadSituationType.ӵ��;
                    break;
                case 1:
                    todayRoadSituation = RoadSituationType.����;
                    break;
                case 2:
                    todayRoadSituation = RoadSituationType.��ͨ;
                    break;
                default:
                    todayRoadSituation = RoadSituationType.��ͨ;
                    break;
            }
        }
        #endregion

        #region ��Ǯ���

        public float currentMoneyCount = 100f;

        public Queue<IncomeInfo> incomeInfoQueue = new Queue<IncomeInfo>();

        public void AddMoney(IncomeInfo inCome) 
        {
            Debug.Log("��Ҽ���"+ inCome.value);
            currentMoneyCount += inCome.value;

            if (incomeInfoQueue.Count < 5)
            {
                incomeInfoQueue.Enqueue(inCome);
            }
            else 
            {
                incomeInfoQueue.Dequeue();
                incomeInfoQueue.Enqueue(inCome);
            }
        }
        public void RemoveMoney(int money) 
        {
            currentMoneyCount -= money;

        
        }

        #endregion

        public bool yesterdayChiDao = false;
        public bool yesterdayBikeZhuangLeTwice = false;

        public bool yesterdayWindAndRain = false;
        public bool yesterdaySubwayHumanCountOver30 = false;
        public bool yesterdayTaxiOver10Min = false;


        public void ResetYesterDayStation() 
        {
            yesterdayChiDao = false;
            yesterdayBikeZhuangLeTwice = false;

            yesterdayWindAndRain = false;
            yesterdaySubwayHumanCountOver30 = false;
            yesterdayTaxiOver10Min = false;
        }

    }

    [System.Serializable]
    public class WeatherRate 
    {
        public float sunnyDay;
        public float rainDay;
        public float windDay;
    }


    public class GameDay 
    {
        public int month = 1;
        public int day = 1;

        public void MoveToNextDay() 
        {

            if (month != 2)
            {

                if (month % 2 == 0)
                {
                    if (day + 1 > 31)
                    {
                        month++;
                        day = 1;

                        if (month > 12)
                        {
                            month = 1;
                        }
                    }
                    else
                    {
                        day++;
                    }
                }
                else 
                {
                    if (day + 1 > 30)
                    {
                        month++;
                        day = 1;
                    }
                    else
                    {
                        day++;
                    }
                }
            }
            else 
            {
                if (day + 1 > 28)
                {
                    month++;
                    day = 1;
                }
                else
                {
                    day++;
                }
            }
        }

        public string GetDayContent()
        {

            switch (month)
            {
                case 1:
                    return $"Jan {day}";
                case 2:
                    return $"Feb {day}";
                case 3:
                    return $"Mar {day}";
                case 4:
                    return $"Apr {day}";
                case 5:
                    return $"May {day}";
                case 6:
                    return $"Jun {day}";
                case 7:
                    return $"Jul {day}";
                case 8:
                    return $"Aug {day}";
                case 9:
                    return $"Sept {day}";
                case 10:
                    return $"Oct {day}";
                case 11:
                    return $"Nov {day}";
                case 12:
                    return $"Dec {day}";
            }

            return $"{month}��{day}��";
        }
    }


    public class GameTime 
    {

        public static string GetTimeContent(float timer) 
        {
            string result = "";

            int hour = (int)(timer / 3600);
            int minute = (int)((timer - hour * 3600) / 60);
            result =  string.Format("{0:D2}ʱ{1:D2}��", hour, minute);
            return result;
        }

        public static string GetTimeContentTwoStyle(float timer)
        {
            string result = "";

            int hour = (int)(timer / 3600);
            int minute = (int)((timer - hour * 3600) / 60);
            result = string.Format("{0:D1}:{1:D2}", hour, minute);
            return result;
        }

        public static string GetTimeContentThreeStyle(float timer)
        {
            string result = "";

            int hour = (int)(timer / 3600);
            int minute = (int)((timer - hour * 3600) / 60);
            result = string.Format("{0:D1}:{1:D2}", hour, minute);
            return result;
        }

        public static string GetTimeFourStyleContent(float timer)
        {
            string result = "";

            int hour = (int)(timer / 3600);
            int minute = (int)((timer - hour * 3600) / 60);
            result = string.Format("{0:D2}:{1:D2}", hour, minute);
            return result;
        }


        public static float GetTime(WakeUpTime type) 
        {
            switch (type)
            {
                case WakeUpTime.�����:
                    return 23400f;
                case WakeUpTime.�ߵ�:
                    return 25200f;
                case WakeUpTime.�ߵ��:
                    return 27000f;
                default:
                    return 0f;
            }
        }
    
    }

    public enum WakeUpTime 
    {
        �����,
        �ߵ�,
        �ߵ��
    }

    public enum NegativeSituation
    {
        �ٵ�����ֵ��2,
        ���г�����ײ��2���ϰ�������ֵ��1,
        �����������ֵ��1,
        ������������������30����ֵ��1,
        ���⳵ָ�ӳ���10min����ֵ��1
    }

    public enum WeatherType 
    {
        ����,
        ���,
        ����
    }

    public enum RoadSituationType
    {
        ӵ��,
        ����,
        ��ͨ
    }

    [System.Serializable]
    public class IncomeInfo 
    {
        public float value;
        public IncomeType incomeType;

        public IncomeInfo(float value , IncomeType incomeType) 
        {
            this.value = value;
            this.incomeType = incomeType;
        }
    }

    public enum IncomeType 
    {
        ����,
        �ٵ�,
        ����,
        ����,
        ����,
        ����,
        ����
    }
}



