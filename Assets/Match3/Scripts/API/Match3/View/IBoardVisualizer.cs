namespace Match3.Scripts.API.Match3.View
{
    public interface IBoardVisualizer
    {
        void FeedBoard(IBoard newBoard);
        void UpdateView();
    }
}
