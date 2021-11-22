using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayable
{
    void Possesse();
    void SetFree();
    void SetAsActivePlayable(bool val);
    bool IsActivePlayable();

    void JoyStickLeft(Vector2 val);
    void JoyStickRight(Vector2 val);
    void JoyPadA();
    void JoyPadB();
    Transform CameraFollowRoot();
}
