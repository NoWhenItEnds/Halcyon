using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A map of all the entity states in the project so that they can be accessed like an enum. </summary>
    public class EntityStateType : Enumeration
    {
        public static readonly EntityStateType Idling = new EntityStateType(nameof(IdlingState));
        public static readonly EntityStateType Walking = new EntityStateType(nameof(WalkingState));
        public static readonly EntityStateType Sprinting = new EntityStateType(nameof(SprintingState));
        public static readonly EntityStateType Examining = new EntityStateType(nameof(ExaminingState));
        public static readonly EntityStateType Consuming = new EntityStateType(nameof(ConsumingState));
        public static readonly EntityStateType Closed = new EntityStateType(nameof(ClosedState));
        public static readonly EntityStateType Opened = new EntityStateType(nameof(OpenedState));


        /// <summary> A map of all the entity states in the project so that they can be accessed like an enum. </summary>
        /// <param name="name"> A human-readable label for this value. </param>
        public EntityStateType(String name) : base(name) { }
    }
}
