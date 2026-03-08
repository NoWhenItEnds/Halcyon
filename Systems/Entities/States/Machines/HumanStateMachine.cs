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
            return new IdlingState(this, entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>()
                .WithTransition<InteractCommand, InteractingState>();
        }


        /// <inheritdoc/>
        protected override Dictionary<Type, EntityState> BuildStates(Entity entity)
        {
            Dictionary<Type, EntityState> states = new Dictionary<Type, EntityState>();

            states[typeof(WalkingState)] = new WalkingState(this, entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<ExamineCommand, ExaminingState>();

            states[typeof(SprintingState)] = new SprintingState(this, entity)
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<ExamineCommand, ExaminingState>();

            states[typeof(ExaminingState)] = new ExaminingState(this, entity)
                .WithTransition<IdleCommand, IdlingState>();

            states[typeof(InteractingState)] = new InteractingState(this, entity)
                .WithTransition<IdleCommand, IdlingState>();

            return states;
        }
    }
}
