using HurryUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBike_XiaoYuan : MonoBehaviour
{
    //平衡
    float curRotateZ = 0;
    [Header("摔倒后恢复时间")]
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
        //回归正常状态
        curRotateZ = 0;
        rotateTarget.rotation = Quaternion.Euler(0, 0, curRotateZ);
        dirChangeRobot.SetNone();
        dirChangeRobot.SetNextDir();

    }
    [SerializeField]  Transform rotateTarget;
    [Header("不平衡的旋转速度")]
    [SerializeField]  float rotateSpeed = 180;
    [Header("不平衡的扳回速度")]
    [SerializeField]  float backRotateSpeed = 360;
    [Header("不平衡的最左端")]
    [SerializeField]  float minRotate = -130;
    [Header("不平衡的最右端")]
    [SerializeField]  float maxRotate = 130;
    [SerializeField]  DirChangeRobot dirChangeRobot;
    [SerializeField]  GameObject rightShow;
    [SerializeField]  GameObject leftShow;
    //变道
    [SerializeField] float biandao_SpeedX = 20f;
    bool isBianDao = false;
    float targetX_BianDao = 0;
    public void MovePosition(float x)
    {


        targetX_BianDao = x;
        isBianDao = true;
    }

    //速度
    [Header("默认的移动速度")]
    [SerializeField] float baseMove = 10;
    [Header("刹车的移动倍率")]
    [SerializeField] float StopMovePower = 0;
    [Header("加速的移动倍率")]
    [SerializeField] float RunMovePower = 2.5f;
    float curPower = 1;
    float targetPower = 1;
    [Header("变速速率")]
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
        #region 平衡
        {
            //平衡操作与提示
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
        #region 变道
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                BikeGameManager.instance.MovePlayerToLeft();
                Debug.Log("变道");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                BikeGameManager.instance.MovePlayerToRight();
                Debug.Log("变道");
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
        #region 速度移动
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
