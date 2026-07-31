using UnityEngine;

public class EntityAnimationsEvents : MonoBehaviour
{
    private Entity entity;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets() => entity.DamageTargets();
    public void DisableMovementAndJump() => entity.EnableMovement(false);
    public void EnableMovementAndJump() => entity.EnableMovement(true);
    
}
