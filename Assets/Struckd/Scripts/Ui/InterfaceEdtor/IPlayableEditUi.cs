using UnityEngine.UI;
using UnityEngine;

public class IPlayableEditUi : ElementInterfaceUi
{
    public Button button;
    public IPlayable iPlayable => (IPlayable)target;

    protected override void Init()
    {
        base.Init();
        button.onClick.AddListener(SetAsPlayable);
        button.image.color = iPlayable.IsActivePlayable() ? Color.green : Color.red;
    }
    private void SetAsPlayable()
    {
        Services.GamePlayElement.ClearAllActiveIPlayable();
        iPlayable.SetAsActivePlayable(true);
        button.image.color = iPlayable.IsActivePlayable() ? Color.green : Color.red;
    }
}
