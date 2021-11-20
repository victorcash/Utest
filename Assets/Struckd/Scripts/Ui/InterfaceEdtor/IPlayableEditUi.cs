using UnityEngine.UI;

public class IPlayableEditUi : ElementInterfaceUi
{
    public Button button;
    public IPlayable iPlayable => (IPlayable)target;

    private void Awake()
    {
        button.onClick.AddListener(SetAsPlayable);
    }
    private void SetAsPlayable()
    {
        iPlayable.SetAsPlayable();
    }
}
