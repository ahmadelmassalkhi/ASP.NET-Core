using CVInfoApp.Models;

namespace CVInfoApp.Services
{
    public interface ICVService
    {
        public Task AddCV(CVBindModel cvBindModel);
        public List<CV> GetAllCVs();
        public Task DeleteCVAsync(long id);
    }
}
