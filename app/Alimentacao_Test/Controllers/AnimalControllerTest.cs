using System;
using System.Net;
using Xunit;
using Xunit.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Alimentacao_Test.MockFactory;
using Alimentacao_App.Models;
using Alimentacao_App.Controllers;
using Alimentacao_App.Repository;

namespace Alimentacao_Test.ControllersTest
{
    public class AnimalControllerTest
    {

        private readonly ITestOutputHelper _outputHelper;

        public AnimalControllerTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }



        //GetId
        [Fact]
        public async Task GetAnimal_StatusCode_200()
        {
            // Given
            // In this example we do not use MockFactory to show the possibilities
            Animal newAnimal = new Animal
            {  
                Id = 1,
                Name = "Novo Animal",
                Portions = 3,
                WeightPortions = 100
            };
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            var controller = new AnimalController(repositoryMock.Object);

            // When
            var response = await controller.GetAnimal(1) as ObjectResult;

            // Assert
            Assert.Equal(200, response.StatusCode);
        }
        
        [Fact]
        public async Task GetAnimal_Return_Id_Is_1()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            var controller = new AnimalController(repositoryMock.Object);

            // When
            var response = await controller.GetAnimal(1) as OkObjectResult;
            var result = response.Value as Animal;

            // Assert
            Assert.Equal(newAnimal.Id, result.Id);
        }
        
        [Fact]
        public async Task GetAnimal_Return_Name_Is_NewAnimal()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            var controller = new AnimalController(repositoryMock.Object);

            // When
            var response = await controller.GetAnimal(1) as OkObjectResult;
            var result = response.Value as Animal;
            //If test fail show output
            _outputHelper.WriteLine(result.Name);

            // Assert
            Assert.Equal(newAnimal.Name, result.Name);
        }
        
        [Fact]
        public async Task GetAnimal_Return_Portions_Is_3()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            var controller = new AnimalController(repositoryMock.Object);

            // When
            var response = await controller.GetAnimal(1) as OkObjectResult;
            var result = response.Value as Animal;

            // Assert
            Assert.Equal(newAnimal.Portions, result.Portions);
        }
        
        [Fact]
        public async Task GetAnimal_Return_WeightPortions_Is_100()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            var controller = new AnimalController(repositoryMock.Object);

            // When
            var response = await controller.GetAnimal(1) as OkObjectResult;
            var result = response.Value as Animal;

            // Assert
            Assert.Equal(newAnimal.WeightPortions, result.WeightPortions);
        }

        [Fact]
        public async Task GetAnimal_StatusCode_404()
        {
            // Given
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(100)).ReturnsAsync((Animal)null);
            
            // When
            var controller = new AnimalController(repositoryMock.Object);
            var response = await controller.GetAnimal(100) as NotFoundResult;

            // Assert
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetAnimal_Return_Null_Animal()
        {
            // Given
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(100)).ReturnsAsync((Animal)null);
            
            // When
            var controller = new AnimalController(repositoryMock.Object);
            var response = await controller.GetAnimal(100) as ObjectResult;

            // Assert
            Assert.Null(response);
        }



        //Get All
        [Fact]
        public async Task GetAnimals_StatusCode_200()
        {
            // Given
            List<Animal> animalList = AnimalMock.NewAnimalsList();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get()).ReturnsAsync(animalList);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = (OkObjectResult)await controller.GetAnimals();

            // Assert
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAnimals_Count_3()
        {
            // Given
            List<Animal> animalList = AnimalMock.NewAnimalsList();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get()).ReturnsAsync(animalList);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.GetAnimals() as OkObjectResult;
            var result = response.Value as List<Animal>;

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAnimals_StatusCode_204()
        {
            //Given
            List<Animal> animalList = AnimalMock.EmptyAnimalsList();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get()).ReturnsAsync(animalList);
            var controller = new AnimalController(repositoryMock.Object);

            //When
            var response = (NoContentResult)await controller.GetAnimals();

            //Assert
            Assert.Equal(204, response.StatusCode);
        }



        //Post 
        [Fact]
        public async Task PostAnimal_StatusCode_201()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.NewAnimalDTO();
            Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).ReturnsAsync(createdAnimal);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as ObjectResult;
        
            // Then
            Assert.Equal(201, (int)response.StatusCode);
        }
         
        [Fact]
        public async Task PostAnimal_Return_Id_is_1()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.NewAnimalDTO();
            Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).ReturnsAsync(createdAnimal);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as ObjectResult;
            var result = response.Value as Animal;
        
            // Then
            Assert.Equal(createdAnimal.Id, result.Id);
        }
         
        [Fact]
        public async Task PostAnimal_Return_Name_is_NewAnimal()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.NewAnimalDTO();
            Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).ReturnsAsync(createdAnimal);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as ObjectResult;
            var result = response.Value as Animal;
        
            // Then
            Assert.Equal(createdAnimal.Name, result.Name);
        }
         
        [Fact]
        public async Task PostAnimal_Return_Portions_is_3()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.NewAnimalDTO();
            Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).ReturnsAsync(createdAnimal);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as ObjectResult;
            var result = response.Value as Animal;
        
            // Then
            Assert.Equal(createdAnimal.Portions, result.Portions);
        }
         
        [Fact]
        public async Task PostAnimal_Return_WeightPortions_is_100()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.NewAnimalDTO();
            Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).ReturnsAsync(createdAnimal);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as ObjectResult;
            var result = response.Value as Animal;
        
            // Then
            Assert.Equal(createdAnimal.WeightPortions, result.WeightPortions);
        }

        /*[Fact]
        public async Task PostAnimal_Return_NameCharZero_StatusCode400()
        {
            // Given
            Animal newAnimalDTO = AnimalMock.AnimalDTO_Name_Zero();
            //Animal createdAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Create(newAnimalDTO)).Throws(new Exception());
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PostAnimal(newAnimalDTO) as BadRequestObjectResult;
        
            // Then
            //Assert.Equal(400, response.StatusCode);
        }*/

        //"Nome deve conter de 2 a 30 caracteres"
        //"Porções deve ser maior que zero"
        //"Peso por Porção deve ser maior que zero"





        //Put
        [Fact]
        public async Task PutAnimal_StatusCode_200()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Update(newAnimal));
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PutAnimal(newAnimal.Id, newAnimal) as OkObjectResult;
        
            // Then
            Assert.Equal(200, response.StatusCode);
        }

        /*[Fact]   
        public async void PutAnimal_StatusCode404()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Update((Animal)null));
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PutAnimal(newAnimal.Id, newAnimal) as NotFoundResult;

            bool result;
            if (response == null)
                _outputHelper.WriteLine("Is Null");
            else
                _outputHelper.WriteLine("Not Null");
        
            // Then
            Assert.Equal(404, response.StatusCode);
        }*/

        [Fact]
        public async Task PutAnimal_StatusCode_400()
        {
            // Given
            int idAnimalFail = 2;
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Update(newAnimal));
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PutAnimal(idAnimalFail, newAnimal) as BadRequestObjectResult;
        
            // Then 
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task PutAnimal_MessageError()
        {
            // Given
            int idAnimalFail = 2;
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Update(newAnimal));
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.PutAnimal(idAnimalFail, newAnimal) as BadRequestObjectResult;
        
            // Then
            Assert.Equal("The ids are not equals", response.Value);
        }



        //Delete
        [Fact]
        public async Task DelAnimal_StatusCode_204()
        {
            // Given
            Animal newAnimal = AnimalMock.NewAnimalId01();
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(newAnimal.Id)).ReturnsAsync(newAnimal);
            repositoryMock.Setup(mock => mock.Delete(newAnimal.Id));
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.DelAnimal(newAnimal.Id) as NoContentResult;
        
            // Then
            Assert.Equal(204, response.StatusCode);
        }

        [Fact]
        public async void DelAnimal_StatusCode_404()
        {
            // Given
            int idAnimal = 100;
            var repositoryMock = new Mock<IAnimalRepository>();
            repositoryMock.Setup(mock => mock.Get(idAnimal)).ReturnsAsync((Animal)null);
            var controller = new AnimalController(repositoryMock.Object);
        
            // When
            var response = await controller.DelAnimal(idAnimal) as NotFoundResult;
        
            // Then
            Assert.Equal(404, response.StatusCode);
        }
    }
}