using Godot;
using Halcyon.Entities.EntityCommands;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is open; whatever that means for its specific situation. </summary>
    public class OpenedState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; init; } = "opened";


        /// <summary> The entity is open; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public OpenedState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Sprite.Animation = $"{_animationPrefix}_s";
            ENTITY.Velocity = Vector2.Zero;
        }
    }
}
