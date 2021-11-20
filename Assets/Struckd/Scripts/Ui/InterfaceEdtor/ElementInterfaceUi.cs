using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementInterfaceUi : MonoBehaviour
{
    protected object target;
    public void SetTarget(object target)
    {
        this.target = target;
        Init();
    }
    protected virtual void Init()
    {

    }
}