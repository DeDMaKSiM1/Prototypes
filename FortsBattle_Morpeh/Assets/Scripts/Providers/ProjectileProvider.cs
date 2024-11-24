using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ProjectileProvider : MonoProvider<ProjectileComponent>
{
    [SerializeField] private float projectileDamage;

    private Request<DamageRequest> _damageRequest;

    protected override void Initialize()
    {
        _damageRequest = World.Default.GetRequest<DamageRequest>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out HealthProvider targetHealthProvider))
        {
            _damageRequest.Publish(new DamageRequest
            {
                TargetEntityId = targetHealthProvider.Entity.ID,
                Damage = projectileDamage
            });
        }
        
    }

}