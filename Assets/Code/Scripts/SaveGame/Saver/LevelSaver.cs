using UnityEngine;

namespace Platformer2D
{
    public class LevelSaver : BaseSaver
    {
        public LevelSaver(DataSaved dataSaved) : base(dataSaved)
        {
        }

        public override void LoadData()
        {
            LevelManager.Instance.CurrentHighestLevel = dataSaved.HighestLevel;
        }

        public override void SaveData()
        {
            dataSaved.HighestLevel = LevelManager.Instance.CurrentHighestLevel;
        }
    }
}