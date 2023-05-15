using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HurryUp 
{
    public class TrafficCondition : MonoBehaviour
    {

        [SerializeField] GameObject yongDuObject;
        [SerializeField] UnityEvent OnYongDu;

        [SerializeField] GameObject huanXingObject;
        [SerializeField] UnityEvent OnHuanXing;

        [SerializeField] GameObject changTongObject;
        [SerializeField] UnityEvent OnChangTong;

        private void Start()
        {
            switch (GameManager.instance.todayRoadSituation)
            {

                case RoadSituationType.Óµ¶Â:
                    yongDuObject.SetActive(true);
                    OnYongDu?.Invoke();
                    break;
                case RoadSituationType.»ºÐÐ:
                    huanXingObject.SetActive(true);
                    OnHuanXing?.Invoke();
                    break;
                case RoadSituationType.³©Í¨:
                    changTongObject.SetActive(true);
                    OnChangTong?.Invoke();
                    break;
                default:
                    break;
            }
        }


    }

}
