#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Utilities;

namespace Halcyon.Animations
{
    /// <summary> An 2D sprite that handles layered animations.  </summary>
    [Tool]
    public partial class LayeredSprite2D : Node2D
    {
        /// <summary> A individual animations being applied to the sprite. </summary>
        [Export] public Godot.Collections.Array<LayeredAnimation> Animations
        {
            get => new Godot.Collections.Array<LayeredAnimation>(_animations);
            set
            {
                // Ensure that we first unsubscribe from any previous animations.
                foreach (LayeredAnimation animation in _animations)
                {
                    animation?.Changed -= UpdateAnimations;
                }

                _animations = value.ToArray();

                // Resubscribe to allow for changes.
                foreach (LayeredAnimation animation in _animations)
                {
                    animation?.Changed += UpdateAnimations;
                }

                CallDeferred(nameof(UpdateAnimations));
            }
        }

        /// <summary> How many frames per second the animations should be formatted for. </summary>
        [Export(PropertyHint.Range, "1,64")] private Int32 _animationFPS = 12;


        /// <summary> A individual animations being applied to the sprite. </summary>
        private LayeredAnimation[] _animations = [];

        /// <summary> A reference to the node's animation player. </summary>
        /// <remarks> As this is a tool, direct references to a node will often be lost, so we assume null and reload using a nodepath. </remarks>
        private AnimationPlayer _player;

        /// <summary> A map of the a layer to its kind. </summary>
        /// <remarks> As this is a tool, direct references to a node will often be lost, so we assume null and reload using a nodepath. </remarks>
        private Dictionary<LayerKind, Sprite2D?> _spriteLayers = new Dictionary<LayerKind, Sprite2D?>();


        /// <inheritdoc/>
        public override void _EnterTree()
        {
            RefreshAnimationPlayer();
            RefreshSprites();
        }


        /// <summary> Ensure that the class has a reference to the animation player sub-node. </summary>
        /// <exception cref="ArgumentNullException"/>
        private void RefreshAnimationPlayer()
        {
            // Check we still have a reference to the animation node, if not, retrieve it.
            if (_player == null)
            {
                _player = GetNodeOrNull<AnimationPlayer>("AnimationPlayer") ?? throw new ArgumentNullException("Unable to find 'AnimationPlayer' subnode.");
            }
        }


        /// <summary> Ensure that the sprite sub-nodes are correctly mapped to the animation layers. </summary>
        private void RefreshSprites()
        {
            // Check that the map has been correctly populated with references to the sprites.
            foreach (LayerKind kind in Enum.GetValues<LayerKind>())
            {
                if (kind != LayerKind.NONE) // Ensure we don't create a layer for NONE.
                {
                    String nodePath = $"Layer - {kind.ToString().ToUpper()}";
                    Sprite2D? currentLayer = GetNodeOrNull<Sprite2D>(nodePath);
                    if (currentLayer != null)
                    {
                        _spriteLayers.TryAdd(kind, currentLayer);
                    }
                    else
                    {
                        GD.PushError($"Unable to find a sprite layer in the LayeredSprite2D with the path, '{nodePath}'. You probably want to add it, Fuckwit.");
                    }
                }
            }
        }


        /// <summary> Update the animation player and layered sprites with the new LayeredAnimation(s). </summary>
        private void UpdateAnimations()
        {
            RefreshAnimationPlayer();
            RefreshSprites();

            if (_player.HasAnimationLibrary("current"))
            {
                _player.RemoveAnimationLibrary("current");
            }
            AnimationLibrary library = new AnimationLibrary();
            _player.AddAnimationLibrary("current", library);
            Dictionary<Direction, Animation> animationMap = new Dictionary<Direction, Animation>();

            // Pre-build lookup to avoid O(n) scan per layer.
            Dictionary<LayerKind, LayeredAnimation> animationByLayer = _animations
                .Where(x => x != null)
                .ToDictionary(x => x.Layer);

            Single secondsPerFrame = 1f / _animationFPS; // How long each frame needs to last to match the desired speed (_animationFPS).

            foreach (KeyValuePair<LayerKind, Sprite2D?> layer in _spriteLayers)
            {
                animationByLayer.TryGetValue(layer.Key, out LayeredAnimation? animation);
                if (animation != null)
                {
                    layer.Value?.Texture = animation.Texture;
                    layer.Value?.Hframes = animation.HFrames;
                    layer.Value?.Vframes = animation.VFrameOrder.Count;

                    // Set up the animations for each direction.
                    foreach (Direction direction in animation.VFrameOrder)
                    {
                        // Create the animation if it doesn't already exist.
                        String animationKey = direction.ToString().ToLower();
                        if (!animationMap.ContainsKey(direction))
                        {
                            Animation directionAnimation = new Animation
                            {
                                LoopMode = Animation.LoopModeEnum.Linear,
                                Length = secondsPerFrame * animation.HFrames
                            };

                            library.AddAnimation(animationKey, directionAnimation);
                            animationMap.Add(direction, directionAnimation);
                        }

                        // Set up a new track for each sprite layer on the animation.
                        Animation trackAnimation = library.GetAnimation(animationKey);
                        Int32 trackIndex = trackAnimation.AddTrack(Animation.TrackType.Value);
                        NodePath frameProperty = new NodePath($"{layer.Value?.GetPath()}:frame_coords");
                        trackAnimation.TrackSetPath(trackIndex, frameProperty);
                        trackAnimation.ValueTrackSetUpdateMode(trackIndex, Animation.UpdateMode.Discrete);

                        // Build the keyframes.
                        Int32 vRow = animation.VFrameOrder.IndexOf(direction);
                        for (Int32 i = 0; i < animation.HFrames; i++)
                        {
                            trackAnimation.TrackInsertKey(trackIndex, i * secondsPerFrame, new Vector2(i, vRow));
                        }
                    }
                }
                else
                {
                    layer.Value?.Texture = null;
                    layer.Value?.Hframes = 1;
                    layer.Value?.Vframes = 1;
                }
            }
        }


        /// <summary> Begin playing the current animation. </summary>
        /// <param name="direction"> Which direction the animation should play for. </param>
        public void Play(Direction direction)
        {
            RefreshAnimationPlayer();

            String animationName = $"current/{direction.ToString().ToLower()}";
            _player.Play(animationName);
        }


        /// <summary> Stop playing the current animation and reset the animation's progress to zero. </summary>
        public void Stop()
        {
            RefreshAnimationPlayer();
            _player.Stop();
        }


        public override void _ExitTree()
        {
            // Ensure, absolutely, that we clean up after ourselves to prevent a memory leak.
            foreach (LayeredAnimation animation in _animations)
            {
                animation?.Changed -= UpdateAnimations;
            }
        }


        // TODO - Should layers be inverted automatically for direction, since that is automatically handled?
    }
}
