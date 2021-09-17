namespace Match3.Scripts.API.Match3.Data.AI
{
    public interface IBestRowCalculator
    {
        int CalculateBestRow(IBoard board, PieceType nextPieceType);
        int GetRandomRow(IBoard board);
    }
}
