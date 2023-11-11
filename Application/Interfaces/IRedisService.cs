using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.DTO;

namespace Application.Interfaces;

public interface IRedisService
{
    Task Update(IList<ArticleCachedDTO> articles);

    Task<List<ArticleCachedDTO>> Read();

    Task<ArticleCachedDTO> ReadOne(long articleId);
}
