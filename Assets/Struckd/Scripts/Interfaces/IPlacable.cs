using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacable
{
    Transform GetRootTransform();
    void Remove();
}
