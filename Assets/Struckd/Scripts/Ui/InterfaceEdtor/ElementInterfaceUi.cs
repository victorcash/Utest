using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInterfaceUi : MonoBehaviour
{
    private object target;
    public void SetTarget(object target)
    {
        this.target = target;
        Init();
    }
    protected virtual void Init()
    {

    }
}