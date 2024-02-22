using System.Collections;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    /// <summary>
    /// Factory repository provider
    /// </summary>
    [CreateAssetMenu(fileName = "DatabaseRepoProvider", menuName = "ScriptableDatabase/RepositoryProvider")]
    public class ScriptableDatabaseRepoProvider : ScriptableObject, IDatabaseRepoProvider
    {
        [SerializeField] ScriptableDatabase Database;
        public IReadDatabaseRepository<SoundData> SoundRepository => GetSoundRepository();

        public IReadDatabaseRepository<VFXData> VFXRepository => GetVFXRepository();

        #if UNITY_EDITOR
        void OnValidate(){
            if(Database == null){
                Debug.LogError("Database is null");
            }
        }
        #endif

        public IEnumerator Initialize(){
            yield return Database.Initialize();
        }

        private IReadDatabaseRepository<VFXData> GetVFXRepository()
        {
            if(m_vfxRepository == null){
                m_vfxRepository = new VFXRepository(Database);
            }
            return m_vfxRepository;
        }

        private IReadDatabaseRepository<SoundData> GetSoundRepository(){
            if(m_soundRepository == null){
                m_soundRepository = new SoundRepository(Database);
            }
            return m_soundRepository;
        }


        #region Caches
        IReadDatabaseRepository<SoundData> m_soundRepository;
        IReadDatabaseRepository<VFXData> m_vfxRepository;
        #endregion Caches
    }
}