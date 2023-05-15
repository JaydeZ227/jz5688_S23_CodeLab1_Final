using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{

    public class PlayerBike : MonoBehaviour
    {
        [SerializeField] BalanceBarController balanceController;
        [SerializeField] Transform bikeModel;
        Rigidbody myRigidbody;

        public float beginNormalSpeed = 5f;

        private float speed;

        public float decelerateSpeed = 2f;
        public float speedUpPower = 2.0f;
        bool isUp = false;

        public float restoreSpeed = 20f;
        public float maxNotBalanceTime = 2f;

        private bool isMoveRightOrLeft =false;

        private MoveToNext currentMoveDir;

        private bool isWait = false;
        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();

        }

        private void Start()
        {
            beginNormalSpeed *= GameManager.instance.feelCount / 8.0f;
            speed = beginNormalSpeed;
        }

        public GameObject leftShow, rightShow;
        public void Update()
        {
            leftShow.SetActive(false);
            rightShow.SetActive(false);
            if (isWait)
            {
                return;
            }
            //switch (balanceController.GetDir())
            //{
            //    case 0: //平衡
            //        {

            //        }
            //        break ;
            //    case -1: //左
            //        {
            //            rightShow.SetActive(true);
            //        }
            //        break;
            //    case 1://右
            //        {
            //            leftShow.SetActive(true);
            //        }
            //        break;
            //    default:
            //        break;
            //}
            if (balanceController.CheckIsInController())
            {

            }
            else 
            {
                switch (currentMoveDir)
                {
                    case MoveToNext.Left:
                        {
                            leftShow.SetActive(true);
                        }
                        break;
                    case MoveToNext.Right:
                        {
                            rightShow.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }
          

            if (!isMoveRightOrLeft)
            {
                var randomIndex = Random.Range(0,2);
                //TODO 根据天气变换
                float moveTime;
                moveTime = Random.Range(1.5f,3f);

                switch (randomIndex)
                {
                    case 0:
                        isMoveRightOrLeft = true;
                        currentMoveDir = MoveToNext.Left;
                        balanceController.MoveToNext(moveTime,MoveToNext.Left, () => {
                            isMoveRightOrLeft = false;
                        });
                        break;
                    default:
                        isMoveRightOrLeft = true;
                        currentMoveDir = MoveToNext.Right;
                        balanceController.MoveToNext(moveTime, MoveToNext.Right, () => {
                            isMoveRightOrLeft = false;
                        });
                        break;
                }
            }


            if (balanceController.CheckIsInController())
            {
                var currentLocalRotate = bikeModel.transform.localRotation.eulerAngles;

                if (currentLocalRotate.z != 0 || currentLocalRotate.z != 360)
                {

                    float aimRotatZ = 0;

                    if (currentLocalRotate.z >= 320f)
                    {
                        aimRotatZ = Mathf.Lerp(currentLocalRotate.z, 360f, Time.deltaTime * restoreSpeed);
                    }
                    else if (currentLocalRotate.z <= 45f) 
                    {
                        aimRotatZ = Mathf.Lerp(currentLocalRotate.z, 0f, Time.deltaTime * restoreSpeed);
                    }

                    bikeModel.transform.localRotation = Quaternion.Euler(0,0, aimRotatZ);
                }

            }
            else 
            {
                var currentLocalRotate = bikeModel.transform.localRotation.eulerAngles;

                if (currentLocalRotate.z < 320f && currentLocalRotate.z > 45f) 
                {
                    ShuaiDao();
                    return;
                }

                if (currentMoveDir != MoveToNext.Left)
                {

                    bikeModel.transform.localRotation = Quaternion.Euler(0,0, currentLocalRotate.z - Time.deltaTime * 20f);

                }
                else 
                {
                    bikeModel.transform.localRotation = Quaternion.Euler(0, 0, currentLocalRotate.z + Time.deltaTime * 20f);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {

                if (Input.GetKeyDown(KeyCode.A))
                {
                    BikeGameManager.instance.MovePlayerToLeft();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    BikeGameManager.instance.MovePlayerToRight();
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //减速
                speed = decelerateSpeed;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                //恢复初始速度
                speed = beginNormalSpeed;
            }
            //判断加速
            isUp = Input.GetKey(KeyCode.E);
            
        }

        private void ShuaiDao() 
        {
            Debug.Log("摔倒");
            myRigidbody.velocity = Vector3.zero;
            StartCoroutine(WaitTimeToGo(2f));
        }

        IEnumerator WaitTimeToGo(float waitTime) 
        {
            isWait = true;
            yield return new WaitForSeconds(waitTime);

            isWait = false;
        }


        private void FixedUpdate()
        {
            if (isWait)
            {
                return;
            }

            myRigidbody.velocity = transform.forward* Time.fixedDeltaTime * speed * (isUp ? speedUpPower : 1);
        }

        public void MovePosition(float x) 
        {
            myRigidbody.MovePosition(new Vector3(x, transform.position.y, transform.position.z)) ;
        }
    }

}
