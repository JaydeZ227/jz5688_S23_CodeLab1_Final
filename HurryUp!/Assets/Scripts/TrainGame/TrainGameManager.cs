using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class TrainGameManager : MonoBehaviour
    {

        public static TrainGameManager instance;

        public TrainMoveType currentTrainType = TrainMoveType.停止后;

        public BalanceBarController xingShiZhongBar;
        public BalanceBarRedBlueGreen shaCheBar;
        public BalanceBarRedBlueGreen tingZhiBar;
        public TrainPlayerModelController trainPlayerModelController;

        [SerializeField] TMP_Text xingQing;
        [SerializeField] DataAndTime dayAndTime;
        string dayContent;

        float outSideSafeAreaTimer = 0f;


        public List<GameObject> twoStationMan;
        public List<GameObject> threeStartionMan;

        public MoveToNext currentMove;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.timer = GameManager.instance.beginTimer;
            }
        }

        private void Start()
        {
            dayContent = GameManager.instance.currentDay.GetDayContent();
            GameManager.instance.timer = GameManager.instance.beginTimer;

            dayAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dayContent);
            StartCoroutine(BeginTimer());

            foreach (var item in twoStationMan)
            {
                item.gameObject.SetActive(false);
            }

            foreach (var item in threeStartionMan)
            {
                item.gameObject.SetActive(false);
            }
        }

        IEnumerator BeginTimer()
        {
            while (true)
            {

                yield return new WaitForSeconds(1f);

                GameManager.instance.timer += 60f;
                dayAndTime.UpdateTimeTexT(GameTime.GetTimeContent(GameManager.instance.timer), dayContent);
            }
        }

        private void OnDestroy()
        {
            instance = null;
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


            if (xingShiZhongBar.CheckIsInController())
            {
                outSideSafeAreaTimer = 0f;
            }
            else 
            {
                outSideSafeAreaTimer += Time.deltaTime;

                if (outSideSafeAreaTimer >= 2f)
                {
                    Debug.Log("跌倒");
                }
            }


            switch (currentTrainType)
            {
                case TrainMoveType.行驶中:

                    if (!xingShiZhongBar.gameObject.activeSelf)
                    {
                        shaCheBar.gameObject.SetActive(false);
                        tingZhiBar.gameObject.SetActive(false);
                        xingShiZhongBar.gameObject.SetActive(true);

                        StartCoroutine(WaitTimeToChangeBalance());
                    }

                    break;
                case TrainMoveType.刹车:

                    if (!shaCheBar.gameObject.activeSelf)
                    {
                        StopAllCoroutines();

                        shaCheBar.gameObject.SetActive(true);
                        tingZhiBar.gameObject.SetActive(false);
                        xingShiZhongBar.gameObject.SetActive(false);

                        shaCheBar.InitBar();
                    }
                    break;
                case TrainMoveType.停止后:

                    if (!tingZhiBar.gameObject.activeSelf)
                    {
                        StopAllCoroutines();

                        shaCheBar.gameObject.SetActive(false);
                        tingZhiBar.gameObject.SetActive(true);
                        xingShiZhongBar.gameObject.SetActive(false);

                        tingZhiBar.InitBar();
                    }
                    break;
            }

        }

        IEnumerator WaitTimeToChangeBalance() 
        {
            while (true)
            {

                var randomIndex = Random.Range(0, 2);
                //TODO 根据天气变换
                float moveTime;
                moveTime = Random.Range(2f, 3f);

                switch (randomIndex)
                {
                    case 0:
                        currentMove = MoveToNext.Left;
                        xingShiZhongBar.MoveToNext(moveTime, MoveToNext.Left);
                        break;
                    default:
                        currentMove = MoveToNext.Right;
                        xingShiZhongBar.MoveToNext(moveTime, MoveToNext.Right);
                        break;
                }

                yield return new WaitForSeconds(moveTime);
            }
        }


        public void ShaCheRed() 
        {
        
        }

        public void ShaCheBlue() 
        {
        
        }

        public void TingZhiRed() 
        {
            
        }

        public void TingZhiBlue() 
        {
        
        }

    }

    public enum TrainMoveType 
    {
        行驶中,
        刹车,
        停止后,
        进入车厢,
        挤出车厢
    }

}
