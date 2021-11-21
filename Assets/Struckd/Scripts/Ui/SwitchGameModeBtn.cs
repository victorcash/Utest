using UnityEngine;
using UnityEngine.UI;

public class SwitchGameModeBtn : MonoBehaviour
{
    public GameMode targetMode;
    public Button switchButton;
    private void Awake() => switchButton.onClick.AddListener(() => { Services.GameStates.SwitchGameMode(targetMode); });
}
