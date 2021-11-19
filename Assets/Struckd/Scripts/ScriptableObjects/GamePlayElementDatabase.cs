using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Struckd/GamePlayElementDatabase")]
public class GamePlayElementDatabase : ScriptableObject
{
    public List<GamePlayElement> allGamePlayElements;



    //TODO: Fix the index
    public GamePlayElement GetElement(int id)
    {
        return allGamePlayElements[id];
    }


    public void SyncElemenetID()
    { 
    
    }
}


