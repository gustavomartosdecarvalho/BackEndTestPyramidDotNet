using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alimentacao_App.Models;
using Alimentacao_App.Repository;

namespace Alimentacao_App.Controllers
{
    [ApiController]
    [Route("alimentacao/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepo;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepo = animalRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAnimals() // Task<IEnumerable<Animal>>
        {
            var response = await _animalRepo.Get();
            if (response.Count() == 0)
                return NoContent();
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAnimal(int id)
        {
            var response = await _animalRepo.Get(id);
            if(response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> PostAnimal([FromBody] Animal animal)
        {
            var newAnimal = await _animalRepo.Create(animal);
            if(newAnimal != null)
            {
                return CreatedAtAction(nameof(GetAnimal), new {id = newAnimal.Id}, newAnimal);
            }
            return BadRequest("Failed to create Animal");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAnimal(int id, [FromBody] Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest("The ids are not equals");
            }
            await _animalRepo.Update(animal);
            return Ok(animal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DelAnimal(int id)
        {
            var animalDelete = await _animalRepo.Get(id);
            if(animalDelete == null)
                return NotFound();
            await _animalRepo.Delete(animalDelete.Id);
            return NoContent();
        }
    }
}