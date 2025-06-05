using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SaveGameManager : Singleton<SaveGameManager>
    {
        #region Default Data
        [SerializeField] private int defaultHighestLevel = 1;
        #endregion

        private List<BaseSaver> savers;
        private DataSaved dataSaved;

        protected override void Awake()
        {
            base.Awake();

            InitializeSavers();
        }

        protected virtual void Start()
        {
            LoadData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) SaveData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void InitializeSavers()
        {
            string jsonString = SaveSystem.HasKey("PLATFORMER2D_SAVEDATA")
                    ? SaveSystem.GetString("PLATFORMER2D_SAVEDATA")
                    : null;

            dataSaved = !string.IsNullOrEmpty(jsonString)
                    ? JsonUtility.FromJson<DataSaved>(jsonString)
                    : new DataSaved(defaultHighestLevel);


            savers = new()
            {
                new LevelSaver(dataSaved)
            };
        }

        private void LoadData()
        {
            //Set Game Data Saved to Game Running Data
            foreach (var saver in savers)
            {
                saver.LoadData();
            }
        }

        public void SaveData()
        {
            //Set Game Running Data to Game Data Saved
            foreach (var saver in savers)
            {
                saver.SaveData();
            }

            string jsonString = JsonUtility.ToJson(dataSaved);

            SaveSystem.SetString("PLATFORMER2D_SAVEDATA", jsonString);
            SaveSystem.SaveToDisk();
        }
    }
}
