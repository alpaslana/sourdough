using System.Reflection;
using Sourdough.DataManagement.DataModel;
using Sourdough.Extensions;
using Sourdough.IO;
using UnityEngine;

namespace Sourdough.DataManagement
{
    public class GameDataManager : MonoBehaviour
    {
        #region PROPS

        public static GameDataManager Instance { get; private set; }

        #endregion

        #region FIELDS

        [Header("SETTINGS")]
        [SerializeField] private string savedFileName;

        [Header("DATA SOURCE")]
        [SerializeField] private LocalDataSource localDataSource;

        #endregion

        #region UNITY EVENTS

        private void Awake() => Init();

        #endregion

        #region PRIVATE METHODS

        private void Init()
        {
            if (Instance == null) Instance = this;
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            if (!FileManager.CheckFile(savedFileName))
            {
                SaveJsonFile();
                return;
            }

            string jsonString = FileManager.OpenTextFile(savedFileName);
            localDataSource.gameDataModel = jsonString.ToJsonObject<GameDataModel>();
        }

        private void SaveJsonFile()
        {
            string jsonString = localDataSource.gameDataModel.ToJsonString();
            FileManager.SaveTextFile(savedFileName, jsonString);
        }

        #endregion

        #region PUBLIC METHODS

        public T GetGameData<T>() where T : class
        {
            foreach (FieldInfo fieldInfo in localDataSource.gameDataModel.GetType().GetFields())
            {
                if (fieldInfo.FieldType != typeof(T)) continue;
                return (T)fieldInfo.GetValue(localDataSource.gameDataModel);
            }

            return null;
        }

        public void SaveGameData()
        {
            SaveJsonFile();
        }
        
        public void DeleteSaveData()
        {
            FileManager.DeleteFile(savedFileName);
        }

        #endregion
    }
}