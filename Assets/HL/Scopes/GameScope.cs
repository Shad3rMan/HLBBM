using System;
using Core.Controllers;
using Core.Logging;
using HL.Controllers;
using HL.Modules.MainMenu.Controllers;
using HL.Modules.MainMenu.Views;
using HL.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ILogger = Core.Logging.ILogger;

namespace HL.Scopes
{
	public class GameScope : LifetimeScope
	{

protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<IControllerFactory, ControllerFactory>(Lifetime.Singleton);
			builder.Register<ISavesService>(Lifetime.Scoped);
			builder.Register<ILogger, HlLogger>(Lifetime.Singleton);
			builder.RegisterEntryPoint<GameEntryPoint>(Lifetime.Scoped);

			RegisterControllers(builder);
			RegisterViews(builder);
		}

		private void RegisterControllers(IContainerBuilder builder)
		{
			builder.Register<GameStartupController>(Lifetime.Transient);
			builder.Register<MainMenuController>(Lifetime.Scoped);
			builder.Register<LevelSelectionController>(Lifetime.Scoped);
		}

		private void RegisterViews(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<MenuCanvasView>();
			builder.RegisterComponentInHierarchy<LevelSelectionView>();
			builder.RegisterComponentInHierarchy<MainMenuView>();
		}
	}
}