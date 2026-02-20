using System;
using Godot;
using Halcyon.Entities;
using Halcyon.Entities.ActorCommands;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> A manager for converting player input to a controlled entity / UI. </summary>
    public partial class InputManager : SingletonNode<InputManager>
    {
        [Export] private ActorEntity _playerEntity;

        /// <summary> The current direction being input by the player. </summary>
        private Vector2 _direction = Vector2.Zero;

        /// <summary> Whether the player is currently using a controller for input. </summary>
        private Boolean _isUsingController = false;


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            Vector2 direction = Input.GetVector("action_move_w", "action_move_e", "action_move_n", "action_move_s");

            if (direction != Vector2.Zero)
            {
                _direction = Input.GetVector("action_move_w", "action_move_e", "action_move_n", "action_move_s");
                _playerEntity.StateMachine.HandleCommand(new WalkCommand(_direction));
            }
            else
            {
                _playerEntity.StateMachine.HandleCommand(new IdleCommand(_direction));
            }

        }


        /// <inheritdoc/>
        public override void _Input(InputEvent @event)
        {
            // Check if using a controller or not.
            _isUsingController = @event is InputEventJoypadButton || @event is InputEventJoypadMotion;
        }

    }
}
