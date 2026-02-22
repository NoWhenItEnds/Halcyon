using Godot;
using Halcyon.Entities.ActorCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> The actor is running, potentially for their life. </summary>
    public class SprintingState : ActorState
    {
        /// <inheritdoc/>
        public override String AnimationPrefix { get; } = "run";

        /// <summary> How fast the walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 1000f;


        /// <summary> The actor is running, potentially for their life. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public SprintingState(ActorEntity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(ActorCommand command)
        {
            ENTITY.SetAnimation(AnimationPrefix, command.Direction.ToDirection());

            Single speed = ENTITY.Data.SpeedStat.CurrentValue * MOVE_SPEED;
            ENTITY.Velocity = command.Direction * speed;
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            ENTITY.Velocity *= (Single)delta;
            Boolean isCollision = ENTITY.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = ENTITY.GetLastSlideCollision();
            }
        }
    }
}
