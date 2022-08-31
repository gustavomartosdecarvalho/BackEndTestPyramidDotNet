using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alimentacao_App.Models;
using Alimentacao_App.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Alimentacao_App.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalContext _context;

        public AnimalRepository(AnimalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> Get()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal> Get(int Id)
        {
            return await _context.Animals.FindAsync(Id);
        }

        public async Task<Animal> Create(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task Update(Animal animal)
        {
            _context.Entry(animal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var animalDelete = await _context.Animals.FindAsync(Id);
            if(animalDelete != null)
            {
               _context.Animals.Remove(animalDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}