using Unity.Mathematics;

namespace DefaultNamespace
{
    public static class MathHelpers
    {
        public static float GetHeading(float3 objectPos, float3 targetPos)
        {
            var x = objectPos.x - targetPos.x;
            var y = objectPos.z - targetPos.z;
            return math.atan2(x, y) + math.PI;
        }
    }
}