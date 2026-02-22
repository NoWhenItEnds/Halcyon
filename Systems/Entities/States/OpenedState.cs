using Godot;
using Halcyon.Entities.EntityCommands;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is open; whatever that means for its specific situation. </summary>
    public class OpenedState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; } = "opened";


        /// <summary> The entity is open; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public OpenedState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            _entity.Sprite.Animation = $"{_animationPrefix}_vertical";
            _entity.Velocity = Vector2.Zero;
        }
    }
}
