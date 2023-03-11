using Unity.MultiPlayerGame.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.MultiPlayerGame.Menu
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        void Update()
        {
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}