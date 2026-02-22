using Godot;
using Halcyon.Entities.EntityCommands;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
    public class ClosedState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; init; } = "closed";


        /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public ClosedState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Sprite.Animation = $"{_animationPrefix}_s";
            ENTITY.Velocity = Vector2.Zero;
        }
    }
}
