using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A map of all the state machines in the project so that they can be accessed like an enum. </summary>
    public class StateMachineType : Enumeration
    {
        public static readonly StateMachineType Human = new StateMachineType(nameof(HumanStateMachine));
        public static readonly StateMachineType Door = new StateMachineType(nameof(DoorStateMachine));


        /// <summary> A map of all the state machines in the project so that they can be accessed like an enum. </summary>
        /// <param name="name"> A human-readable label for this value. </param>
        public StateMachineType(String name) : base(name) { }
    }
}
