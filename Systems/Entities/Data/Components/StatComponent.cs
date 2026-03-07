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
        [Export] public ClampedProperty Strength { get; private set; } = new ClampedProperty("strength", 1, 0, 10);

        [Export] public ClampedProperty Dexterity { get; private set; } = new ClampedProperty("dexterity", 1, 0, 10);

        [Export] public ClampedProperty Vigor { get; private set; } = new ClampedProperty("vigor", 1, 0, 10);

        [Export] public ClampedProperty Intellect { get; private set; } = new ClampedProperty("intellect", 1, 0, 10);

        [Export] public ClampedProperty Presence { get; private set; } = new ClampedProperty("presence", 1, 0, 10);


        [ExportSubgroup("Skills")]
        [Export] public Array<ClampedProperty> Skills { get; private set; } = new Array<ClampedProperty>();


        /// <summary> The persistent data for an entity's stats / skills. </summary>
        public StatComponent() : base() { }
    }
}
