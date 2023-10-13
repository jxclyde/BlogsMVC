using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface ITagRepository
    {
        Task <IEnumerable<Tag>> GetAllAsync();

        Task <Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);

    }
}
