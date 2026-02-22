using Godot;


namespace Halcyon.Entities.Data
{
    /// <summary> The persistent data for an entity. </summary>
    public abstract partial class EntityData : Resource
    {
        /// <summary> The entity's personal name. </summary>
        [ExportGroup("General")]
        [Export] public EntityName Name { get; private set; } = EntityName.Random(NameGender.NONE);


        /// <summary> The persistent data for an entity. </summary>
        public EntityData() { }
    }
}
