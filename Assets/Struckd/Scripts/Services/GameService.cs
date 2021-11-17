using UnityEngine;

[ExecuteAlways]
public class GameService : MonoBehaviour
{
    public GameConfig config;
    public static GameConfig Config;

    public GamePlayElementDatabase database;
    public static GamePlayElementDatabase Database;

    public static CameraService Camera;
    public static PlayableService Playable;
    public static UiService Ui;

    private void Awake() => Iniit();
    private void OnValidate() => Iniit();
    private void Iniit()
    {
        Config = config;
        Database = database;
        Camera = new CameraService();
        Playable = new PlayableService();
        Ui = new UiService();
    }
}
