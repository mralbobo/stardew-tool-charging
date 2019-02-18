using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ToolCharging
{
    /// <summary>The mod entry class loaded by SMAPI.</summary>
    public class ToolChargingMainClass : Mod
    {
        private int extraRemove = 40;
        //public ToolChargingConfig config;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            ModConfig config = helper.ReadConfig<ModConfig>();

            //config = new ToolChargingConfig().InitializeConfig(BaseConfigPath);
            extraRemove = config.IncreaseBy;
            debugLog("extraRemove" + extraRemove);
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        private void debugLog(string message)
        {
#if DEBUG
            this.Monitor.Log(message);
#endif
        }

        /// <summary>Raised after the game state is updated (≈60 times per second).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            int hold = Game1.player.toolHold;
            if (!Game1.player.canReleaseTool || hold <= 0)
                return; //either maxed or not being held

            if (hold - extraRemove <= 0)
                Game1.player.toolHold = 1;
            else
                Game1.player.toolHold -= extraRemove;
        }
    }

    public class ModConfig
    {
        private int _increaseBy;
        public int IncreaseBy
        {
            get { return _increaseBy; }
            set
            {
                if (value < -15) { _increaseBy = -15; }
                else if (value > 599) { _increaseBy = 599; }
                else { _increaseBy = value; }
            }
        }

        public ModConfig()
        {
            this._increaseBy = 40;
        }
    }
}
