using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts.API.Match3.Control
{
    public interface IBoardChecker
    {
        bool AreNeightboorsMatched(IBoard board, int startRow, int startCol, UnidirectionalDirection direction, int steps,
            PieceType pieceType);

        bool AreNeightboorsMatched(IBoard board, int startRow, int startCol, BidirectionalDirection direction, int steps,
            PieceType pieceType);

        List<Vector2Int> GetCoordinateSet(IBoard board, int row, int col, UnidirectionalDirection unidirection, int steps);
        List<Vector2Int> GetCoordinateSet(IBoard board, int row, int col, BidirectionalDirection bidirectionalDirection, int steps);
    }
}
