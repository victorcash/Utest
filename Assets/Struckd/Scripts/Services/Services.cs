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

    public GameStates gameStates;
    public static GameStates GameStates;

    public ElementPlacer elementPlacer;
    public static ElementPlacer ElementPlacer;    
    
    public PlayerInput playerInput;
    public static PlayerInput PlayerInput;

    public UICanvasControllerInput joyStick;
    public static UICanvasControllerInput JoyStick;

    public GameElementEditor gameElementEditor;
    public static GameElementEditor GameElementEditor;

    public EnvironmentController environmentController;
    public static EnvironmentController EnvironmentController;

    public SceneReferences sceneReferences;
    public static SceneReferences SceneReferences;

    public GraphApi graphApi;
    public static GraphApi GraphApi;

    public static CameraService Camera;
    public static ElementService Element;

    private void Awake() => Init();
    private void Init()
    {
        SceneReferences = sceneReferences;
        Config = config;
        Database = database;
        GameStates = gameStates;
        JoyStick = joyStick;
        JoyStick.Init();
        ElementPlacer = elementPlacer;
        PlayerInput = playerInput;
        playerInput.neverAutoSwitchControlSchemes = true;
        GameElementEditor = gameElementEditor;
        EnvironmentController = environmentController;
        GraphApi = graphApi;
        Ui = ui;
        Ui.Init();
        Camera = GetComponent<CameraService>();
        Camera.Init(GetComponentInChildren<CinemachineVirtualCamera>());
        Element = new ElementService();
        gameStates.SetGameMode(GameMode.Edit);
    }
}
