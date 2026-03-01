using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for human entities. </summary>
    public class HumanStateMachine : EntityStateMachine
    {
        /// <summary> A state machine used for human entities. </summary>
        /// <param name="entity"> A reference to the entity controlled by the state. </param>
        public HumanStateMachine(ActorEntity entity)
        {
            // Set the initial state.
            EntityState initialState = new IdlingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>();

            CurrentState = initialState;

            STATES.Add(initialState);

            STATES.Add(new WalkingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>());

            STATES.Add(new SprintingState(entity)
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>());
        }
    }
}
