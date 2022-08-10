using VContainer;

namespace Core.Controllers
{
	public class ControllerFactory : IControllerFactory
	{
		private readonly IObjectResolver _resolver;

		public ControllerFactory(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public T CreateController<T>() where T : BaseController
		{
			return _resolver.Resolve<T>();
		}
	}
}