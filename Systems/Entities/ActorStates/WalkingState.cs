using Godot;
using Halcyon.Entities.ActorCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> The actor is walking across the ground. </summary>
    public class WalkingState : ActorState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; init; } = "walk";


        /// <summary> How fast the walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 500f;


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(ActorEntity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(ActorCommand command)
        {
            ENTITY.SetAnimation(_animationPrefix, command.Direction.ToDirection());

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
