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

        public TrainMoveType currentTrainType = TrainMoveType.ֹͣ��;

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

                    SceneManager.LoadScene("��Ϸʧ��");

                    return;
                }
                xingQing.text = $"����ֵ��{GameManager.instance.feelCount}";

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
                    Debug.Log("����");
                }
            }


            switch (currentTrainType)
            {
                case TrainMoveType.��ʻ��:

                    if (!xingShiZhongBar.gameObject.activeSelf)
                    {
                        shaCheBar.gameObject.SetActive(false);
                        tingZhiBar.gameObject.SetActive(false);
                        xingShiZhongBar.gameObject.SetActive(true);

                        StartCoroutine(WaitTimeToChangeBalance());
                    }

                    break;
                case TrainMoveType.ɲ��:

                    if (!shaCheBar.gameObject.activeSelf)
                    {
                        StopAllCoroutines();

                        shaCheBar.gameObject.SetActive(true);
                        tingZhiBar.gameObject.SetActive(false);
                        xingShiZhongBar.gameObject.SetActive(false);

                        shaCheBar.InitBar();
                    }
                    break;
                case TrainMoveType.ֹͣ��:

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
                //TODO ���������任
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
        ��ʻ��,
        ɲ��,
        ֹͣ��,
        ���복��,
        ��������
    }

}
