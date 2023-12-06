
using Application.Interfaces.Common;

namespace Application.Interfaces.IRepositories.Rabbit
{
    public interface IRabbitRepository
    {
        public Task<IEnumerable<Boolean>> publicar( string? parametro);
    }
}
