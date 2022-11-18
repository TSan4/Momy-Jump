using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MommyJump
{
    public class SceneController : Singleton<SceneController>
    {
        public override void Awake()
        {
            MakeSingleton(false);
        }

        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
