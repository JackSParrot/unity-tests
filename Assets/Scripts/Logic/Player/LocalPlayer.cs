using System;
using System.Collections.Generic;

public class LocalPlayer : IPlayer
{
    public int Id { get; private set; }
    Action<int, int> _makeMoveCallback;
    Board _board = null;

    public LocalPlayer(List<CellView> cells)
    {
        for (int i = 0; i < cells.Count; ++i)
        {
            cells[i].SetTouchedCallback(OnCellPressed);
        }
    }

    public void Initialize(int id, Board board, Action<int, int> makeMoveCallback)
    {
        Id = id;
        _makeMoveCallback = makeMoveCallback;
        _board = board;
    }

    public void StartTurn()
    {

    }

    void OnCellPressed(int cellId)
    {
        if(_board.CurrentPlayer == Id)
        {
            _makeMoveCallback?.Invoke(Id, cellId);
        }
    }
}
