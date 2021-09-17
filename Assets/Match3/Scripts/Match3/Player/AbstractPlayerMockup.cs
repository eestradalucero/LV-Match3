using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Player;
using Match3.Scripts.API.Match3.View;
using Match3.Scripts.Match3.View;
using UnityEngine;

namespace Match3.Scripts.Match3.Player
{
    public abstract class AbstractPlayerMockup : MonoBehaviour, IPlayer
    {
#pragma warning disable 649
        [SerializeField] private BoardVisualizerMockup boardVisualizerMockup;
#pragma warning restore 649

        #region Properties
        public IBoard Board { get; private set; }
        public IBoardVisualizer BoardVisualizer => boardVisualizerMockup;
        public PlayerState PlayerState { get; private set; }
        public int? DesiredMovement { get; private set; }
        public int? FilledLine { get; private set; } = null;

        #endregion

        public virtual void SetupPlayer(int rows, int columns)
        {
            Board = new Board(rows, columns);
            BoardVisualizer.FeedBoard(Board);
            BoardVisualizer.UpdateView();
            PlayerState = PlayerState.SelectingPiece;
        }

        public abstract void ProcessPieceSelection(PieceType currentPieceType);
        public void SetDesiredPiece(int row)
        {
            DesiredMovement = row;
            PlayerState = PlayerState.SelectedPiece;
        }

        public void SetFilledRow(int row)
        {
            FilledLine = row;
        }

        public void ResetFilledRow()
        {
            FilledLine = null;
        }

        public void ResetPieceSelection()
        {
            DesiredMovement = null;
            PlayerState = PlayerState.SelectingPiece;
        }
    }
}
