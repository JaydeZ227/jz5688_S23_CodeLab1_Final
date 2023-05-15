using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeTeacherPage : MonoBehaviour
{
    [SerializeField] Camera uiCamera;
    PlayerBike_XiaoYuan player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerBike_XiaoYuan>();
    }
    void Open() 
    {
        gameObject.SetActive(true);
    }   
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    public  void Close() 
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
