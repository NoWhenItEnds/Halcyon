#nullable disable warnings
using Godot;
using Godot.Collections;
using Halcyon.Entities.Data;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;
using System;
using System.Linq;

namespace Halcyon.Animations
{
    /// <summary> A correctly formatted texture with metadata to be used on a layered sprite. </summary>
    [GlobalClass]
    [Tool]
    public partial class LayeredAnimation : Resource
    {
        /// <summary> The state machine type this animation is associated with. </summary>
        [Export] public String StateMachine { get; set; } = String.Empty;

        /// <summary> The gender of the entity in the animation. </summary>
        [Export] public EntityGender Gender { get; set; } = EntityGender.NONE;

        /// <summary> The kind of animation the resource represents. </summary>
        [Export] public AnimationKind Animation { get; set; } = AnimationKind.NONE;

        /// <summary> The render order of the layer relative to other layers on the same sprite. Lower values render first. </summary>
        [Export(PropertyHint.Range, "0,10")] public Int32 Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                EmitChanged();
            }
        }
        private Int32 _layer = 0;

        /// <summary> The sprite frames wrapped by the resource object. </summary>
        [Export] public Texture2D Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                EmitChanged();
            }
        }
        private Texture2D _texture;

        /// <summary> How many horizontal frames the animation possesses. </summary>
        [Export(PropertyHint.Range, "1,32")] public Int32 HFrames
        {
            get => _hFrames;
            set
            {
                _hFrames = value;
                EmitChanged();
            }
        }
        private Int32 _hFrames = 8;

        /// <summary> An ordered list of the directions the texture possesses from top to bottom. </summary>
        /// <remarks As Godot still doesn't support serialising HashSets, we can't enforce set uniqueness here. It's up to you, bro. </remarks>
        [Export] public Godot.Collections.Array<Direction> VFrameOrder
        {
            get => _vFrameOrder;
            set
            {
                _vFrameOrder = value;
                EmitChanged();
            }
        }
        private Godot.Collections.Array<Direction> _vFrameOrder = new Godot.Collections.Array<Direction>()
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
        public LayeredAnimation() { }


        /// <inheritdoc/>
        public override void _ValidateProperty(Dictionary property)
        {
            if (property["name"].AsStringName() == "StateMachine")
            {
                String names = String.Join(",", Enumeration.GetAll<StateMachineType>().Select(s => s.Name));
                property["hint"] = (Int32)PropertyHint.Enum;
                property["hint_string"] = names;
            }
        }
    }
}
