using Match3.Scripts.API.Match3;
using Match3.Scripts.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.View
{
    public class BoardPieceView : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] private PiecesColorDataScriptableObject piecesColor;
        
        #pragma warning restore 649
        public void SetColor(PieceType pieceColor)
        {
            sprite.color = piecesColor.GetPieceColor(pieceColor);
        }
    }
}