using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HurryUp 
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text healthUI;

        public void UpdateTmpHealth(float health) 
        {
            healthUI.text = $"ÃÂ¡¶÷µ£∫{health}";
        }
    }

}
