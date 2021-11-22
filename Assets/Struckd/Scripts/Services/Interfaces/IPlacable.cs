using UnityEngine;

public interface IPlacable
{
    Vector3 GetPos();
    Quaternion GetRotation();
    float GetScale();
    float GetHeight();
    void SetPos(Vector3 val);
    void SetRotationX(float val);
    void SetRotationY(float val);
    void SetScale(float val);
    void SetHeight(float val);
    void Remove();
}