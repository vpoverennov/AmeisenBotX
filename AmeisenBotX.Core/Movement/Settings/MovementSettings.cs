﻿namespace AmeisenBotX.Core.Movement.Settings
{
    public class MovementSettings
    {
        public bool EnableDistanceMovedJumpCheck { get; set; } = true;

        public bool EnableTracelineJumpCheck { get; set; } = true;

        public float JumpCheckDistance { get; set; } = 0.3f;

        public float JumpCheckHeight { get; set; } = 0.7f;

        public float MaxAcceleration { get; set; } = 1f;

        public double MaxDistanceMovedJumpUnstuck { get; set; } = 0.3;

        public float MaxSteering { get; set; } = 1f;

        public float MaxVelocity { get; set; } = 9f;

        public double MinDistanceMovedJumpUnstuck { get; set; } = -1.0;

        public double MinUnstuckDistance { get; set; } = 8.0;

        public double SeperationDistance { get; set; } = 2.0;

        public int StuckCounterUnstuck { get; set; } = 3;

        public float UnstuckDistance { get; set; } = 6.0f;

        public double WaypointCheckThreshold { get; set; } = 2.0;

        public double WaypointCheckThresholdMounted { get; set; } = 3.5;
    }
}