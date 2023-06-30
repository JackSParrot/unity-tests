using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class OfflineMenuPopupView : MonoBehaviour
    {
        public class Config
        {
            public string PrefabName => "OfflineMenuPopup";
            public Action<bool, bool> OnStartWithPlayersAI;

            public Config(Action<bool, bool> startCallback)
            {
                OnStartWithPlayersAI = startCallback;
            }
        }

        [SerializeField]
        private Image _player1AIBG = null;
        [SerializeField]
        private Image _player2AIBG = null;
        [SerializeField]
        private Image _player1HumanBG = null;
        [SerializeField]
        private Image _player2HumanBG = null;
        [SerializeField]
        private Sprite _activeSprite = null;
        [SerializeField]
        private Sprite _inactiveSprite = null;

        private Config _popupConfig = null;
        private bool   _isPlayer1AI = false;
        private bool   _isPlayer2AI = true;

        public void Initialize(Config config)
        {
            _popupConfig = config;
            _isPlayer1AI = PlayerPrefs.GetInt("AI1", 0) > 0;
            _isPlayer2AI = PlayerPrefs.GetInt("AI2", 1) > 0;
            UpdatePlayerAIImages();
        }

        public void OkClicked()
        {
            _popupConfig.OnStartWithPlayersAI(_isPlayer1AI, _isPlayer2AI);
            Destroy(gameObject);
        }

        private void UpdatePlayerAIImages()
        {
            _player1HumanBG.sprite = !_isPlayer1AI ? _activeSprite : _inactiveSprite;
            _player1AIBG.sprite = _isPlayer1AI ? _activeSprite : _inactiveSprite;
            _player2HumanBG.sprite = !_isPlayer2AI ? _activeSprite : _inactiveSprite;
            _player2AIBG.sprite = _isPlayer2AI ? _activeSprite : _inactiveSprite;
        }

        public void SetPlayer1AI(bool isAI)
        {
            _isPlayer1AI = isAI;
            UpdatePlayerAIImages();
            PlayerPrefs.SetInt("AI1", isAI ? 1 : 0);
        }

        public void SetPlayer2AI(bool isAI)
        {
            _isPlayer2AI = isAI;
            UpdatePlayerAIImages();
            PlayerPrefs.SetInt("AI2", isAI ? 1 : 0);
        }
    }
}