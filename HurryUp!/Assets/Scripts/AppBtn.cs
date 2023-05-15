using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class AppBtn : MonoBehaviour
    {
        [SerializeField] Image normal;

        [SerializeField] Toggle toggle;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(ToggleMethod);
        }

        private void ToggleMethod(bool arg0)
        {
            normal.enabled = !arg0;
        }
    }
}
