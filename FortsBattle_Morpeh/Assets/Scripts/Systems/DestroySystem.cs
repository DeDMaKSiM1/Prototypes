using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem
{
    private Event<DamageEvent> _damageEvent;
    public override void OnAwake() => _damageEvent = World.GetEvent<DamageEvent>();

    public override void OnUpdate(float deltaTime)
    {
        foreach (var events in _damageEvent.publishedChanges)
        {
            Destroy(events.TargetEntityId);
        }
    }
    private void Destroy(EntityId target)
    {
        if(World.TryGetEntity(target, out var entity))
        {
            var healthComponent = entity.GetComponent<HealthComponent>();
            if (healthComponent.healthPoint <= 0)
            {
                Destroy(healthComponent.gameObject);
            }
        }
    }
}