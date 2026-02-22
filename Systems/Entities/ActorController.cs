using System;
using Godot;
using Halcyon.Entities.ActorCommands;

namespace Halcyon.Entities
{
    /// <summary> An AI controller for an actor entity. </summary>
    /// <remarks> Decisions are made using GOAP. </remarks>
    public class ActorController
    {
        /// <summary> A reference to the entity being controlled. </summary>
        private readonly ActorEntity ENTITY;


        /// <summary> An AI controller for an actor entity. </summary>
        /// <param name="entity"> A reference to the entity being controlled. </param>
        public ActorController(ActorEntity entity)
        {
            ENTITY = entity;
        }


        public void DoTest(Double delta)
        {
            Random random = new Random();
            Vector2 direction = new Vector2((random.NextSingle() - 0.5f) * 2f, (random.NextSingle() - 0.5f) * 2f);
            ENTITY.StateMachine.TryTransitionState(new WalkCommand(direction));
        }
    }
}
