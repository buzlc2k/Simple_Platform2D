using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer2D
{
    [Serializable]
    public class Level
    {
        public int Index;
        public string ConfigPath;
    }

    public class LevelManager : Singleton<LevelManager>
    {
        private int currentKey = 0;
        private LevelConfig currentLevel;
        [SerializeField] private int currentHighestLevel;
        [SerializeField] private List<Level> levels;

        public int CurrentKey { get => currentKey; }
        public LevelConfig CurrentLevel { get => currentLevel; }
        public int CurrentHighestLevel { get => currentHighestLevel; set => currentHighestLevel = value; }
        public int TotalLevel { get => levels.Count; }
        public bool IsLoading { get; private set; }

        public void AddCurrentKey() => currentKey++;

        public void LoadNextLevel()
        {
            if (currentKey < currentLevel.TargetKeys) return;

            StartCoroutine(C_LoadLevel(currentLevel.Index + 1));
        }

        public void ResetCurrentLevel()
        {
            StartCoroutine(C_LoadLevel(currentLevel.Index));
        }

        public void LoadLevel(int index)
        {
            StartCoroutine(C_LoadLevel(index));
        }

        private Level GetLevelFromIndex(int index)
        {
            foreach (var level in levels)
                if (level.Index == index) return level;

            return null;
        }

        private IEnumerator C_LoadLevel(int index)
        {
            IsLoading = true;

            if (index > levels.Count)
            {
                ((ISMContext<GameStateID>)GameManager.Instance).ChangeState(GameStateID.Winning);
                IsLoading = false;
                yield break;
            }

            if (index > currentHighestLevel)
                currentHighestLevel = index;

            var levelLoaded = GetLevelFromIndex(index);
            PlayerController.Instance.gameObject.SetActive(false);

            // Hủy level trước đó
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
                yield return null;
            }

            yield return new WaitForSeconds(2);

            // Tải cấu hình level
            var levelLoadedConfig = Resources.Load<LevelConfig>(levelLoaded.ConfigPath);
            if (levelLoadedConfig == null)
            {
                Debug.LogError($"Failed to load level config at path: {levelLoaded.ConfigPath}");
                IsLoading = false;
                yield break;
            }
            yield return null;

            // Tạo instance của level mới
            Instantiate(levelLoadedConfig.Prefab, transform);
            yield return null;

            // Reset current key và cập nhật currentLevel
            currentKey = 0;
            currentLevel = levelLoadedConfig;

            yield return new WaitForSeconds(2);

            // Reset vị trí Player
            PlayerController.Instance.transform.position = levelLoadedConfig.SpawnPos;
            PlayerController.Instance.gameObject.SetActive(true);

            yield return null;

            IsLoading = false;
        }

        public IEnumerator ReloadGame(float offsetTime = 0)
        {
            IsLoading = true;

            SaveGameManager.Instance.SaveData();
            
            float currentTime = 0;
            while (currentTime <= offsetTime)
            {
                currentTime += Time.unscaledDeltaTime;
                yield return null;
            }
            
            AsyncOperation reloadAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

            while (!reloadAsync.isDone)
                yield return null;
        }
    }
}