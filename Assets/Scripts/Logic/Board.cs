using System.Collections.Generic;

public class Board
{
    public static implicit operator List<Cell>(Board b) => b._cells;
    public static implicit operator List<int>(Board b) => BoardToIntList(b._cells);

    public List<int> ToIntList() => BoardToIntList(_cells);

    public int Count => Size * Size;
    public int Size { get; }

    public int CurrentPlayer { get; private set; }
    public bool IsGameOver = false;
    public int? Winner     = null;

    private readonly List<Cell> _cells = new List<Cell>();


    public Cell this[int x, int y]
    {
        get { return _cells[y * Size + x]; }
        set { _cells[y * Size + x] = value; }
    }

    public Cell this[int id]
    {
        get { return _cells[id]; }
        set { _cells[id] = value; }
    }

    public Board(int size = 3)
    {
        Size = size;
        for (int i = 0; i < Count; ++i)
        {
            _cells.Add(new Cell(i, Size));
        }

        CurrentPlayer = 0;
    }

    public bool PlaceMove(int id)
    {
        if (_cells[id].Owner >= 0)
            return false;
        _cells[id].Owner = CurrentPlayer;
        CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
        Winner = GetWinner();
        List<int> availableMoves = GetAvailableMoves(BoardToIntList(_cells));
        IsGameOver = Winner.HasValue || availableMoves.Count < 1;
        return true;
    }

    int? GetWinner()
    {
        return GetWinner(BoardToIntList(_cells));
    }

    static int? GetWinner(List<int> board)
    {
        if (CheckWinner(0, board)) return 0;
        if (CheckWinner(1, board)) return 1;
        return null;
    }

    public static bool CheckWinner(int player, List<int> board)
    {
        return HasWon(player, board, UnityEngine.Mathf.CeilToInt(UnityEngine.Mathf.Sqrt(board.Count)));
    }

    public static bool HasWon(int player, List<int> board, int size)
    {
        //horizontals
        for (int y = 0; y < size; ++y)
        {
            int current = 0;
            for (int x = 0; x < size; ++x)
            {
                current += board[x + y * size] == player ? 1 : 0;
            }

            if (current > 2)
            {
                return true;
            }
        }

        //verticals
        for (int x = 0; x < size; ++x)
        {
            int current = 0;
            for (int y = 0; y < size; ++y)
            {
                current += board[x + y * size] == player ? 1 : 0;
            }

            if (current > 2)
            {
                return true;
            }
        }

        //topleft - bottomright
        int diagScore = 0;
        for (int i = 0; i < size; ++i)
        {
            diagScore += board[i + i * size] == player ? 1 : 0;
        }

        if (diagScore > 2)
        {
            return true;
        }

        // bottomleft - topright
        diagScore = 0;
        for (int i = 0; i < size; ++i)
        {
            diagScore += board[i + (size - (i + 1)) * size] == player ? 1 : 0;
        }

        return diagScore > 2;
    }

    public static List<int> GetAvailableMoves(List<int> board)
    {
        List<int> availableMoves = new List<int>();
        for (int i = 0; i < board.Count; ++i)
        {
            if (board[i] < 0)
            {
                availableMoves.Add(i);
            }
        }

        return availableMoves;
    }

    static List<int> BoardToIntList(List<Cell> cells)
    {
        List<int> retVal = new List<int>();
        for (int i = 0; i < cells.Count; ++i)
        {
            retVal.Add(cells[i].Owner);
        }

        return retVal;
    }
}