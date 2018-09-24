using Unity.Entities;

namespace Improbable.Gdk.TransformSynchronization
{
    public struct ClientInterpolationConfig : IComponentData
    {
        public int TargetBufferSize;
        public int MaxLoadMatchedBufferSize;
    }
}
