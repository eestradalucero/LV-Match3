using System.Linq;
using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.Data
{
    [CreateAssetMenu(fileName = "PiecesColorData", menuName = "Match3/PiecesColorData")]
    public class PiecesColorDataScriptableObject : ScriptableObject, IPiecesColorContainer
    {
        [SerializeField] private PieceTypeColorData[] pieceTypeColorData;
        
        public Color GetPieceColor(PieceType pieceType)
        {
            for (int i = 0; i < pieceTypeColorData.Length; i++)
            {
                if (pieceTypeColorData[i].PieceType == pieceType) return pieceTypeColorData[i].Color;
            }
            return Color.white;
        }
    }
}
