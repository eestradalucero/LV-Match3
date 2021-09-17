using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data.AI;
using Match3.Scripts.Match3.Data.AI;
using UnityEngine;

namespace Match3.Scripts.Match3.Player
{
    public class AIPlayer : AbstractPlayerMockup
    {
        [SerializeField] private PiecesValueScriptableObject _piecesValues;
        
        private int? possibleIndex = null;
        private IBestRowCalculator bestRowCalculator;
        private IPiecesValuesContainer piecesValues => _piecesValues;
        [SerializeField] [Range(0f, 1f)] private float messUpThreshold = 0f;
        
        public override void SetupPlayer(int rows, int columns)
        {
            base.SetupPlayer(rows, columns);
            bestRowCalculator = new BestRowCalculator(piecesValues);
        }

        public override void ProcessPieceSelection(PieceType currentPieceType)
        {
            possibleIndex = null;
            possibleIndex = bestRowCalculator.CalculateBestRow(Board, currentPieceType);
            if (shouldMessUp())
            {
                SetDesiredPiece(bestRowCalculator.GetRandomRow(Board));
                return;
            }
            SetDesiredPiece(possibleIndex.Value);
        }

        private bool shouldMessUp()
        {
            if (messUpThreshold == 0f) return false;
            var randomChanceToMessUp = Random.Range(0f, 1f);
            return randomChanceToMessUp < messUpThreshold;

        }
    }
}
