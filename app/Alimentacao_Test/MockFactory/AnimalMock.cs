using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Alimentacao_App.Models;

namespace Alimentacao_Test.MockFactory
{
    public class AnimalMock
    {
        public static Animal NewAnimalDTO()
        {
            return new Animal
            { 
                Name = "New Animal",
                Portions = 3,
                WeightPortions = 100
            };
        }

        public static Animal NewAnimalId01()
        {
            return new Animal
            {  
                Id = 1,
                Name = "New Animal",
                Portions = 3,
                WeightPortions = 100
            };
        }


        public static Animal AnimalDTO_Name_Zero()
        {
            return new Animal
            {  
                Name = "",
                Portions = 3,
                WeightPortions = 100
            };
        }




        public static List<Animal> NewAnimalsList()
        {
            return new List<Animal>{ 
                new Animal{  
                    Id = 1,
                    Name = "First Animal",
                    Portions = 6,
                    WeightPortions = 100
                },
                new Animal{  
                    Id = 2,
                    Name = "Second Animal",
                    Portions = 3,
                    WeightPortions = 200
                },
                new Animal{  
                    Id = 3,
                    Name = "Third Animal",
                    Portions = 1,
                    WeightPortions = 600
                }
            };
        }

        public static List<Animal> EmptyAnimalsList()
        {
            return new List<Animal>{          
            };
        }
    }
}