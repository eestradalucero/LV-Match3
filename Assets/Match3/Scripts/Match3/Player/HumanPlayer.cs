using Match3.Scripts.API.Match3;
using UnityEngine;

namespace Match3.Scripts.Match3.Player
{
    public class HumanPlayer : AbstractPlayerMockup
    {
        [SerializeField] KeyCode space1Key;
        [SerializeField] KeyCode space2Key;
        [SerializeField] KeyCode space3Key;

        public override void ProcessPieceSelection(PieceType currentPieceType)
        {
            if (Input.GetKeyDown(space1Key))
            {
                SetDesiredPiece(0);
            } else if (Input.GetKeyDown(space2Key))
            {
                SetDesiredPiece(1);
            } else if (Input.GetKeyDown(space3Key))
            {
                SetDesiredPiece(2);
            }
        }
    }
}
