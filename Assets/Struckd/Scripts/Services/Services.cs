using UnityEngine;

public class Services : MonoBehaviour
{
    [Header("SceneReferences")]
    public Transform elementsRoot;

    [Header("ScripableObjectRefereces")]
    public GameConfig config;
    public static GameConfig Config;

    public GamePlayElementDatabase database;
    public static GamePlayElementDatabase Database;

    public static CameraService Camera;
    public static PlayableService Playable;
    public static UiService Ui;
    public static GamePlayElementService GamePlayElement;


    private void Awake() => Iniit();
    private void OnValidate() => Iniit();
    private void Iniit()
    {
        Config = config;
        Config.elementsRoot = elementsRoot;
        Database = database;
        Camera = new CameraService();
        Playable = new PlayableService();
        Ui = new UiService();
        GamePlayElement = new GamePlayElementService();
    }
}
