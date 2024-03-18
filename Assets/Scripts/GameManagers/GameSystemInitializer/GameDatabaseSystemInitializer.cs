using System.Collections;
using UnityEngine;
using Project.GameDb.ScriptableDatabase;
using Project.GameDb;

namespace Project.Manager{
    public class GameDatabaseSystemInitializer : MonoSystemInitializer
    {
        [SerializeField] string filePath = "ScriptableDatabaseRepoProvider";
        ScriptableDatabaseRepoProvider m_scriptableDatabase; 
        protected override IEnumerator InitializeInternal(GameManager manager)
        {
            var request = Resources.LoadAsync<ScriptableDatabaseRepoProvider>(filePath);
            yield return request;
            m_scriptableDatabase = request.asset as ScriptableDatabaseRepoProvider;
            yield return m_scriptableDatabase.Initialize();

            manager.AddService<IDatabaseRepoProvider>(m_scriptableDatabase);
        }
    }
}