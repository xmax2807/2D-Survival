using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    /// <summary>
    /// Factory repository provider
    /// </summary>
    [CreateAssetMenu(fileName = "DatabaseRepoProvider", menuName = "ScriptableDatabase/RepositoryProvider")]
    public class ScriptableDatabaseRepoProvider : ScriptableObject, IDatabaseRepoProvider, IDatabaseReposSubscription
    {
        [SerializeField] ScriptableDatabase Database;

        #if UNITY_EDITOR
        void OnValidate(){
            if(Database == null){
                Debug.LogError("Database is null");
            }
        }
        #endif

        public IEnumerator Initialize(){
            yield return Database.Initialize();
            AddRepository<ISoundRepository>(new SoundRepository(Database));
            AddRepository<IVFXRepository>(new VFXRepository(Database));
        }

        public TRepository GetRepository<TRepository>()
        {
            return (TRepository)m_repositories[typeof(TRepository).Name];
        }

        public void AddRepository<TRepository>(TRepository instance)
        {
            if(!m_repositories.ContainsKey(typeof(TRepository).Name)){
                m_repositories.Add(typeof(TRepository).Name, instance);
            }
        }

        public void RemoveRepository<TRepository>()
        {
            if(m_repositories.ContainsKey(typeof(TRepository).Name)){
                m_repositories.Remove(typeof(TRepository).Name);
            }
        }


        #region Caches
        Dictionary<string, object> m_repositories = new();
        #endregion Caches
    }
}