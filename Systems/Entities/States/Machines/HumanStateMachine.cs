using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for human entities. </summary>
    public class HumanStateMachine : EntityStateMachine
    {
        /// <summary> A state machine used for human entities. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        public HumanStateMachine(Entity entity) : base(entity)
        {
            STATES.Add(new WalkingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>());

            STATES.Add(new SprintingState(entity)
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<ExamineCommand, ExaminingState>());

            STATES.Add(new ExaminingState(entity)
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>());
        }


        /// <inheritdoc/>
        protected override EntityState BuildDefaultState(Entity entity)
        {
            return new IdlingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>();
        }
    }
}
