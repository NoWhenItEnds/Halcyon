using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is standing idle, waiting for an action. </summary>
    public class IdlingState : EntityState
    {
        /// <summary> The entity is standing idle, waiting for an action. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public IdlingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


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
            ENTITY.Velocity = Vector2.Zero;
            Boolean isCollision = ENTITY.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = ENTITY.GetLastSlideCollision();
            }
        }
    }
}
