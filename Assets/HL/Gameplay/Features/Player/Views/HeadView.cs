using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Features.Player.Components;

namespace HL.Gameplay.Features.Player.Views
{
	public class HeadView : Actor<Head>
	{
		protected override void Initialize()
		{
			base.Initialize();

			Add<Components.Player>();
			Add<Input>();
		}
	}
}