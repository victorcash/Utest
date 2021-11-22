using UnityEngine;

public class FactionColorSetter : MonoBehaviour
{
    public void SetColor(Faction faction)
    {
        var color = Services.Config.factionColorDictionary[faction];
        var allRenders = GetComponentsInChildren<Renderer>();
        foreach (var r in allRenders)
        {
            r.material.SetColor("_Color", color);
        }
    }
}
