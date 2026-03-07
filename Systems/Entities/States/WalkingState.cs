using Godot;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is walking across the ground. </summary>
    public class WalkingState : EntityState
    {
        /// <summary> How fast, in pixels, each point of speed is worth. </summary>
        private readonly Single MOVE_SPEED = 16f;


        /// <summary> The velocity applied each frame while walking. </summary>
        private Vector2 _targetVelocity = Vector2.Zero;


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            if (ENTITY.Data.TryGetComponent<SpeedComponent>(out SpeedComponent? speedComponent) && speedComponent != null)
            {
                Single speed = speedComponent.Value.CurrentValue * MOVE_SPEED;
                _targetVelocity = command.Direction * speed;

                // Handle animation.
                ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
                ENTITY.LayeredSprite.Play(command.Direction.ToDirection());
            }
            else
            {
                throw new ArgumentNullException($"Entity, '{ENTITY.Data.Name}', doesn't possess '{typeof(SpeedComponent)}' component, which this state, {GetType()}, requires.");
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
