using Match3.Scripts.API.Match3.View;

namespace Match3.Scripts.API.Match3.Player
{
    public interface IPlayer
    {
        IBoard Board { get; }
        IBoardVisualizer BoardVisualizer { get; }
        PlayerState PlayerState { get; }
        int? DesiredMovement { get; }
        int? FilledLine { get; }
        void SetupPlayer(int rows, int columns);
        void ProcessPieceSelection(PieceType currentPieceType);
        void SetDesiredPiece(int row);
        void SetFilledRow(int row);
        void ResetFilledRow();
        void ResetPieceSelection();
    }
}

