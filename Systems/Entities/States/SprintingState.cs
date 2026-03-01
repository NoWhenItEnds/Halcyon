using Godot;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is running, potentially for their life. </summary>
    public class SprintingState : EntityState
    {
        /// <inheritdoc/>
        protected override ActorEntity _entity { get; }

        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.SPRINTING;


        /// <summary> How fast the walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 1000f;


        /// <summary> The entity is running, potentially for their life. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public SprintingState(ActorEntity entity) : base(entity)
        {
            _entity = entity;
        }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            Single speed = _entity.GetData().SpeedStat.CurrentValue * MOVE_SPEED;
            _entity.Velocity = command.Direction * speed;

            // Handle animation.
            _entity.LayeredSprite.Animations = GetCurrentAnimations();
            _entity.LayeredSprite.Play(command.Direction.ToDirection());
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            _entity.Velocity *= (Single)delta;
            Boolean isCollision = _entity.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = _entity.GetLastSlideCollision();
            }
        }


        /// <inheritdoc/>
        public override void Stop(EntityCommand command) { }
    }
}
