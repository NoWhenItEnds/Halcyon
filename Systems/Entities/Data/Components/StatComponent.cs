#nullable disable warnings
using Godot;
using Godot.Collections;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's stats / skills. </summary>
    [GlobalClass]
    [Tool]
    public partial class StatComponent : DataComponent
    {
        [ExportGroup("Stats")]
        [ExportSubgroup("Attributes")]
        [Export] public ClampedProperty Strength { get; private set; } = new ClampedProperty("strength", 0, 10, 1);

        [Export] public ClampedProperty Dexterity { get; private set; } = new ClampedProperty("dexterity", 0, 10, 1);

        [Export] public ClampedProperty Vigor { get; private set; } = new ClampedProperty("vigor", 0, 10, 1);

        [Export] public ClampedProperty Intellect { get; private set; } = new ClampedProperty("intellect", 0, 10, 1);

        [Export] public ClampedProperty Presence { get; private set; } = new ClampedProperty("presence", 0, 10, 1);


        [ExportSubgroup("Skills")]
        [Export] public Array<ClampedProperty> Skills { get; private set; } = new Array<ClampedProperty>();


        /// <summary> The persistent data for an entity's stats / skills. </summary>
        public StatComponent() : base() { }
    }
}
