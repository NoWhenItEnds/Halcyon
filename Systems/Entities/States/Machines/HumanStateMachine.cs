using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for human entities. </summary>
    [GlobalClass]
    [Tool]
    public partial class HumanStateMachine : EntityStateMachine
    {
        /// <inheritdoc/>
        protected override EntityState BuildDefaultState(Entity entity)
        {
            return new IdlingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>()
                .WithTransition<UseCommand, InteractingState>();
        }


        /// <inheritdoc/>
        protected override Dictionary<Type, EntityState> BuildStates(Entity entity)
        {
            Dictionary<Type, EntityState> states = new Dictionary<Type, EntityState>();

            states[typeof(WalkingState)] = new WalkingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>()
                .WithTransition<UseCommand, InteractingState>();

            states[typeof(SprintingState)] = new SprintingState(entity)
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<ExamineCommand, ExaminingState>()
                .WithTransition<UseCommand, InteractingState>();

            states[typeof(ExaminingState)] = new ExaminingState(entity)
                .WithTransition<IdleCommand, IdlingState>();

            states[typeof(InteractingState)] = new InteractingState(entity)
                .WithTransition<IdleCommand, IdlingState>();

            return states;
        }
    }
}
