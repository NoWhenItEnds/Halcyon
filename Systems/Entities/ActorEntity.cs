#nullable disable warnings
using System;
using Godot;
using Halcyon.Entities.ActorStates;
using Halcyon.Utilities;

namespace Halcyon.Entities
{
    /// <summary> An moving, sentient entity within the game world. </summary>
    public partial class ActorEntity : CharacterBody2D, IEntity
    {
        /// <summary> The node that defines the node's collision. </summary>
        [ExportGroup("Nodes")]
        [Export] private CollisionShape2D _collisionShape;

        /// <summary> The sprite representing the actor within the world. </summary>
        [Export] private AnimatedSprite2D _sprite;


        /// <summary> How quickly our movement increases each frame. </summary>
        [ExportGroup("Settings")]
        [Export] private Single _moveAcceleration = 300f;

        /// <summary> Our maximum movement speed while walking. </summary>
        [Export] private Single _walkMaxSpeed = 1000f;

        /// <summary> Our maximum movement speed while sprinting. </summary>
        [Export] private Single _sprintingMaxSpeed = 2000f;


        public ActorStateMachine StateMachine;

        private Direction _currentDirection = Direction.S;


        public override void _Ready()
        {
            StateMachine = new ActorStateMachine(this);
            _sprite.Play();
        }


        public override void _PhysicsProcess(Double delta)
        {
            Velocity *= (Single)delta;
            MoveAndSlide();
        }


        public void SetAnimation(String name, Direction direction) => _sprite.Animation = $"{name}_{direction.ToString().ToLower()}";


        public Vector2 GetDirection() => Velocity.Normalized();


        /// <inheritdoc/>
        public Vector2 GetLocation() => GlobalPosition;
    }
}
