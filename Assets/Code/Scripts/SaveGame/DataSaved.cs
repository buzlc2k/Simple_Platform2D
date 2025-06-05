using System;

namespace Platformer2D
{
    [Serializable]
    public class DataSaved
    {
        public int HighestLevel;

        public DataSaved(int CurrentHighestLevel)
        {
            this.HighestLevel = CurrentHighestLevel;
        }
    }
}