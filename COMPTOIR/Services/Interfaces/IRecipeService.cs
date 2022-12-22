using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Binding;
using COMPTOIR.Models.View;

namespace COMPTOIR.Services.Interfaces
{
    public interface IRecipeService
    {
        Recipe InitialRecipe(Product model,double price);
    }
}
