using Godot;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is standing idle, waiting for an action. </summary>
    public class IdlingState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.IDLING;


        /// <summary> How quickly the entity decelerates to a stop, in units per second per second. </summary>
        private readonly Single DECELERATION = 512f;


        /// <summary> The entity is standing idle, waiting for an action. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public IdlingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            // Handle animation.
            ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
            ENTITY.LayeredSprite.Play(command.Direction.ToDirection());
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            ENTITY.Velocity = ENTITY.Velocity.MoveToward(Vector2.Zero, DECELERATION * (Single)delta);
            Boolean isCollision = ENTITY.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = ENTITY.GetLastSlideCollision();
            }
        }
    }
}
