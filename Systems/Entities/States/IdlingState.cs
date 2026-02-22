using System;
using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is standing idle, waiting for an action. </summary>
    public class IdlingState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; init; } = "idle";


        /// <summary> The entity is standing idle, waiting for an action. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public IdlingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.SetAnimation(_animationPrefix, command.Direction.ToDirection());
            ENTITY.Velocity = Vector2.Zero;
        }
    }
}
