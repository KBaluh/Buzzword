using Buzzword.Application.Contracts.V1.Requests.UserWords;
using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Domain.DataContext;
using Buzzword.Application.Domain.Entities;
using Buzzword.Application.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Buzzword.Applicaiton.DomainServices
{
    public class UserWordService : IUserWordService
    {
        private readonly IApplicationDataSource _dataSource;

        public UserWordService(IApplicationDataSource applicationDataSource)
        {
            _dataSource = applicationDataSource;
        }

        public async Task<IList<UserWordDto>> GetUserWordsAsync(UserWordListQuery query, CancellationToken cancellationToken)
        {
            var items = await _dataSource.UserWords
                .Where(userWord => userWord.UserId == query.UserId)
                .Select(userWord => new UserWordDto
                {
                    Id = userWord.Id,
                    UserId = userWord.UserId,
                    Word = userWord.Word,
                    Translate = userWord.Translate
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return items;
        }

        public async Task<UserWordDto> GetUserWordAsync(Guid userWordId, CancellationToken cancellationToken)
        {
            var item = await _dataSource.UserWords
                .Where(userWord => userWord.Id == userWordId)
                .Select(userWord => new UserWordDto
                {
                    Id = userWord.Id,
                    UserId = userWord.UserId,
                    Word = userWord.Word,
                    Translate = userWord.Translate
                })
                .AsNoTracking()
                .FirstAsync(cancellationToken);
            return item;
        }

        public async Task<Guid> AddWordAsync(AddWordRequest request)
        {
            UserWord userWord = new UserWord
            {
                UserId = request.UserId,
                Word = request.Word,
                Translate = request.Translate
            };

            _dataSource.Entry(userWord).State = EntityState.Added;

            await _dataSource.SaveChangesAsync();

            return userWord.Id;
        }

        public async Task<UserWordDto> UpdateWordAsync(Guid userWordId, UpdateWordRequest request)
        {
            var userWord = await _dataSource.UserWords.FirstAsync(userWord => userWord.Id == userWordId);
            userWord.Word = request.Word;
            userWord.Translate = request.Translate;

            await _dataSource.SaveChangesAsync();

            return new UserWordDto
            {
                Id = userWord.Id,
                UserId = userWord.UserId,
                Word = userWord.Word,
                Translate = userWord.Translate
            };
        }

        public async Task<bool> RemoveWordAsync(Guid userWordId)
        {
            var item = await _dataSource.UserWords.FirstAsync(userWord => userWord.Id == userWordId);
            _dataSource.UserWords.Remove(item);
            int affectedRows = await _dataSource.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
