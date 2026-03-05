using Godot;
using Halcyon.Utilities;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> Data that defines the entity's collision. </summary>
    [GlobalClass]
    [Tool]
    public partial class CollisionComponent : DataComponent
    {
        /// <summary> The dimensions of the collision shape, in pixels. </summary>
        /// <remarks> For a circle, this is the radius. </remarks>
        [ExportGroup("Collision")]
        [Export] public Vector2 CollisionSize { get; set; } = Vector2.Zero; // TODO - Have based upon entity size?

        /// <summary> The collision's shape. </summary>
        [Export] public Shape CollisionShape { get; set; } = Shape.NONE;    // TODO - Implement collision shape changes. With Tool-dynamic updates.


        /// <summary> Data that defines the entity's collision. </summary>
        public CollisionComponent() : base() { }
    }
}
