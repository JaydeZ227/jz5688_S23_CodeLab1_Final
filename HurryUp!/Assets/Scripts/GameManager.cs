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

        public WakeUpTime alarmClockType = WakeUpTime.六点半;

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
                        case NegativeSituation.迟到心情值减2:
                            result += 2;
                            break;
                        case NegativeSituation.自行车连续撞到2次障碍物心情值减1:
                            result += 1;
                            break;
                        case NegativeSituation.大风下雨心情值减1:
                            result += 1;
                            break;
                        case NegativeSituation.地铁车厢内人数超过30心情值减1:
                            result += 1;
                            break;
                        case NegativeSituation.出租车指挥超过10min心情值减1:
                            result += 1;
                            break;
                    }
                }
            }
            return result;
        }


        #region 天气参数

        public WeatherType todayWeather;
        public WeatherType tomorrowWeather;

        public List<WeatherRate> weatherRates = new List<WeatherRate>();

        public void GetRandomTwoDayWeather() 
        {
            var rate = weatherRates[Random.Range(0,weatherRates.Count)];

            float randomNumber = Random.Range(0f,1.0f);

            if (randomNumber < rate.rainDay)
            {
                todayWeather = WeatherType.下雨;
            }
            else if (randomNumber < rate.rainDay + rate.sunnyDay)
            {
                todayWeather = WeatherType.晴天;
            }
            else 
            {
                todayWeather = WeatherType.大风;
            }

            float randomNumber2 = Random.Range(0f, 1.0f);

            if (randomNumber2 < rate.rainDay)
            {
                tomorrowWeather = WeatherType.下雨;
            }
            else if (randomNumber2 < rate.rainDay + rate.sunnyDay)
            {
                tomorrowWeather = WeatherType.晴天;
            }
            else
            {
                tomorrowWeather = WeatherType.大风;
            }
        }

        public void GetTomorrowWeather() 
        {
            todayWeather = tomorrowWeather;

            var rate = weatherRates[Random.Range(0, weatherRates.Count)];

            float randomNumber2 = Random.Range(0f, 1.0f);

            if (randomNumber2 < rate.rainDay)
            {
                tomorrowWeather = WeatherType.下雨;
            }
            else if (randomNumber2 < rate.rainDay + rate.sunnyDay)
            {
                tomorrowWeather = WeatherType.晴天;
            }
            else
            {
                tomorrowWeather = WeatherType.大风;
            }
        }

        #endregion


        #region 路况

        public RoadSituationType todayRoadSituation;

        public void RandomRoadSituation() 
        {
            int randomOffset = Random.Range(0,3);

            switch (randomOffset)
            {
                case 0:
                    todayRoadSituation = RoadSituationType.拥堵;
                    break;
                case 1:
                    todayRoadSituation = RoadSituationType.缓行;
                    break;
                case 2:
                    todayRoadSituation = RoadSituationType.畅通;
                    break;
                default:
                    todayRoadSituation = RoadSituationType.畅通;
                    break;
            }
        }
        #endregion

        #region 金钱相关

        public float currentMoneyCount = 100f;

        public Queue<IncomeInfo> incomeInfoQueue = new Queue<IncomeInfo>();

        public void AddMoney(IncomeInfo inCome) 
        {
            Debug.Log("金币计算"+ inCome.value);
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

            return $"{month}月{day}日";
        }
    }


    public class GameTime 
    {

        public static string GetTimeContent(float timer) 
        {
            string result = "";

            int hour = (int)(timer / 3600);
            int minute = (int)((timer - hour * 3600) / 60);
            result =  string.Format("{0:D2}时{1:D2}分", hour, minute);
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
                case WakeUpTime.六点半:
                    return 23400f;
                case WakeUpTime.七点:
                    return 25200f;
                case WakeUpTime.七点半:
                    return 27000f;
                default:
                    return 0f;
            }
        }
    
    }

    public enum WakeUpTime 
    {
        六点半,
        七点,
        七点半
    }

    public enum NegativeSituation
    {
        迟到心情值减2,
        自行车连续撞到2次障碍物心情值减1,
        大风下雨心情值减1,
        地铁车厢内人数超过30心情值减1,
        出租车指挥超过10min心情值减1
    }

    public enum WeatherType 
    {
        晴天,
        大风,
        下雨
    }

    public enum RoadSituationType
    {
        拥堵,
        缓行,
        畅通
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
        工资,
        迟到,
        行人,
        汽车,
        奖励,
        房租,
        地铁
    }
}



