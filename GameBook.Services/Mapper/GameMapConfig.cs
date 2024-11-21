using GameBook.Core.Models;
using GameBook.Core.ViewModels;
using Mapster;

namespace GameBook.Services.Mapper
{
    public class GameMapConfig
    {
        public static void RegisterGameConfig()
        {
            TypeAdapterConfig<Game, GameViewModel>.NewConfig();
        }    

        
    }

}

