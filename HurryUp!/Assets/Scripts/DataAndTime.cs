using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HurryUp 
{

    public class DataAndTime : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText;


        public void UpdateTimeTexT(string timer,string data) 
        {
            timeText.text = data + " " + timer;
        }


    }
}

