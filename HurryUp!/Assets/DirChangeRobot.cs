using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Dir 
{
    NONE,
    LEFT,
    RIGHT,
    
}
public class DirChangeRobot : MonoBehaviour
{
    public Vector2 changeDirIntervalRange=new Vector2(2,3.0f);
    public float changeDirPower=1;
    float curChangeDirInterval;
    float changeTimer = 0;
    public int stopLayer = 0;
    [SerializeField]
    Dir thisDir;
    public void SetNone() 
    {
        thisDir = Dir.NONE;
    }
    public Dir getDir() 
    {
        return thisDir;
    }
    public bool isNext = false;
    public void SetNextDir() 
    {
        isNext = true;
        changeTimer = 0;
        curChangeDirInterval = Random.Range(changeDirIntervalRange.x, changeDirIntervalRange.y);
    }
    // Start is called before the first frame update
    void Start()
    {
       SetNextDir();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopLayer>0)
        {
            return;
        }
        if (isNext)
        {
            changeTimer += Time.deltaTime;
            if (changeTimer > curChangeDirInterval * changeDirPower)
            {
                //变化一次方向
                thisDir = Random.Range(0, 1000) > 500 ? Dir.LEFT : Dir.RIGHT;
                //重新计时
                changeTimer = 0;
                isNext = false;
            }
        }
        
    }
}
