#nullable disable warnings
using System;
using Godot;
using Halcyon.Entities;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> A manager for converting player input to a controlled entity / UI. </summary>
    public partial class InputManager : SingletonNode<InputManager>
    {
        /// <summary> A reference to the game world's entity manager singleton. </summary>
        private EntityManager _entityManager;

        /// <summary> A reference to the game world's camera manager singleton. </summary>
        private CameraManager _cameraManager;

        /// <summary> The currently queued command for the player entity. </summary>
        private EntityCommand? _currentCommand = null;

        /// <summary> The last direction being input by the player that wasn't zero. </summary>
        private Vector2 _previousDirection = Vector2.Zero;

        /// <summary> Whether the player is currently using a controller for input. </summary>
        private Boolean _isUsingController = false;


        /// <inheritdoc/>
        public override void _Ready()
        {
            _entityManager = EntityManager.Instance;
            _cameraManager = CameraManager.Instance;
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            // Get current input direction.
            Vector2 direction = Input.GetVector("action_move_w", "action_move_e", "action_move_n", "action_move_s");

            // Set initial default action and cache direction.
            if (direction != Vector2.Zero)
            {
                _previousDirection = direction;
                _currentCommand = new WalkCommand(direction);

                // Handle complex actions that require direction.
                if (Input.IsActionPressed("action_sprint"))
                {
                    _currentCommand = new SprintCommand(direction);
                }
            }
            else
            {
                _currentCommand = new IdleCommand(_previousDirection);
            }

            // Handle complex actions that DON'T require direction.
            // Note: these WILL overwrite any other input.
            if (Input.IsActionPressed("action_interact"))
            {
                // TODO - Need to determine HERE what command to use depending upon input.
                Entity[] entities = _entityManager.PlayerEntity.GetNearbyEntities();
                if (entities.Length > 0)
                {
                    _currentCommand = new ExamineCommand(entities[0]);
                }
            }

            _entityManager.PlayerEntity.Data?.StateMachine?.TryTransitionState(_currentCommand);

            // TODO - Probably not here.
            _cameraManager.SetPosition(_entityManager.PlayerEntity.GlobalPosition);
        }


        /// <inheritdoc/>
        public override void _Input(InputEvent @event)
        {
            // Check if using a controller or not.
            _isUsingController = @event is InputEventJoypadButton || @event is InputEventJoypadMotion;
        }
    }
}
