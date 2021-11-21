using GraphQlClient.Core;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public static CameraService Camera;
    public static PlayableService Playable;

    public static GamePlayElementService GamePlayElement;

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


    private void Awake() => Init();
    private void Init()
    {
        SceneReferences = sceneReferences;
        Config = config;
        Database = database;
        GameStates = gameStates;
        ElementPlacer = elementPlacer;
        PlayerInput = playerInput;
        playerInput.neverAutoSwitchControlSchemes = true;
        JoyStick = joyStick;
        GameElementEditor = gameElementEditor;
        EnvironmentController = environmentController;
        GraphApi = graphApi;
        Ui = ui;
        Ui.Init(GameStates.onGameModeChanged);


        Camera = new CameraService();
        Playable = new PlayableService();
        GamePlayElement = new GamePlayElementService();
    }
}
