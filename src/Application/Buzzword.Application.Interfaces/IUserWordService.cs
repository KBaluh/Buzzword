using Buzzword.Application.Contracts.V1.Requests.UserWords;
using Buzzword.Application.Contracts.V1.Responses;

namespace Buzzword.Application.Interfaces
{
    public interface IUserWordService
    {
        Task<IList<UserWordDto>> GetUserWordsAsync(UserWordListQuery query, CancellationToken cancellationToken = default);
        Task<UserWordDto> GetUserWordAsync(Guid userWordId, CancellationToken cancellationToken = default);
        Task<Guid> AddWordAsync(AddWordRequest request);
        Task<UserWordDto> UpdateWordAsync(Guid userWordId, UpdateWordRequest request);
        Task<bool> RemoveWordAsync(Guid userWordId);
    }
}
