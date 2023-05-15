using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HurryUp 
{
    public class LoadSceneManager : MonoBehaviour
    {

        public string nextSceneName = "��";

        public void GoToNextScene() 
        {
            SceneManager.LoadScene(nextSceneName);
        }

    }
}

