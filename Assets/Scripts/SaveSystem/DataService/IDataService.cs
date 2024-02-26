using System.Threading;
using System.Threading.Tasks;
namespace Project.SaveSystem
{
    public interface IDataService
    {
        void Save<TObject>(string fileName, TObject data);
        Task<bool> SaveAsync<TObject>(string fileName, TObject data, CancellationToken cancellationToken = default);
        TObject Load<TObject>(string fileName);
        Task<TObject> LoadAsync<TObject>(string fileName, CancellationToken cancellationToken = default);
    }
}