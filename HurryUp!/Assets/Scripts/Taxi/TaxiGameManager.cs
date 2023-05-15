using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class TaxiGameManager : MonoBehaviour
    {
        public static TaxiGameManager instance;

        [SerializeField] TaxiController taxi;
        [SerializeField] List<Transform> spawnPoints = new List<Transform>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

        [SerializeField] TMP_Text xingQing;
        [SerializeField] DataAndTime dayAndTime;

        string dayContent;

        private void Start()
        {
            dayContent = GameManager.instance.currentDay.GetDayContent();
            GameManager.instance.timer = GameManager.instance.beginTimer;

            dayAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dayContent);
            StartCoroutine(BeginTimer());

            var randomPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];

            taxi.transform.position = randomPoint.position;
        }

        IEnumerator BeginTimer()
        {
            int timer = 0;

            while (true)
            {
                timer++;
                yield return new WaitForSeconds(1f);

                if (timer >= 15)
                {
                    GameManager.instance.yesterdayTaxiOver10Min = true;
                }

                GameManager.instance.timer += 60f;
                dayAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dayContent);
            }
        }

        private void Update()
        {
            if (GameManager.instance != null)
            {
                if (GameManager.instance.currentMoneyCount <= 0 || GameManager.instance.feelCount <= 0)
                {

                    SceneManager.LoadScene("游戏失败");

                    return;
                }
                xingQing.text = $"心情值：{GameManager.instance.feelCount}";

            }
        }
    }
}
