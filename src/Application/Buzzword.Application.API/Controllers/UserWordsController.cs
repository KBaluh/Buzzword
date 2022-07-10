using Buzzword.Application.Contracts.V1;
using Buzzword.Application.Contracts.V1.Requests.UserWords;
using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Buzzword.Application.API.Controllers
{
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _userWordService;
        private readonly ILogger<UserWordsController> _logger;

        public UserWordsController(IUserWordService userWordService, ILogger<UserWordsController> logger)
        {
            _userWordService = userWordService;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.UserWords.GetAll)]
        [ProducesResponseType(typeof(IList<UserWordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWordsAsync([FromQuery] UserWordListQuery query, CancellationToken cancellationToken)
        {
            var items = await _userWordService.GetUserWordsAsync(query, cancellationToken);
            return Ok(items);
        }

        [HttpGet(ApiRoutes.UserWords.Get, Name = nameof(GetUserWordAsync))]
        [ProducesResponseType(typeof(UserWordDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWordAsync(Guid userWordId, CancellationToken cancellationToken)
        {
            var item = await _userWordService.GetUserWordAsync(userWordId, cancellationToken);
            return Ok(item ?? new UserWordDto());
        }

        [HttpPost(ApiRoutes.UserWords.Create)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddWordAsync(AddWordRequest wordRequest)
        {
            var item = await _userWordService.AddWordAsync(wordRequest);
            return CreatedAtRoute(nameof(GetUserWordAsync), new { userId = wordRequest.UserId }, item);
        }

        [HttpPut(ApiRoutes.UserWords.Update)]
        [ProducesResponseType(typeof(UserWordDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWordAsync(Guid userWordId, UpdateWordRequest wordRequest)
        {
            var item = await _userWordService.UpdateWordAsync(userWordId, wordRequest);
            return Ok(item);
        }

        [HttpDelete(ApiRoutes.UserWords.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveWordAsync(Guid userWordId)
        {
            await _userWordService.RemoveWordAsync(userWordId);
            return Ok();
        }
    }
}
