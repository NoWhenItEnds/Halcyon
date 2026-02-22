using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is walking across the ground. </summary>
    public class WalkingState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; init; } = "walking";


        /// <summary> How fast the walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 500f;


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Sprite.Animation = $"{_animationPrefix}_{command.Direction.ToDirection().ToString().ToLower()}";

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
