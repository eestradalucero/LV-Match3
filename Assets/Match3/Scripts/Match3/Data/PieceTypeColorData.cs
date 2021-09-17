using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.Data
{
    [System.Serializable]
    public class PieceTypeColorData : IPieceTypeColorData
    {
        #region EditorFields
#pragma warning disable 649
        [SerializeField] private PieceType pieceType;
        [SerializeField] private Color color;
#pragma warning restore 649
        #endregion
    
        #region Properties

        public PieceType PieceType => pieceType;
        public Color Color => color;

        #endregion
    }
}
