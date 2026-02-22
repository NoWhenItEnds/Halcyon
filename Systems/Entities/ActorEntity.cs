#nullable disable warnings
using System;
using Godot;
using Halcyon.Entities.Data;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities
{
    public partial class ActorEntity : Entity
    {
        /// <summary> The persistent data for an entity. </summary>
        [ExportGroup("Settings")]
        [Export] private ActorData _data = new ActorData();


        /// <summary> A reference to the entity's state machine. </summary>
        private ActorStateMachine _stateMachine;

        public override void Initialise(EntityStateMachine stateMachine)
        {
            if (stateMachine is ActorStateMachine machine)
            {
                Sprite.Stop();
                _stateMachine = machine;
                Sprite.Play();
            }
            else
            {
                throw new ArgumentException($"Entity state machine for the ActorEntity should be ActorStateMachine, not {stateMachine.GetType()}!", nameof(stateMachine));
            }
        }


        public override ActorData GetData() => _data;

        public override ActorStateMachine GetStateMachine() => _stateMachine;
    }
}
