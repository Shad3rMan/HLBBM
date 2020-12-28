namespace Core.Controllers
{
    public interface IControllerFactory
    {
        T CreateController<T>() where T : BaseController;
    }
}
