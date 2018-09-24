using System;
using System.Collections;
using Improbable.Gdk.GameObjectRepresentation;
using Improbable.Transform;
using Improbable.Worker.Core;
using Unity.Entities;
using UnityEngine;

namespace Improbable.Gdk.TransformSynchronization
{
    public class TransformSychronizationConfiguration : MonoBehaviour
    {
        [Require] private TransformInternal.Requirable.Reader transformReader;

        public bool AutomaticallyApplyTransform;

        public TransformSynchrnoizationStrategy Strategy;

        private SpatialOSComponent spatialOSComponent;
        private bool initialised;

        public uint TickNumber
        {
            get
            {
                if (enabled == false)
                {
                    return 0;
                }

                var manager = spatialOSComponent.World.GetOrCreateManager<EntityManager>();
                if (!initialised)
                {
                    initialised = manager.HasComponent<TransformToSet>(spatialOSComponent.Entity);
                    if (!initialised)
                    {
                        return 0;
                    }
                }

                if (transformReader.Authority != Authority.NotAuthoritative)
                {
                    return manager.GetComponentData<TicksSinceLastTransformUpdate>(spatialOSComponent.Entity)
                        .NumberOfTicks + transformReader.Data.PhysicsTick;
                }

                return manager.GetComponentData<TransformToSet>(spatialOSComponent.Entity).ApproximateRemoteTick;
            }
        }

        private void OnEnable()
        {
            if (Strategy == null)
            {
                throw new InvalidOperationException($"{nameof(TransformSychronizationConfiguration)} " +
                    $"must be provided a transform synchronization strategy");
            }

            spatialOSComponent = GetComponent<SpatialOSComponent>();
            if (spatialOSComponent == null)
            {
                throw new InvalidOperationException($"{nameof(TransformSychronizationConfiguration)} " +
                    $"should only be added to a GameObject linked to a SpatialOS entity");
            }

            // Check if this is actually needed - to not use the entity manager while in a system
            StartCoroutine(ApplyStrategy());
        }

        private IEnumerator ApplyStrategy()
        {
            yield return null;
            if (AutomaticallyApplyTransform)
            {
                spatialOSComponent.World.GetOrCreateManager<EntityManager>().AddComponent(spatialOSComponent.Entity,
                    typeof(AutomaticallyApplyTransformTag));
            }

            Strategy.Apply(spatialOSComponent.Entity, spatialOSComponent.World);
        }
    }
}
