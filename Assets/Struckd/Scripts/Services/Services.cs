using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Services : MonoBehaviour
{
    [Header("SceneReferences")]
    [SerializeField] private Transform elementsRoot;
    [SerializeField] private EditCamera editCamera;
    [SerializeField] private Canvas editCanvas;

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


    private void Awake() => Init();
    private void Init()
    {
        Config = config;
        Config.Init(elementsRoot);
        Database = database;
        Ui = ui;
        Ui.Init(editCanvas);
        GameStates = gameStates;
        Camera = new CameraService(editCamera);
        Playable = new PlayableService();
        GamePlayElement = new GamePlayElementService();
        ElementPlacer = elementPlacer;
        PlayerInput = playerInput;
        playerInput.neverAutoSwitchControlSchemes = true;
        JoyStick = joyStick;
        GameElementEditor = gameElementEditor;
    }
}
