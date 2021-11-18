using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    public static AppCanvas instance;
    private void Awake()
    {
        instance = this;
    }

}
