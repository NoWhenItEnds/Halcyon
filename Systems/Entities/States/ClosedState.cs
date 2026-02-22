using Godot;
using Halcyon.Entities.EntityCommands;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
    public class ClosedState : EntityState
    {
        /// <inheritdoc/>
        protected override String _animationPrefix { get; } = "closed";


        /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public ClosedState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            _entity.Sprite.Animation = $"{_animationPrefix}_vertical";
            _entity.Velocity = Vector2.Zero;
        }
    }
}
