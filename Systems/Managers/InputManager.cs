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
            Entity player = _entityManager.PlayerEntity;

            // Get current input direction.
            Vector2 direction = Input.GetVector("action_move_w", "action_move_e", "action_move_n", "action_move_s");

            EntityCommand currentCommand = HandleMovement(player, direction);

            // First ensure that we have a direction. If not, we will try to use the last recorded direction.
            if(direction == Vector2.Zero)
            {
                direction = _previousDirection;
            }

            // Handle complex actions we prioritise above other inputs.
            // Note: these WILL overwrite any other input.
            EntityCommand? complexCommand = HandleComplexInput(player, direction);
            if(complexCommand != null)
            {
                currentCommand = complexCommand;
            }

            _entityManager.PlayerEntity.HandleCommand(currentCommand);

            // TODO - Probably not here.
            _cameraManager.SetPosition(_entityManager.PlayerEntity.GlobalPosition);
        }


        /// <inheritdoc/>
        public override void _Input(InputEvent @event)
        {
            // Check if using a controller or not.
            _isUsingController = @event is InputEventJoypadButton || @event is InputEventJoypadMotion;
        }


        private EntityCommand HandleMovement(Entity player, Vector2 direction)
        {
            // Initial assumption is the player is idling. I.e. not doing anything.
            EntityCommand command = new IdleCommand(player);

            // If there is a direction, then they're probably trying to move.
            if (direction != Vector2.Zero)
            {
                command = new WalkCommand(player, direction);

                // Handle complex MOVEMENT-based actions.
                if (Input.IsActionPressed("action_sprint"))
                {
                    command = new SprintCommand(player, direction);
                }
            }

            return command;
        }


        private EntityCommand? HandleComplexInput(Entity player, Vector2 direction)
        {
            EntityCommand? command = null;

            if (Input.IsActionJustPressed("action_interact"))
            {
                // TODO - Need to determine HERE what command to use depending upon input.
                Entity[] entities = _entityManager.PlayerEntity.GetNearbyEntities();
                if (entities.Length > 0)
                {
                    //command = new ExamineCommand(player, entities[0]);
                    command = new ConsumeCommand(player, entities[0]);
                }
            }

            return command;
        }
    }
}
