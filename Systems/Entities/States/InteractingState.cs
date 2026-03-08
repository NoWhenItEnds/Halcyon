using System;
using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.Interactions;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities.States
{
    /// <summary> A generic state for entities participating in an interaction. </summary>
    public class InteractingState : EntityState
    {
        /// <summary> The active interaction driving this state. </summary>
        public Interaction? ActiveInteraction { get; set; } = null;


        /// <summary> A generic state for entities participating in an interaction. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public InteractingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override Boolean CanTransition() => ActiveInteraction == null || ActiveInteraction.IsComplete;


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Velocity = Vector2.Zero;

            if (command is InteractCommand interact)
            {
                ActiveInteraction = interact.Interaction;
            }
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            if (ActiveInteraction != null)
            {
                // Only the actor ticks the interaction to maintain a single source of truth. This prevents the acted upon to also tick.
                if (ENTITY == ActiveInteraction.Actor)
                {
                    ActiveInteraction.Update(delta);

                    if (ActiveInteraction.IsComplete)
                    {
                        ActiveInteraction.End();

                        // Release the target first, then self.
                        ActiveInteraction.Target.HandleCommand(new IdleCommand(ActiveInteraction.Target));
                        RequestTransition(new IdleCommand(ENTITY));
                    }
                }
            }
        }


        /// <inheritdoc/>
        public override void Stop()
        {
            ActiveInteraction = null;
            base.Stop();
        }
    }
}
