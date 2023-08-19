using System.Collections;
using UnityEngine;

namespace MathApp.UI
{
	public class MenuUI : SceneUI
	{
		[SerializeField]
		private float gameOverScreenDelay = 3f;

		// private UIDeathView deathView;

		private bool gameOverShown;
		private Coroutine gameOverCoroutine;

		public void RefreshCursorVisibility()
		{
			bool showCursor = false;

			for (int i = 0; i < Views.Length; i++)
			{
				var view = Views[i];

				if (view.IsOpen && view.NeedsCursor)
				{
					showCursor = true;
					break;
				}
			}

			// Context.Input.RequestCursorVisibility(showCursor, ECursorStateSource.UI);
		}

		protected override void OnInitializeInternal()
		{
			base.OnInitializeInternal();

			// deathView = Get<UIDeathView>();
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			// if (Context.Runner.Mode == Fusion.SimulationModes.Server)
			// {
			// 	Open<UIDedicatedServerView>();
			// }
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();

			if (gameOverCoroutine != null)
			{
				StopCoroutine(gameOverCoroutine);
				gameOverCoroutine = null;
			}

			gameOverShown = false;
		}

		protected override void OnTickInternal()
		{
			base.OnTickInternal();

			if (gameOverShown)
				return;
			// if (Context.Runner == null || Context.Runner.Exists(Context.GameplayMode.Object) == false)
			// 	return;
			//
			// var player = Context.NetworkGame.GetPlayer(Context.LocalPlayerRef);
			// if (player == null || player.Statistics.IsAlive == true)
			// {
			// 	deathView.Close();
			// }
			// else
			// {
			// 	deathView.Open();
			// }

			// if (Context.gameplayMode.State == EState.Finished && gameOverCoroutine == null)
			// {
			// 	gameOverCoroutine = StartCoroutine(ShowGameOver_Coroutine(gameOverScreenDelay));
			// }
		}

		protected override void OnViewOpened(UIView view)
		{
			RefreshCursorVisibility();
		}

		protected override void OnViewClosed(UIView view)
		{
			RefreshCursorVisibility();
		}

		private IEnumerator ShowGameOver_Coroutine(float delay)
		{
			yield return new WaitForSeconds(delay);

			gameOverShown = true;

			// deathView.Close();
			// Close<UIGameplayView>();
			// Close<UIScoreboardView>();
			// Close<UIGameplayMenu>();
			// Close<UIAnnouncementsView>();

			// Open<UIGameOverView>();

			gameOverCoroutine = null;
		}
	}
}
