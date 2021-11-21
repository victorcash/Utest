using UnityEngine;


[CreateAssetMenu(menuName = "Struckd/GamePlayElement")]
public class GamePlayElement : ScriptableObject
{
    public int elementID;
    public GameElementBehaviour prefab;
    public Sprite elementIcon;
    public string elementName;
}