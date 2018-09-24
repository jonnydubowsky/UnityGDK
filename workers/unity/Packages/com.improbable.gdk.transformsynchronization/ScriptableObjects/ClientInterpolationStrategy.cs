using Improbable.Gdk.TransformSynchronization;
using Unity.Entities;
using UnityEngine;

namespace Improbable.Gdk.Transformsynchronization
{
    [CreateAssetMenu(menuName = "SpatialOS/Transform/ClientInterpolationStrategy")]
    public class ClientInterpolationStrategy : TransformSynchrnoizationStrategy
    {
        public int TargetBufferSize = 15;
        public int MaxBufferSize = 10;

        public override void Apply(Entity entity, World world)
        {
            var manager = world.GetExistingManager<EntityManager>();
            manager.AddComponent(entity, typeof(ClientInterpolationConfig));
            manager.SetComponentData(entity, new ClientInterpolationConfig
            {
                TargetBufferSize = TargetBufferSize,
                MaxLoadMatchedBufferSize = MaxBufferSize
            });
        }
    }
}
