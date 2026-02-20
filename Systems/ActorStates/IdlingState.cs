using System;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> The actor is standing idle, waiting for an action. </summary>
    public class IdlingState : ActorState
    {
        /// <inheritdoc/>
        public override String AnimationPrefix { get; } = "idle";


        /// <summary> The actor is standing idle, waiting for an action. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public IdlingState(ActorEntity entity) : base(entity) { }
    }
}
