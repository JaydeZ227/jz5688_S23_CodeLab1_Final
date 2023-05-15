using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class AppClock : MonoBehaviour
    {
        [SerializeField] Image healthSlider;
        [SerializeField] TMP_Text xingQin;
        [SerializeField] DataAndTime dayAndTime;

        private void Start()
        {
            if (PhoneController.instance == null)
            {
                healthSlider.fillAmount = GameManager.instance.healthCount / 100.0f;
                xingQin.text = GameManager.instance.feelCount.ToString();
                var data = GameManager.instance.currentDay.GetDayContent();
                dayAndTime.UpdateTimeTexT("22:30", data);
            }
        }


        public void SixHalf() 
        {
            GameManager.instance.alarmClockType = WakeUpTime.Áùµã°ë;
            if (PhoneController.instance != null)
            {
                PhoneController.instance.HomeButtton();
            }

        }

        public void Seven() 
        {
            GameManager.instance.alarmClockType = WakeUpTime.Æßµã;
            if (PhoneController.instance != null)
                PhoneController.instance.HomeButtton();
        }

        public void SevenHalf()
        {
            GameManager.instance.alarmClockType = WakeUpTime.Æßµã°ë;
            if (PhoneController.instance != null)
                PhoneController.instance.HomeButtton();
        }

    }

}
