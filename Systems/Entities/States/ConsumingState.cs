using Godot;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is currently devouring another entity. </summary>
    public class ConsumingState : EntityState
    {
        /// <summary> The entity is currently devouring another entity. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public ConsumingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            if(ENTITY == command.ActingEntity)
            {
                GD.Print($"{command.ActingEntity.Data.Name} eats {command.TargetEntity.Data.Name}!");
                if(ENTITY.Data != null && ENTITY.Data.TryGetComponent<HungerComponent>(out HungerComponent? hunger))
                {
                    hunger?.Value.BaseValue += 1f;
                }
            }

            if(command.TargetEntity != null && ENTITY == command.TargetEntity)
            {
                GD.Print($"{command.TargetEntity.Data.Name} is consumed by {command.ActingEntity.Data.Name}!");
                RequestTransition(new IdleCommand(command.TargetEntity));
            }
        }
    }

}
