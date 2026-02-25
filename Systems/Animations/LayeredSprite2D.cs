using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Utilities;

namespace Halcyon.Animations
{
    /// <summary> An 2D sprite that handles layered animations.  </summary>
    [GlobalClass]
    [Tool]
    public partial class LayeredSprite2D : Node2D
    {
        [Export] public Godot.Collections.Array<LayeredAnimation> Animations
        {
            get => new Godot.Collections.Array<LayeredAnimation>(_animations);
            set
            {
                _animations = value.ToArray();
                UpdateAnimations();
            }
        }

        private LayeredAnimation[] _animations = [];

        [Export] private Int32 _animationFPS = 12;

        private AnimationPlayer _player = new AnimationPlayer();    // TODO - Need to be childed?

        /// <summary> A map of the a layer to its kind. </summary>
        private Dictionary<LayerKind, Sprite2D> _spriteLayers = new Dictionary<LayerKind, Sprite2D>();


        /// <inheritdoc/>
        public override void _Ready()
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(_animationFPS);

            AddChild(_player);  // TODO - Like this?

            // Initialise sprites.
            foreach (LayerKind kind in Enum.GetValues<LayerKind>())
            {
                if (kind != LayerKind.NONE) // Ensure we don't create a layer for NONE.
                {
                    // Create sprite layers.
                    Sprite2D layer = new Sprite2D();
                    layer.Name = $"Layer - {kind.ToString().ToUpper()}";
                    layer.TextureFilter = TextureFilterEnum.NearestWithMipmaps;
                    AddChild(layer);
                    _spriteLayers.Add(kind, layer);
                }
            }
        }

        private void UpdateAnimations()
        {
            //LayeredAnimation[] animations = _animations.Where(x => x != null).ToArray();

            AnimationLibrary library = new AnimationLibrary();
            _player.RemoveAnimationLibrary("current");
            _player.AddAnimationLibrary("current", library);
            Dictionary<Direction, Animation> animationMap = new Dictionary<Direction, Animation>();

            foreach (KeyValuePair<LayerKind, Sprite2D> layer in _spriteLayers)
            {
                LayeredAnimation? animation = _animations.FirstOrDefault(x => x != null && x.Layer == layer.Key) ?? null;
                if (animation != null)
                {
                    layer.Value.Texture = animation.Texture;
                    layer.Value.Hframes = animation.HFrames;
                    layer.Value.Vframes = animation.VFrameOrder.Count;

                    // Set up the animations for each direction.
                    foreach (Direction direction in animation.VFrameOrder)
                    {
                        // Create the animation if it doesn't already exist.
                        String animationKey = direction.ToString().ToLower();
                        if (!animationMap.ContainsKey(direction))
                        {
                            Animation directionAnimation = new Animation
                            {
                                LoopMode = Animation.LoopModeEnum.Linear
                            };

                            library.AddAnimation(animationKey, directionAnimation);
                            animationMap.Add(direction, directionAnimation);
                        }

                        // Set up a new track for each sprite layer on the animation.
                        Animation trackAnimation = library.GetAnimation(animationKey);
                        Int32 trackIndex = trackAnimation.AddTrack(Animation.TrackType.Value);
                        NodePath propertyPath = new NodePath($"{layer.Value.GetPath()}:frame");
                        trackAnimation.TrackSetPath(trackIndex, propertyPath);
                        trackAnimation.ValueTrackSetUpdateMode(trackIndex, Animation.UpdateMode.Discrete);

                        // Build the keyframes.
                        Single secondsPerFrame = (Single)_animationFPS / (Single)animation.HFrames; // How long each frame of the animation (animation.HFrames) needs to last for for our animation to match the desired speed (_animationFPS).
                        for (Int32 i = 0; i < animation.HFrames; i++)
                        {
                            // Animate by changing the sprite's frame.
                            trackAnimation.TrackInsertKey(trackIndex, i * secondsPerFrame, i);
                        }
                    }
                }
                else
                {
                    layer.Value.Texture = null;
                    layer.Value.Hframes = 1;
                    layer.Value.Vframes = 1;
                }
            }
        }

        public void Play(Direction direction) => _player.Play(direction.ToString().ToLower());

        public void Stop() => _player.Stop();

        // TODO - Should layers be inverted automatically for direction, since that is automatically handled?
        // TODO - Make a resource wrapper for Texture2D to provide metadata about the image. A library singleton can then be used to load it and look it up.
    }
}
