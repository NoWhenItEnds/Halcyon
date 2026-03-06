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
        [Export] public Stat Strength { get; private set; } = new Stat("attribute_strength", 1, 0, 10);

        [Export] public Stat Dexterity { get; private set; } = new Stat("attribute_dexterity", 1, 0, 10);

        [Export] public Stat Vigor { get; private set; } = new Stat("attribute_vigor", 1, 0, 10);

        [Export] public Stat Intellect { get; private set; } = new Stat("attribute_intellect", 1, 0, 10);

        [Export] public Stat Presence { get; private set; } = new Stat("attribute_presence", 1, 0, 10);


        [ExportSubgroup("Skills")]
        [Export] public Array<Stat> Skills { get; private set; } = new Array<Stat>();


        /// <summary> The persistent data for an entity's stats / skills. </summary>
        public StatComponent() : base() { }
    }
}
