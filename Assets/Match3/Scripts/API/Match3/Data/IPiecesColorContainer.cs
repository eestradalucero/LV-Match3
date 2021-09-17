using UnityEngine;

namespace Match3.Scripts.API.Match3.Data
{
    public interface IPiecesColorContainer
    {
        Color GetPieceColor(PieceType pieceType);
    }
}
