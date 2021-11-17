
public class PlayableService
{
    private IPlayable currentPlayable;
    public void SetAsPlayable(IPlayable playable)
    {
        currentPlayable = playable;
    }
}
