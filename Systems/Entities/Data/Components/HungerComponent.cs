using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
    [GlobalClass]
    [Tool]
    public partial class HungerComponent : DataComponent
    {
        /// <summary> How 'satisfied', whatever that means for a given entity, the entity is. </summary>
        public Stat Hunger { get; private set; } = new Stat("hunger", 100, 0, 100);


        /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
        public HungerComponent() : base() { }
    }
}
