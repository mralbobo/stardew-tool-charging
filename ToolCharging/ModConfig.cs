namespace ToolCharging
{
    internal class ModConfig
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
