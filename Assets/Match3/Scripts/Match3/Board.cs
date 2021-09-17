using Match3.Scripts.API.Match3;

namespace Match3.Scripts.Match3
{
    public class Board : IBoard
    {
        private readonly PieceType[,] board;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            board = new PieceType[Rows, Columns];
            ResetBoard();
        }
        public PieceType GetPieceInCoordinate(int row, int column) => IsOutOfRange(row, column) ? PieceType.None : board[row, column];
        public bool IsOutOfRange(int row, int column) => row < 0 || row >= board.GetLength(0) || column < 0 || column >= board.GetLength(1);
        public bool IsCoordinateMatchingPiece(PieceType pieceType, int row, int column) => pieceType == GetPieceInCoordinate(row, column);

        public bool PlacePiece(PieceType pieceType, int row, int limitColumn = 0)
        {
            for (int j = board.GetLength(1) - 1; j >= limitColumn; j--)
            {
                if (board[row, j] == PieceType.None)
                {
                    board[row, j] = pieceType;
                    return true;
                }
            }
            return false;
        }

        public int GetUppermostColumnIndex(int row)
        {
            for (int j = board.GetLength(1) - 1; j >= 0; j--)
            {
                if (board[row, j] == PieceType.None)
                {
                    return j;
                }
            }
            return 0;
        }

        public void ClearPiece(int row, int column) => board[row, column] = PieceType.None;
        public void ClearRow(int row)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                ClearPiece(row, j);
            }
        }

        public bool IsRowFull(int row) => board[row, 0] != PieceType.None;

        public void RearrangeBoard()
        {
            for (int i = 0; i < board.GetLength(0); ++i)
            {
                for (int j = board.GetLength(1) - 1; j >= 0; --j)
                {
                    var pieceToCheck = board[i, j];
                    var wasPiecePlaced = PlacePiece(pieceToCheck, i, j);
                    if (wasPiecePlaced)
                    {
                        board[i, j] = PieceType.None;
                    }
                }
            }
        }

        public void ResetBoard()
        {
            for (int i = 0; i < board.GetLength(0); ++i)
            {
                for (int j = 0; j < board.GetLength(1); ++j)
                {
                    board[i, j] = PieceType.None;
                }
            }
        }
    }
}

