using Godot;
using Halcyon.Animations;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is walking across the ground. </summary>
    public class WalkingState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.WALKING;


        /// <summary> How fast the maximum walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 16f;


        /// <summary> The velocity applied each frame while walking. </summary>
        private Vector2 _targetVelocity = Vector2.Zero;


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            if (ENTITY.Data.TryGetComponent<StatComponent>(out StatComponent? statComponent) && statComponent != null)
            {
                Single speed = statComponent.SpeedStat.CurrentValue * MOVE_SPEED;
                _targetVelocity = command.Direction * speed;

                // Handle animation.
                ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
                ENTITY.LayeredSprite.Play(command.Direction.ToDirection());
            }
            else
            {
                throw new ArgumentNullException($"Entity, '{ENTITY.Data.Name}', doesn't possess '{typeof(StatComponent)}' component, which this state, {GetType()}, requires.");
            }
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            ENTITY.Velocity = _targetVelocity;
            Boolean isCollision = ENTITY.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = ENTITY.GetLastSlideCollision();
            }
        }


        /// <inheritdoc/>
        public override void Stop(EntityCommand command) { }
    }
}
