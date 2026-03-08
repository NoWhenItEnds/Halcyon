using System;

namespace Halcyon.Entities.Interactions
{
    /// <summary> Represents a two-entity interaction that drives the behaviour of both participants. </summary>
    public abstract class Interaction
    {
        /// <summary> The entity performing the interaction. </summary>
        public Entity Actor { get; }

        /// <summary> The entity being acted upon. </summary>
        public Entity Target { get; }

        /// <summary> Whether the interaction has finished. </summary>
        public Boolean IsComplete { get; protected set; } = false;


        /// <summary> Represents a two-entity interaction that drives the behaviour of both participants. </summary>
        /// <param name="actor"> The entity performing the interaction. </param>
        /// <param name="target"> The entity being acted upon. </param>
        protected Interaction(Entity actor, Entity target)
        {
            Actor = actor;
            Target = target;
        }


        /// <summary> Whether the interaction can begin. Checks that both participants have the required components. </summary>
        /// <returns> Whether the interaction is valid and can proceed. </returns>
        public abstract Boolean CanBegin();


        /// <summary> Called when both entities have entered their interacting states. </summary>
        public abstract void Begin();

        /// <summary> Called every physics frame while the interaction is active. </summary>
        /// <param name="delta"> The time in seconds since the last physics frame. </param>
        public abstract void Update(Double delta);

        /// <summary> Called when the interaction ends. Handles cleanup and final effects. </summary>
        public abstract void End();
    }
}
