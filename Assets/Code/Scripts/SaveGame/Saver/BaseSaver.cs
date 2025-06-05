using UnityEngine;

namespace Platformer2D
{
    public abstract class BaseSaver
    {
        protected DataSaved dataSaved;

        public BaseSaver(DataSaved dataSaved)
        {
            this.dataSaved = dataSaved;
        }

        public abstract void SaveData();
        public abstract void LoadData();
    }
}