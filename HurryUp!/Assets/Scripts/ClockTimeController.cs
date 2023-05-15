using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class ClockTimeController : MonoBehaviour
    {

        [SerializeField] Image clockTime;


        public List<Sprite> allNumber = new List<Sprite>();

        public void UpdateTime(int number) 
        {

            if (number >= 0 && number <= 9) 
            {
                clockTime.sprite = allNumber[number];
            }
        }


    }

}
