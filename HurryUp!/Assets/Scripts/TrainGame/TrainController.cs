using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class TrainController : MonoBehaviour
    {

        [SerializeField] Transform cheZhanPoint;
        [SerializeField] Transform movePoint1;
        [SerializeField] Transform movePoint2;

        [SerializeField] float moveDuration = 10f;
        [SerializeField] float waitTimeToStation = 4f;

        [SerializeField] Animator trainAnimator;

        private float timer = 0f;

        public int stationOffset = 0;

        [SerializeField] List<TrainAi> tranAi = new List<TrainAi>();

        [SerializeField] Transform aimPoint;

        private void Start()
        {
            transform.position = cheZhanPoint.position;
            timer = 0;

            StartCoroutine(WaitTimeToGo());
        }


        IEnumerator WaitTimeToGo() 
        {
            TrainGameManager.instance.currentTrainType = TrainMoveType.ֹͣ��;
            //����
            trainAnimator.SetBool("open", true);

            yield return new WaitForSeconds(1f);

            TrainGameManager.instance.trainPlayerModelController.StartEnterMetro();

            yield return new WaitForSeconds(15);

            trainAnimator.SetBool("open", false);

            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(1f);


            TrainGameManager.instance.trainPlayerModelController.FinishEnterMetro();

            //-----------��������----------

            for (int i = 0; i < 3; i++)
            {
                TrainGameManager.instance.currentTrainType = TrainMoveType.��ʻ��;

                float timer = 0;

                while (timer < moveDuration)
                {
                    timer += Time.deltaTime;

                    transform.position = Vector3.Lerp(cheZhanPoint.position, movePoint1.position, timer / moveDuration);

                    yield return null;
                }

                timer = 0;

                while (timer < moveDuration)
                {
                    timer += Time.deltaTime;

                    transform.position = Vector3.Lerp(movePoint2.position, cheZhanPoint.position, timer / moveDuration);

                    if (timer >= moveDuration * 0.2f)
                    {
                        TrainGameManager.instance.currentTrainType = TrainMoveType.ɲ��;


                    }

                    yield return null;
                }

                stationOffset++;


                //����
                trainAnimator.SetBool("open", true);

                yield return new WaitForSeconds(1f);

                TrainGameManager.instance.currentTrainType = TrainMoveType.ֹͣ��;


                if (stationOffset == 1)
                {
                    foreach (var item in tranAi)
                    {
                        item.MoveToAimPosition(aimPoint);
                    }
                }
                else if (stationOffset == 2)
                {
                    foreach (var item in tranAi)
                    {
                        item.MoveToAimPosition(aimPoint);
                    }
                }
                else if (stationOffset == 3) 
                {
                    TrainGameManager.instance.trainPlayerModelController.BeginExitMetro();
                    TrainGameManager.instance.shaCheBar.gameObject.SetActive(false);
                    TrainGameManager.instance.tingZhiBar.gameObject.SetActive(true);

                    TrainGameManager.instance.currentTrainType = TrainMoveType.��������;
                }

                yield return new WaitForSeconds(8f);

                if (stationOffset == 3)
                {

                    GameManager.instance.AddMoney(new IncomeInfo(100f,IncomeType.����));

                    if (GameManager.instance.timer > 28800)
                    {
                        GameManager.instance.AddMoney(new IncomeInfo(-20f, IncomeType.�ٵ�));
                    }

                    SceneManager.LoadScene("����");

                    yield return 0;
                }

                foreach (var item in tranAi)
                {
                    item.ResetPosition();
                }


                trainAnimator.SetBool("open", false);

                yield return new WaitForSeconds(2f);

            }

            stationOffset++;

            SceneManager.LoadScene("����");
        }



        public void Update()
        {
            if (TrainGameManager.instance.currentTrainType == TrainMoveType.ɲ��)
            {

                foreach (var item in tranAi)
                {
                    item.SwingToForword();
                }


                //if (TrainGameManager.instance.shaCheBar.isInBule)
                //{
                //    foreach (var item in tranAi)
                //    {
                //        item.SwingToForword();
                //    }
                //}
                //else if (TrainGameManager.instance.shaCheBar.isInRed)
                //{

                //}
                //else 
                //{
                //    foreach (var item in tranAi)
                //    {
                //        item.ResetRotation();
                //    }
                //}

            }
            else if (TrainGameManager.instance.currentTrainType == TrainMoveType.��ʻ��)
            {
                if (TrainGameManager.instance.currentMove == MoveToNext.Left)
                {
                    foreach (var item in tranAi)
                    {
                        item.SwingToLeft();
                    }
                }
                else
                {
                    foreach (var item in tranAi)
                    {
                        item.SwingToRight();
                    }
                }
            }
            else if (TrainGameManager.instance.currentTrainType == TrainMoveType.��������)
            {
                foreach (var item in tranAi)
                {
                    item.ResetRotation();
                }
            }
        }

    }



}
