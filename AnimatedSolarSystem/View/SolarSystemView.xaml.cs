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
            this.CControl_Canvas.Content = SolarSystemViewModel.Canvas;
        }

        private void Stop_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (tokenSource.Token.CanBeCanceled)
            {
				tokenSource.Cancel();
			}
        }

        private void ThreadTimer_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dispatcher_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }

			tokenSource = new CancellationTokenSource();
			var task = Task.Run(() => MoveMyPlanet(tokenSource.Token));
		}

		private void ScrollBar_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.SolarSystemViewModel.YPerspective = e.NewValue;
		}


        private CancellationTokenSource tokenSource;



        private void MoveMyPlanet(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {

                foreach (var planet in SolarSystemViewModel.SolarSystem)
                {

                    SolarSystemViewModel.MovePlanet(planet);

                    Invoke(() =>
                    {
                        SolarSystemViewModel.FlipRender(planet);
                        planet.Shape.Margin = new Thickness(planet.X, planet.Y, 0, 0);
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
