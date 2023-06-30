using System;
using UnityEngine;

namespace Game.UI
{
    public class GameOverPopupView : MonoBehaviour
    {
        public class Config
        {
            public string PrefabName => "GameOverPopup";
            public Action OnCloseCallback;
            public int Winner;
            public Config(int winner, Action closeCallback)
            {
                Winner = winner;
                OnCloseCallback = closeCallback;
            }
        }

        [SerializeField] private GameObject _won1Text = null;
        [SerializeField] private GameObject _won2Text = null;
        [SerializeField] private GameObject _drawText = null;

        private Config _popupConfig = null;

        public void Initialize(Config config)
        {
            _popupConfig = config;
            _drawText.SetActive(_popupConfig.Winner == 0);
            _won1Text.SetActive(_popupConfig.Winner == 1);
            _won2Text.SetActive(_popupConfig.Winner == 2);
        }

        public void CloseClicked()
        {
            _popupConfig.OnCloseCallback();
            Destroy(gameObject);
        }
    }
}