using AnimatedSolarSystem.Model;
using AnimatedSolarSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AnimatedSolarSystem.View
{
    /// <summary>
    /// Interaction logic for SolarSystemView.xaml
    /// </summary>
    public partial class SolarSystemView : UserControl
    {
        SolarSystemViewModel SolarSystemViewModel = new SolarSystemViewModel();
        public SolarSystemView()
        {
            InitializeComponent();
            this.DataContext = SolarSystemViewModel;
		}

        private void Create_Btn_Click(object sender, RoutedEventArgs e)
        {
            SolarSystemViewModel.CreateSolarSystem();

            // Set the new canvas as the current one
            this.CControl_Canvas.Content = SolarSystemViewModel.Canvas;
        }

        private void Stop_Btn_Click(object sender, RoutedEventArgs e)
        {
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{ // Cancle token so it stops accosiated tasks
				tokenSource.Cancel();
			}
			if (timerThread != null)
			{ // Remove timerThread
				timerThread.Dispose();
			}
			if (dispatcherTimer.IsEnabled)
			{ // Stop current DispatchTimer if running
				dispatcherTimer.Stop();
			}
		}

		Timer timerThread;
        private void ThreadTimer_Btn_Click(object sender, RoutedEventArgs e)
        {
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			if (timerThread != null)
			{ // Stop Timer if running
				timerThread.Dispose();
			}
			if (dispatcherTimer.IsEnabled)
			{ // Stop current DispatchTimer if running
				dispatcherTimer.Stop();
			}
			tokenSource = new CancellationTokenSource();

			timerThread = new Timer(
					(o) => { MoveMyPlanetTimerThread(tokenSource.Token); },
					new AutoResetEvent(true),
					0,
					25
				);
		}

        private void Dispatcher_Btn_Click(object sender, RoutedEventArgs e)
        {
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			if (timerThread != null)
			{ // Stop Timer if running
				timerThread.Dispose();
			}
			if (dispatcherTimer.IsEnabled)
			{ // Stop current DispatchTimer if running
				dispatcherTimer.Stop();
			}
			tokenSource = new CancellationTokenSource();

            // Start
			var task = Task.Run(() => MoveMyPlanetDispatcher(tokenSource.Token));
		}

		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		private void DispatcherTimer_Btn_Click(object sender, RoutedEventArgs e)
        {
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			if (timerThread != null)
			{ // Stop Timer if running
				timerThread.Dispose();
			}
			if (dispatcherTimer.IsEnabled)
			{ // Stop current DispatchTimer if running
				dispatcherTimer.Stop();
			}
			else
			{ // Setup DispatchTimer
				dispatcherTimer.Tick += (sender2, e2) => { MoveMyPlanetDispatchTimer(tokenSource.Token); };
				dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
			}
			tokenSource = new CancellationTokenSource();

            // Start
			dispatcherTimer.Start();
		}

		private void ScrollBar_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.SolarSystemViewModel.YPerspective = e.NewValue;
		}


        private CancellationTokenSource tokenSource;

		private void MoveMyPlanetTimerThread(CancellationToken token)
		{
			if (!token.IsCancellationRequested)
			{
				foreach (var planet in SolarSystemViewModel.SolarSystem)
				{
					Invoke(() =>
					{
						SolarSystemViewModel.MovePlanet(planet);
						SolarSystemViewModel.FlipRender(planet);
						SolarSystemViewModel.SetMargin(planet);
					});
				}
			}
		}
		private void MoveMyPlanetDispatchTimer(CancellationToken token)
		{
			if (!token.IsCancellationRequested)
			{
				foreach (var planet in SolarSystemViewModel.SolarSystem)
				{
					SolarSystemViewModel.MovePlanet(planet);
					SolarSystemViewModel.FlipRender(planet);
					SolarSystemViewModel.SetMargin(planet);
				}
			}
		}
		private void MoveMyPlanetDispatcher(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var planet in SolarSystemViewModel.SolarSystem)
                {
                    SolarSystemViewModel.MovePlanet(planet);
                    Invoke(() =>
                    {
                        SolarSystemViewModel.FlipRender(planet);
                        SolarSystemViewModel.SetMargin(planet);
                    });

                }
                    Thread.Sleep(25);
            }
        }
        private void Invoke(Action action)
        {
            Dispatcher?.Invoke(action);
        }

        
    }
}
