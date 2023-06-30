public class Cell
{
    public int Id;
    public int Owner;
    public int X => Id % _boardSize;
    public int Y => Id / _boardSize;
    int _boardSize = 0;

    public Cell(int id, int boardSize) { Id = id; Owner = -1; _boardSize = boardSize; }
}