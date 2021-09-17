namespace Match3.Scripts.API.Match3
{
    public interface IBoard
    {
        int Rows { get; }
        int Columns { get; }
        PieceType GetPieceInCoordinate(int row, int column);
        bool IsOutOfRange(int row, int column);
        bool IsCoordinateMatchingPiece(PieceType pieceType, int row, int column);
        bool IsRowFull(int row);
        bool PlacePiece(PieceType pieceType, int row, int limitColumn = 0);
        int GetUppermostColumnIndex(int row);
        void ClearPiece(int row, int column);
        void ClearRow(int row);
        void RearrangeBoard();
        void ResetBoard();
    }
}