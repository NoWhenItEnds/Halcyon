using System;
using Godot;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is currently devouring another entity. </summary>
    public class ConsumingState : EntityState
    {
        /// <summary> How long consuming the food takes, in delta time, to complete. </summary>
        // TODO - This should be based upon the thing being consumed.
        private const Single CONSUMPTION_TIME = 3f;

        /// <summary> How long remaining before the state is complete. </summary>
        private Single _stateTimeRemaining = 0f;


        /// <summary> The entity is currently devouring another entity. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public ConsumingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override Boolean CanTransition() => _stateTimeRemaining <= 0f;


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            _stateTimeRemaining = CONSUMPTION_TIME;

            if (ENTITY == command.ActingEntity)
            {
                GD.Print($"{ENTITY.Data.Name} starts eatting {command.TargetEntity.Data.Name}!");
                if(ENTITY.Data != null && ENTITY.Data.TryGetComponent<HungerComponent>(out HungerComponent? hunger))
                {
                    hunger?.Value.BaseValue += 1f;
                }
            }

            if(command.TargetEntity != null && ENTITY == command.TargetEntity)
            {
                GD.Print($"{command.TargetEntity.Data.Name} is being consumed by {ENTITY.Data.Name}!");
            }
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            _stateTimeRemaining -= (Single)delta;
            if(CanTransition())
            {
                RequestTransition(new IdleCommand(ENTITY));
            }
        }


        /// <inheritdoc/>
        public override void Stop()
        {
            if (ENTITY == TriggeringCommand?.ActingEntity)
            {
                GD.Print($"{ENTITY.Data.Name} ate {TriggeringCommand?.TargetEntity.Data.Name}!");
            }

            if (TriggeringCommand?.TargetEntity != null && ENTITY == TriggeringCommand?.TargetEntity)
            {
                GD.Print($"{TriggeringCommand?.TargetEntity.Data.Name} was consumed by {ENTITY.Data.Name}!");
            }
        }
    }
}
