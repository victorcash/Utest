using UnityEngine;

[ExecuteAlways]
public class GameService : MonoBehaviour
{
    public GameConfig config;
    public static GameConfig Config;

    public GamePlayElementDatabase database;
    public static GamePlayElementDatabase Database;

    private void Awake() => LoadConfigs();
    private void OnValidate() => LoadConfigs();
    private void LoadConfigs()
    {
        Config = config;
        Database = database;
    }
}

