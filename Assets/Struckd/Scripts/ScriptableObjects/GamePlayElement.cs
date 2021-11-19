using UnityEngine;


[CreateAssetMenu(menuName = "Struckd/GamePlayElement")]
public class GamePlayElement : ScriptableObject
{
    public int elementID;
    public GamePlayElementBehaviour prefab;
    public ElementInterfaceUi editPanelUi;
}