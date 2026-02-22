using Godot;
using Godot.Collections;

namespace Halcyon.Entities.Data
{
    /// <summary> The persistent data for an actor entity. </summary>
    [GlobalClass]
    public partial class ActorData : Resource
    {
        [ExportGroup("General")]
        [Export] public ActorName Name { get; private set; } = ActorName.Random(NameGender.NONE);


        [ExportGroup("Stats")]
        [ExportSubgroup("Attributes")]
        [Export] public Stat Strength { get; private set; } = new Stat("attribute_strength", 1, 0, 10);

        [Export] public Stat Dexterity { get; private set; } = new Stat("attribute_dexterity", 1, 0, 10);

        [Export] public Stat Vigor { get; private set; } = new Stat("attribute_vigor", 1, 0, 10);

        [Export] public Stat Intellect { get; private set; } = new Stat("attribute_intellect", 1, 0, 10);

        [Export] public Stat Presence { get; private set; } = new Stat("attribute_presence", 1, 0, 10);


        [ExportSubgroup("Skills")]
        [Export] public Array<Stat> Skills { get; private set; } = new Array<Stat>();


        /// <summary> How fast the character moves as a result of their physical attributes. </summary>
        public DerivedStat SpeedStat { get; private set; }

        /// <summary> The actor's physical stamina. Drained by performing physically demanding activities. </summary>
        public DerivedStat StaminaStat { get; private set; }

        /// <summary> How entertained / satisfied the actor is. </summary>
        public DerivedStat EntertainmentStat { get; private set; }


        /// <summary> The persistent data for an actor entity. </summary>
        public ActorData()
        {
            SpeedStat = new DerivedStat(() => 0, () => 5 + Strength.CurrentValue + Dexterity.CurrentValue);
            StaminaStat = new DerivedStat(() => 0, () => Vigor.CurrentValue + 3);
            EntertainmentStat = new DerivedStat(() => 0, () => 10);  // TODO - Start at max.
        }
    }
}
