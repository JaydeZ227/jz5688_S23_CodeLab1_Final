using HurryUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosToPlayerUI : MonoBehaviour
{
    [SerializeField] Camera uiCamera;
    Transform player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerBike_XiaoYuan>().transform;
    }
    void RequirePos() 
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 uiPos=uiCamera.ScreenToWorldPoint(pos);
      
        transform.position = uiPos;
        Vector3 p2 = transform.localPosition;
        p2.z = 0;
        transform.localPosition = p2;

    }
    // Start is called before the first frame update
    void Start()
    {
        RequirePos();
    }

    // Update is called once per frame
    void Update()
    {
        RequirePos();
    }
}
