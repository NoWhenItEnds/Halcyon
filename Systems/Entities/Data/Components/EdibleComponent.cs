using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> Marks an entity as something that can be consumed by eating or drinking. </summary>
    [GlobalClass]
    [Tool]
    public partial class EdibleComponent : DataComponent
    {
        /// <summary> How much hunger is restored when this entity is consumed. </summary>
        [Export] public Property NutritionValue { get; private set; } = new Property("nutrition_value", 1f);


        /// <summary> Marks an entity as something that can be consumed by eating or drinking. </summary>
        public EdibleComponent() : base() { }
    }
}
