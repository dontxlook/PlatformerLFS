using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Model;

namespace Platformer.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            GameSession session = FindObjectOfType<GameSession>();
            session.SaveSession();
            SceneManager.LoadScene(_sceneName);

        }
    }
}
