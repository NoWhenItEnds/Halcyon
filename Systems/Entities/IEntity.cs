using System;
using Godot;
using Halcyon.Utilities;

namespace Halcyon.Entities
{
    /// <summary> An interactable entity within the game world. </summary>
    public interface IEntity
    {
        /// <summary> Set the entity's current animation on their sprite. </summary>
        /// <param name="name"> The identifying name of the animation. </param>
        /// <param name="direction"> The direction of the animation. </param>
        public void SetAnimation(String name, Direction direction);


        /// <summary> Get the entity's current position. </summary>
        /// <returns> The node's current global position. </returns>
        public Vector2 GetLocation();
    }
}
