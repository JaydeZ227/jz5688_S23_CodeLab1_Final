using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class PanelGameFail : MonoBehaviour
    {

        [SerializeField] string backSceneName;
        public void RestryGame() 
        {
            SceneManager.LoadScene(backSceneName);
        }

        public void CloseGame() 
        {
            Application.Quit();
        }

    }

}
