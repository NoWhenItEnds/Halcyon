namespace Halcyon.Entities
{
    /// <summary> An AI controller for an entity. </summary>
    /// <remarks> Decisions are made using GOAP. </remarks>
    public class EntityController
    {
        /// <summary> A reference to the entity being controlled. </summary>
        private readonly Entity ENTITY;


        /// <summary> An AI controller for an entity. </summary>
        /// <param name="entity"> A reference to the entity being controlled. </param>
        public EntityController(Entity entity)
        {
            ENTITY = entity;
        }
    }
}
