using System;
using Godot;
using Halcyon.Utilities.Singletons;
using Warlord.Utilities;

namespace Halcyon.Managers
{
    /// <summary> The main game world's manager singleton. The program's entrypoint. </summary>
    public partial class GameManager : SingletonNode<GameManager>
    {
        /// <summary> The ratio between time passing in the real-world to time in game. </summary>
        /// <remarks> We can reverse time by setting this as a negative value. </remarks>
        [ExportGroup("Settings")]
        [Export] private Single _timeMultiplier = 12f;

        /// <summary> The current time within the game world. </summary>
        /// <remarks> Ideally we'd have this set through the editor. For now, we cannot. </remarks>
        public DateTime CurrentTime { get; private set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public DiceRandom DiceRandom { get; private set; } = new DiceRandom();

        /// <summary> Triggered every minute of game time. Indicates that the time has changed a 'significant' amount. </summary>
        public event Action<DateTime> TimeChanged = delegate { };


        /// <inheritdoc/>
        public override void _Ready()
        {
            GD.Print("Hello, World!");
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            Int32 previousMinute = CurrentTime.Minute;
            Double timeSinceLastFrame = delta * _timeMultiplier;
            CurrentTime = CurrentTime.AddSeconds(timeSinceLastFrame);

            if (CurrentTime.Minute != previousMinute)
            {
                TimeChanged.Invoke(CurrentTime);
            }
        }
    }
}
