using UnityEngine;

[ExecuteAlways]
public class GameService : MonoBehaviour
{
    public GameConfig config;
    public static GameConfig Config;

    private void Awake() => LoadConfigs();
    private void OnValidate() => LoadConfigs();
    private void LoadConfigs()
    {
        Config = config;
    }
}

