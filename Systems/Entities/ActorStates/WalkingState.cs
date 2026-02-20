using Godot;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> The actor is walking across the ground. </summary>
    public class WalkingState : ActorState
    {
        /// <inheritdoc/>
        public override String AnimationPrefix { get; } = "walk";


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(ActorEntity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Update(Vector2 direction)
        {
            ENTITY.SetAnimation(AnimationPrefix, direction.ToDirection());

            Single maxSpeed = ENTITY.Data.SpeedStat.CurrentValue * 10;
            Vector2 acceleration = direction * (maxSpeed * 0.25f);
            ENTITY.Velocity += acceleration;
            Single velocityX = Math.Clamp(ENTITY.Velocity.X, -maxSpeed, maxSpeed);
            Single velocityY = Math.Clamp(ENTITY.Velocity.Y, -maxSpeed, maxSpeed);
            ENTITY.Velocity = new Vector2(velocityX, velocityY);
        }
    }
}
