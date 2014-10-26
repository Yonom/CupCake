using System;

namespace EEPhysics
{
    /// <summary>
    /// Constant physics variables.
    /// </summary>
    internal static class PhysicsConfig
    {
        public const int MsPerTick = 10;
        public const double VariableMultiplier = 7.752;
        public static readonly double BaseDrag = (Math.Pow(0.9981, MsPerTick) * 1.00016093);
        public static readonly double NoModifierDrag = (Math.Pow(0.99, MsPerTick) * 1.00016093);
        public static readonly double WaterDrag = (Math.Pow(0.995, MsPerTick) * 1.00016093);
        public static readonly double MudDrag = (Math.Pow(0.975, MsPerTick) * 1.00016093);
        public const double JumpHeight = 26;
        public const double Gravity = 2;
        public const double Boost = 16;
        public const double WaterBuoyancy = -0.5;
        public const double MudBuoyancy = 0.4;
        public const int QueueLength = 2;
    }
}
