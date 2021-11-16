using UnityEngine;

public class GamePlayElementBehaviour : MonoBehaviour, IPlacable
{
    public virtual Transform GetRootTransform() => transform;

    public virtual void Remove()
    {
        Destroy(gameObject);
    }
}
