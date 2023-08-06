using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _savedSession;

        private void Awake()
        {
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                SaveSession();
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return true;
            }

            return false;
        }

        public void SaveSession()
        {
            _savedSession = _data.Clone();
        }

        public void LoadLastSession()
        {
            _data = _savedSession.Clone();
        }
    }
}