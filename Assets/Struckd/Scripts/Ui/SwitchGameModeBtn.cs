using UnityEngine;
using UnityEngine.UI;

public class SwitchGameModeBtn : MonoBehaviour
{
    public GameMode targetMode;
    private Button switchButton;

    private void Awake()
    {
        switchButton = GetComponent<Button>();
        switchButton.onClick.AddListener(() => { Services.GameStates.SetGameMode(targetMode); });
    }
}
