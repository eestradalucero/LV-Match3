using UnityEngine;

namespace Match3.Scripts.API.Match3.Data
{
    public interface IPieceTypeColorData
    {
        PieceType PieceType { get; }
        Color Color { get; }
    }
}
