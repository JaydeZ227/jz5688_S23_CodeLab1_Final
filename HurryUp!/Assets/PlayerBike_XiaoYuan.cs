using HurryUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBike_XiaoYuan : MonoBehaviour
{
    //ƽ��
    float curRotateZ = 0;
    [Header("ˤ����ָ�ʱ��")]
    [SerializeField] float shuaidaoTime = 2;
    bool isShuaiDaoReply=false;
    IEnumerator shuaidaoIE()
    {
        dirChangeRobot.stopLayer++;
        rightShow.SetActive(false);
        leftShow.SetActive(false);
        isShuaiDaoReply = true;
        yield return new WaitForSeconds(shuaidaoTime);
        isShuaiDaoReply = false;
        dirChangeRobot.stopLayer--;
        //�ع�����״̬
        curRotateZ = 0;
        rotateTarget.rotation = Quaternion.Euler(0, 0, curRotateZ);
        dirChangeRobot.SetNone();
        dirChangeRobot.SetNextDir();

    }
    [SerializeField]  Transform rotateTarget;
    [Header("��ƽ�����ת�ٶ�")]
    [SerializeField]  float rotateSpeed = 180;
    [Header("��ƽ��İ���ٶ�")]
    [SerializeField]  float backRotateSpeed = 360;
    [Header("��ƽ��������")]
    [SerializeField]  float minRotate = -130;
    [Header("��ƽ������Ҷ�")]
    [SerializeField]  float maxRotate = 130;
    [SerializeField]  DirChangeRobot dirChangeRobot;
    [SerializeField]  GameObject rightShow;
    [SerializeField]  GameObject leftShow;
    //���
    [SerializeField] float biandao_SpeedX = 20f;
    bool isBianDao = false;
    float targetX_BianDao = 0;
    public void MovePosition(float x)
    {


        targetX_BianDao = x;
        isBianDao = true;
    }

    //�ٶ�
    [Header("Ĭ�ϵ��ƶ��ٶ�")]
    [SerializeField] float baseMove = 10;
    [Header("ɲ�����ƶ�����")]
    [SerializeField] float StopMovePower = 0;
    [Header("���ٵ��ƶ�����")]
    [SerializeField] float RunMovePower = 2.5f;
    float curPower = 1;
    float targetPower = 1;
    [Header("��������")]
    [SerializeField] float ChangeMovePowerSpeed = 10f;
    public int stopLayer = 0;
    private void Update()
    {
        if (stopLayer>0)
        {
            return;
        }
        if (isShuaiDaoReply)
        {
            return;
        }
        #region ƽ��
        {
            //ƽ���������ʾ
            rightShow.SetActive(false);
            leftShow.SetActive(false);
            switch (dirChangeRobot.getDir())
            {
                case Dir.NONE:
                    break;
                case Dir.LEFT:
                    {
                        rightShow.SetActive(true);
                    }
                    break;
                case Dir.RIGHT:
                    {
                        leftShow.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (dirChangeRobot.getDir() == Dir.RIGHT)
                {
                    dirChangeRobot.SetNone();
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (dirChangeRobot.getDir() == Dir.LEFT)
                {
                    dirChangeRobot.SetNone();
                }
            }

            switch (dirChangeRobot.getDir())
            {
                case Dir.NONE:
                    {
                        float targetZ = 0;
                        int dir = (int)Mathf.Sign(targetZ - curRotateZ);
                        float rotateDistance = backRotateSpeed * Time.deltaTime;
                        float distance = Mathf.Abs(curRotateZ - targetZ);
                        if (rotateDistance < distance)
                        {
                            curRotateZ += dir * rotateDistance;

                        }
                        else
                        {
                            curRotateZ = targetZ;
                            if (!dirChangeRobot.isNext)
                            {
                                dirChangeRobot.SetNextDir();
                            }
                        }
                    }
                    break;
                case Dir.RIGHT:
                    {
                        curRotateZ -= rotateSpeed * Time.deltaTime;
                        if (curRotateZ < minRotate)
                        {
                            curRotateZ = minRotate;
                            StartCoroutine(shuaidaoIE());
                        }
                    }
                    break;
                case Dir.LEFT:
                    {
                        curRotateZ += rotateSpeed * Time.deltaTime;
                        if (curRotateZ > maxRotate)
                        {
                            curRotateZ = maxRotate;
                            StartCoroutine(shuaidaoIE());
                        }
                    }
                    break;
                default:
                    break;
            }
            rotateTarget.rotation = Quaternion.Euler(0, 0, curRotateZ);
        }
        #endregion
        #region ���
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                BikeGameManager.instance.MovePlayerToLeft();
                Debug.Log("���");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                BikeGameManager.instance.MovePlayerToRight();
                Debug.Log("���");
            }
            if (isBianDao )
            {
                float biandaoDistance = Mathf.Abs(targetX_BianDao - transform.position.x);
                float biandaoChange = biandao_SpeedX * Time.deltaTime;
                int dir = (int)Mathf.Sign(targetX_BianDao - transform.position.x);
                if (biandaoChange< biandaoDistance)
                {
                    Vector3 pos = transform.position;
                    pos.x += dir * biandaoChange;
                    transform.position = pos;
                }
                else 
                {
                    Vector3 pos = transform.position;
                    pos.x = targetX_BianDao;
                    transform.position = pos;
                    isBianDao = false;
                }
            }
        }
        #endregion
        #region �ٶ��ƶ�
        {
            targetPower = 1;
            if (Input.GetKey(KeyCode.W))
            {
                targetPower = RunMovePower;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                targetPower = StopMovePower;
            }
            float powerDistance=Mathf.Abs(targetPower -curPower);
            float powerChange = ChangeMovePowerSpeed * Time.deltaTime;
            int dir = (int)Mathf.Sign(targetPower - curPower);
            if (powerDistance> powerChange)
            {
                curPower += dir * powerChange;

            }
            else 
            {
                curPower = targetPower;

            }
            transform.position += baseMove * curPower * Time.deltaTime*transform.forward;
        }
        #endregion
    }
   
}
