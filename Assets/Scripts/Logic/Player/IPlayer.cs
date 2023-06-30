using System;

public interface IPlayer
{
    int Id { get; }
    void Initialize(int id, Board board, Action<int, int> makeMoveCallback);
    void StartTurn();
}