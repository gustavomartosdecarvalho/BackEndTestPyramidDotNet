using System.Collections.Generic;
using System.Threading.Tasks;

using Alimentacao_App.Models;

namespace Alimentacao_App.Repository
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> Get();

        Task<Animal> Get(int Id);

        Task<Animal> Create(Animal animal);

        Task Update(Animal animal);

        Task Delete (int Id);
    }
}