using System;
using System.Collections.Generic;

public class AIPlayer : IPlayer
{
    public int Id { get; private set; }
    Action<int, int> _makeMoveCallback;
    Board            _board;

    public void Initialize(int id, Board board, Action<int, int> makeMoveCallback)
    {
        Id = id;
        _board = board;
        _makeMoveCallback = makeMoveCallback;
    }

    public void StartTurn()
    {
        if (!_board.IsGameOver && _board.CurrentPlayer == Id)
        {
            _makeMoveCallback?.Invoke(Id, GetBestMove(Id));
        }
    }
    

    private int GetBestMove(int player)
    {
        List<int> board = _board.ToIntList();
        List<int> availableMoves = Board.GetAvailableMoves(board);
        int maxScore = int.MinValue;
        int bestMove = -1;
        foreach (int available in availableMoves)
        {
            board[available] = player;
            var score = MiniMax(board, player, false);
            if (score > maxScore)
            {
                maxScore = score;
                bestMove = available;
            }

            board[available] = -1;
        }

        return bestMove;
    }

    private int MiniMax(List<int> board, int player, bool maxing = true)
    {
        int opponent = player > 0 ? 0 : 1;
        bool won = Board.CheckWinner(player, board);
        bool lost = Board.CheckWinner(opponent, board);
        if (lost || won)
        {
            return won ? 1 : -1;
        }

        List<int> availableMoves = Board.GetAvailableMoves(board);
        if (availableMoves.Count < 1)
        {
            return 0;
        }

        var tmpBoard = new List<int>(board);
        int maxScore = int.MinValue;
        int minScore = int.MaxValue;
        int playing = maxing ? player : opponent;
        foreach (int available in availableMoves)
        {
            tmpBoard[available] = playing;
            var tmpScore = MiniMax(tmpBoard, player, !maxing);
            tmpBoard[available] = -1;
            maxScore = UnityEngine.Mathf.Max(tmpScore, maxScore);
            minScore = UnityEngine.Mathf.Min(tmpScore, minScore);
        }

        return maxing ? maxScore : minScore;
    }
}