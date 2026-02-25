using Godot;
using Halcyon.Utilities;
using System;

namespace Halcyon.Animations
{
    /// <summary> A correctly formatted texture with metadata to be used on a layered sprite. </summary>
    [GlobalClass]
    [Tool]
    public partial class LayeredAnimation : Resource
    {
        /// <summary> The identifying name of the animation. </summary>
        /// <example> Actor_Human / Prop_Door_Oak </example>
        [Export] public String Name { get; set; } = String.Empty;

        /// <summary> The kind of animation the resource represents. </summary>
        [Export] public AnimationKind Animation { get; set; } = AnimationKind.NONE;

        /// <summary> The kind of layer the texture occupies. </summary>
        [Export] public LayerKind Layer { get; set; } = LayerKind.NONE;

        /// <summary> The sprite frames wrapped by the resource object. </summary>
        [Export] public Texture2D Texture { get; set; }

        /// <summary> How many horizontal frames the animation possesses. </summary>
        [Export(PropertyHint.Range, "1,32")] public Int32 HFrames { get; set; } = 8;

        /// <summary> An ordered list of the directions the texture possesses from top to bottom. </summary>
        /// <remarks As Godot still doesn't support serialising HashSets, we can't enforce set uniqueness here. It's up to you, bro. </remarks>
        [Export] public Godot.Collections.Array<Direction> VFrameOrder { get; private set; } = new Godot.Collections.Array<Direction>()
        {
            Direction.NW,
            Direction.W,
            Direction.SW,
            Direction.S,
            Direction.SE,
            Direction.E,
            Direction.NE,
            Direction.N,
        };


        /// <summary> A correctly formatted texture with metadata to be used on a layered sprite. </summary>
        public LayeredAnimation()
        {
            /*
            ArgumentNullException.ThrowIfNullOrWhiteSpace(Name);
            ArgumentNullException.ThrowIfNull(Texture);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(HFrames);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(VFrameOrder.Count);
            */
        }
    }
}
