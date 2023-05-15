using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class BikeGameManager : MonoBehaviour
    {
        public float TriggerTime = 1f;
        IEnumerator timeIE() 
        {
            Time.timeScale = 0.06f;
            yield return new WaitForSecondsRealtime(TriggerTime);
            Time.timeScale = 1f;
            
        }
        Coroutine cor;
        public void TriggerTimeStop() 
        {
            if (cor!=null)
            {
                StopCoroutine(cor);
            }
            cor = StartCoroutine(timeIE());
        }
             
        public static BikeGameManager instance;

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

        [SerializeField] List<Transform> movePoint = new List<Transform>();

        public PlayerBike player;
        public PlayerBike_XiaoYuan playerBike;

        [SerializeField] private int offset = 0;

        private int passTime = 0;

        string dataContent;


        //-------UI------
        [SerializeField] TMP_Text xinQing;
        [SerializeField] DataAndTime dataAndTime;

        public int zhuangCount = 0;
        private void Start()
        {
            dataContent = GameManager.instance.currentDay.GetDayContent();
            GameManager.instance.timer = GameManager.instance.beginTimer;

            dataAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dataContent);
            //StartCoroutine(BeginTimer());
        }

        IEnumerator BeginTimer() 
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                GameManager.instance.timer += 60f;
                dataAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dataContent);
            }
        }


        private void Update()
        {
            if (GameManager.instance != null)
            {

                xinQing.text = $"心情值:{GameManager.instance.feelCount}";

                if (GameManager.instance.feelCount == 0 || GameManager.instance.currentMoneyCount <= 0)
                {
                    SceneManager.LoadScene("游戏失败");
                }
            }
        }

        public void MovePlayerToRight() 
        {
            if (offset <= 0)
            {
                return;
            }

            offset--;

           
            player.MovePosition(movePoint[offset].position.x);
            playerBike.MovePosition(movePoint[offset].position.x);
        }

        public void MovePlayerToLeft() 
        {
            if (offset >= movePoint.Count - 1)
            {
                return;
            }
            offset++;

            player.MovePosition(movePoint[offset].position.x);
            playerBike.MovePosition(movePoint[offset].position.x);
        }


        public void PassTrafficLight() 
        {
            passTime++;

            if (passTime >= 5)
            {
                GameWin();
            }

        }


        public void GameWin() 
        {
            GameManager.instance.AddMoney(new IncomeInfo(100f, IncomeType.工资));

            if (GameManager.instance.timer > 28800)
            {

                GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.迟到));

                GameManager.instance.feelCount -= 2;

                if (GameManager.instance.feelCount <= 0)
                {
                    SceneManager.LoadScene("游戏失败");
                }
                else 
                {
                    SceneManager.LoadScene("结算");
                }
            }
            else 
            {
                SceneManager.LoadScene("结算");
            }


        }

        public void GameFail() 
        {
            SceneManager.LoadScene("游戏失败");
        }

        public void PauseGame() 
        {
            Time.timeScale = 0;
        }

        public void ContinueGame() 
        {
            Time.timeScale = 1;
        }
    }

}

