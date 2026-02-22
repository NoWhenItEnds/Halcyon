namespace Halcyon.Entities.States
{
    /// <summary> A state machine used for static, non-moving entities. </summary>
    public class StaticStateMachine : EntityStateMachine
    {
        /// <summary> A state machine used for static, non-moving entities. </summary>
        /// <param name="entity"> A reference to the entity controlled by the state. </param>
        public StaticStateMachine(Entity entity)
        {
            EntityState initialState = new IdlingState(entity);
            CurrentState = initialState;
            STATES.Add(initialState);
        }
    }
}
