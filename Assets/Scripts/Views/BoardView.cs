using System.Collections.Generic;
using Game.UI;
using UnityEngine.UI;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private List<CellView> _cells = new List<CellView>();
    [SerializeField]
    private TMPro.TMP_Text _playerText = null;
    [SerializeField]
    private Image _playerImage = null;
    [SerializeField]
    private Sprite[] _playerSprites = null;

    private List<IPlayer> _players = new List<IPlayer>();
    private Board         _board   = null;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        ShowOfflineMenu();
    }

    private void StartGame(bool isPlayer1AI, bool isPlayer2AI)
    {
        _board = new Board(3);
        _players.Clear();
        for (int i = 0; i < _cells.Count; ++i)
        {
            _cells[i].SetData(_board[i]);
            _cells[i].UpdateView();
        }

        _players.Add(isPlayer1AI ? new AIPlayer() : (new LocalPlayer(_cells) as IPlayer));
        _players.Add(isPlayer2AI ? new AIPlayer() : (new LocalPlayer(_cells) as IPlayer));
        _players[0].Initialize(0, _board, OnPlayerMove);
        _players[1].Initialize(1, _board, OnPlayerMove);
        UpdateTurn();
    }

    private void OnPlayerMove(int player, int move)
    {
        if (_board.CurrentPlayer == player && !_board.IsGameOver)
        {
            MakeMove(move);
        }
    }

    private void MakeMove(int id)
    {
        if (!_board.PlaceMove(id))
            return;
        _cells[id].UpdateView();
        UpdateTurn();
    }

    private void UpdateTurn()
    {
        var playerText = "PLAYER_" + _board.CurrentPlayer.ToString();
        _playerImage.sprite = _playerSprites[_board.CurrentPlayer];
        _playerText.text = playerText;
        foreach (var player in _players)
        {
            player.StartTurn();
        }

        if (_board.IsGameOver)
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        var config =
            new GameOverPopupView.Config(_board.Winner.HasValue ? (_board.Winner.Value + 1) : 0, ShowOfflineMenu);
        Instantiate(Resources.Load<GameOverPopupView>(config.PrefabName)).Initialize(config);
    }

    private void ShowOfflineMenu()
    {
        var config = new OfflineMenuPopupView.Config(StartGame);
        Instantiate(Resources.Load<OfflineMenuPopupView>(config.PrefabName)).Initialize(config);
    }
}