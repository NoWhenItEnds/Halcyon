using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
    [GlobalClass]
    [Tool]
    public partial class EntertainmentComponent : DataComponent
    {
        /// <summary> How entertained / satisfied the entity is. </summary>
        public Stat Entertainment { get; private set; } = new Stat("entertainment", 10, 0, 10);


        /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
        public EntertainmentComponent() : base() { }
    }
}
