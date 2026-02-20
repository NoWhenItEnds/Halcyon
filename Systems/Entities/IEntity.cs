using Godot;

namespace Halcyon.Entities
{
    /// <summary> An interactable entity within the game world. </summary>
    public interface IEntity
    {
        /// <summary> Get the entity's current position. </summary>
        /// <returns> The node's current global position. </returns>
        public Vector2 GetLocation();
    }
}
