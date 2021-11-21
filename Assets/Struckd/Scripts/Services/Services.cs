using GraphQlClient.Core;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Services : MonoBehaviour
{
    [Header("ScripableObjectRefereces")]
    public GameConfig config;
    public static GameConfig Config;

    public GamePlayElementDatabase database;
    public static GamePlayElementDatabase Database;

    public UiService ui;
    public static UiService Ui;

    public UICanvasControllerInput joyStick;
    public static UICanvasControllerInput JoyStick;

    public static GameStates GameStates;
    public static GameElementPlacer GameElementPlacer;    
    public static PlayerInput PlayerInput;
    public static GameElementEditor GameElementEditor;
    public static EnvironmentController EnvironmentController;
    public static SceneReferences SceneReferences;

    public GraphApi graphApi;
    public static GraphApi GraphApi;

    public static CameraService Camera;
    public static ElementService GameElement;

    public VFXController vFXController;

    private void Awake() => Init();
    private void Init()
    {
        SceneReferences = GetComponent<SceneReferences>();
        Config = config;
        Database = database;
        GameStates = GetComponent<GameStates>();
        JoyStick = joyStick;
        JoyStick.Init();
        GameElementPlacer = GetComponent<GameElementPlacer>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerInput.neverAutoSwitchControlSchemes = true;
        GameElementEditor = GetComponent<GameElementEditor>();
        EnvironmentController = GetComponent<EnvironmentController>();
        GraphApi = graphApi;
        Ui = ui;
        Ui.Init();
        vFXController.Init();
        Camera = GetComponent<CameraService>();
        Camera.Init(GetComponentInChildren<CinemachineVirtualCamera>());
        GameElement = new ElementService();
        GameStates.SetGameMode(GameMode.Edit);
    }
}
