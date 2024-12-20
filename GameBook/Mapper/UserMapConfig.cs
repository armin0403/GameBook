using GameBook.Core.Models;
using GameBook.Web.ViewModels;
using Mapster;

namespace GameBook.Mapper
{
    public class UserMapConfig
    {
        public static void UserMapperConfig()
        {
            TypeAdapterConfig<RegisterViewModel, User>.NewConfig();
        }
    }
}
