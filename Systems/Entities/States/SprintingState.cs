using Godot;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is running, potentially for their life. </summary>
    public class SprintingState : EntityState
    {
        /// <summary> How fast the maximum sprinting movement speed is. </summary>
        private readonly Single MOVE_SPEED = 32f;
        

        /// <summary> The velocity applied each frame while sprinting. </summary>
        private Vector2 _targetVelocity = Vector2.Zero;


        /// <summary> The entity is running, potentially for their life. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public SprintingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            if (ENTITY.Data.TryGetComponent<SpeedComponent>(out SpeedComponent? speedComponent) && speedComponent != null)
            {
                Single speed = speedComponent.SpeedStat.CurrentValue * MOVE_SPEED;
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
