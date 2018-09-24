using Improbable.Gdk.TransformSynchronization;
using Unity.Entities;
using UnityEngine;

public abstract class TransformSynchrnoizationStrategy : ScriptableObject
{
    public abstract void Apply(Entity entity, World world);
}
