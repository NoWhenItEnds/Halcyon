using Godot;


namespace Halcyon.Entities.Data
{
    /// <summary> The persistent data for an entity. </summary>
    public abstract partial class EntityData : Resource
    {
        /// <summary> The entity's personal name. </summary>
        [ExportGroup("General")]
        [Export] public EntityName Name { get; set; } = EntityName.Random(EntityGender.NONE);

        /// <summary> The current gender / sex of the entity. </summary>
        [Export] public EntityGender Gender { get; set; } = EntityGender.NONE;


        /// <summary> The persistent data for an entity. </summary>
        public EntityData() { }
    }
}
