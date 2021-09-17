using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.View;
using Match3.Scripts.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.View
{
    public class BoardVisualizerMockup : MonoBehaviour, IBoardVisualizer
    {
        #region EditorFields
        #pragma warning disable 649
        [SerializeField] private BoardPieceView boardPieceViewPrefab;
#pragma warning restore 649
        #endregion
        
        private IBoard board;
        private BoardPieceView[,] boardPieceViews;
        public void FeedBoard(IBoard newBoard)
        {
            board = newBoard;
            createBoardView();
            UpdateView();
        }

        private const string boardParentName = "boardParent";
        private void createBoardView()
        {
            boardPieceViews = new BoardPieceView[board.Rows, board.Columns];
            var parentPosition = transform.position;
            for (int i = 0; i < board.Rows; ++i)
            {
                for (int j = 0; j < board.Columns; ++j)
                {
                    var piece = Instantiate(boardPieceViewPrefab,
                    new Vector3(parentPosition.x + i, parentPosition.y + j, 0), Quaternion.identity, transform) as BoardPieceView;
                    piece.name = $"Piece ({i},{j})";
                    boardPieceViews[i, j] = piece;
                }
            }
        }

        public void UpdateView()
        {
            for (int i = 0; i < board.Rows; ++i)
            {
                for (int j = 0; j < board.Columns; ++j)
                {
                    boardPieceViews[i,j].SetColor(board.GetPieceInCoordinate(i,j));
                }
            }
        }
    }
}