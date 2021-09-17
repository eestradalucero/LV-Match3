using System.Collections.Generic;
using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Control;
using UnityEngine;

namespace Match3.Scripts.Match3.Control
{
    public class BoardChecker : IBoardChecker
    {
        public bool AreNeightboorsMatched(IBoard board, int startRow, int startCol, UnidirectionalDirection direction, int steps,
            PieceType pieceType) 
            => checkInDirection(board, startRow, startCol, direction, steps, pieceType);

        private bool checkInDirection(IBoard board, int row, int col, UnidirectionalDirection unidirectionalDirection, int steps, PieceType pieceType)
        {
            var direction = directionToVector(unidirectionalDirection);
            direction.x = direction.x == 0 ? 0 : 1 * (int)Mathf.Sign(direction.x);
            direction.y = direction.y == 0 ? 0 : 1 * (int)Mathf.Sign(direction.y);
            for (int i = 1; i <= steps; ++i)
            {
                var newRow = row + direction.x * i;
                var newCol = col + direction.y * i;

                if (board.IsOutOfRange(newRow, newCol)
                    || !board.IsCoordinateMatchingPiece(pieceType, newRow, newCol))
                    return false;
            }
            //Debug.Log($"Returning true on Unidirectional check: ({row},{col}) - {pieceType}, DX:{direction.x}, DY{direction.y}");
            return true;
        }
        
        Vector2Int directionToVector(UnidirectionalDirection direction)
        {
            switch (direction)
            {
                case UnidirectionalDirection.Right:
                    return Vector2Int.right;
                case UnidirectionalDirection.RightUp:
                    return Vector2Int.one;
                case UnidirectionalDirection.Up:
                    return Vector2Int.up;
                case UnidirectionalDirection.LeftUp:
                    return new Vector2Int(-1, 1);
                case UnidirectionalDirection.Left:
                    return Vector2Int.left;
                case UnidirectionalDirection.LeftDown:
                    return new Vector2Int(-1, -1);
                case UnidirectionalDirection.Down:
                    return Vector2Int.down;
                case UnidirectionalDirection.DownRight:
                    return new Vector2Int(1, -1);
                default:
                    return Vector2Int.zero;
            }
        }

        public bool AreNeightboorsMatched(IBoard board, int startRow, int startCol, BidirectionalDirection direction,
            int steps, PieceType pieceType)
            => checkSurrounding(board, startRow, startCol, direction, steps, pieceType);

        bool checkSurrounding(IBoard board, int row, int col, BidirectionalDirection bidirectionalDirection, int steps,
            PieceType pieceType)
        {
            var direction = directionToVector(bidirectionalDirection);
            direction.x = direction.x == 0 ? 0 : 1 * (int)Mathf.Sign(direction.x);
            direction.y = direction.y == 0 ? 0 : 1 * (int)Mathf.Sign(direction.y);
            for (int i = 1; i <= steps; ++i)
            {
                var rowRight = row + direction.x * i;
                var colRight = col + direction.y * i;
                var rowLeft = row - direction.x * i;
                var colLeft = col - direction.y * i;
                if (board.IsOutOfRange(rowRight, colRight) || board.IsOutOfRange(rowLeft, colLeft)
                                                           || !board.IsCoordinateMatchingPiece(pieceType, rowRight, colRight)
                                                           || !board.IsCoordinateMatchingPiece(pieceType, rowLeft, colLeft))
                    return false;
            }
            //Debug.Log($"Returning true on BidirectionalDirection check: ({row},{col}) - {pieceType}, DX:{direction.x}, DY{direction.y}");
            return true;
        }

        Vector2Int directionToVector(BidirectionalDirection direction)
        {
            switch (direction)
            {
                case BidirectionalDirection.Horizontal:
                    return Vector2Int.right;
                case BidirectionalDirection.DiagonalVertDown:
                    return new Vector2Int(1, -1);
                case BidirectionalDirection.DiagonalVertUp:
                    return new Vector2Int(1, 1);
                default:
                    return Vector2Int.zero;
            }
        }
        
        public List<Vector2Int> GetCoordinateSet(IBoard board, int row, int col, UnidirectionalDirection unidirection, int steps)
        {
            List<Vector2Int> coordinates = new List<Vector2Int>();
            var direction = directionToVector(unidirection);
            direction.x = direction.x == 0 ? 0 : 1 * (int)Mathf.Sign(direction.x);
            direction.y = direction.y == 0 ? 0 : 1 * (int)Mathf.Sign(direction.y);
            for (int i = 1; i <= steps; ++i)
            {
                var newRow = row + direction.x * i;
                var newCol = col + direction.y * i;
                if (!board.IsOutOfRange(newRow, newCol))
                {
                    coordinates.Add(new Vector2Int(newRow, newCol));
                }
            }
            return coordinates;
        }

        public List<Vector2Int> GetCoordinateSet(IBoard board, int row, int col, BidirectionalDirection bidirectionalDirection, int steps)
        {
            List<Vector2Int> coordinates = new List<Vector2Int>();
            var direction = directionToVector(bidirectionalDirection);
            direction.x = direction.x == 0 ? 0 : 1 * (int)Mathf.Sign(direction.x);
            direction.y = direction.y == 0 ? 0 : 1 * (int)Mathf.Sign(direction.y);
            for (int i = 1; i <= steps; ++i)
            {
                var rowRight = row + direction.x * i;
                var colRight = col + direction.y * i;
                var rowLeft = row - direction.x * i;
                var colLeft = col - direction.y * i;
                if (!board.IsOutOfRange(rowRight, colRight))
                {
                    coordinates.Add(new Vector2Int(rowRight, colRight));
                }

                if (!board.IsOutOfRange(rowLeft, colLeft))
                {
                    coordinates.Add(new Vector2Int(rowLeft, colLeft));
                }
            }
            return coordinates;
        }
    }
}
