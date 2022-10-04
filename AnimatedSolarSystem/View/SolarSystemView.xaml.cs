using AnimatedSolarSystem.Model;
using AnimatedSolarSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            this.CControl_Canvas.Content = SolarSystemViewModel.Canvas;
        }

        SolarObject so = new SolarObject(0, 0, 50, Brushes.Red);
        SolarObject so1 = new SolarObject(100, 0.2, 25, Brushes.Blue);
        SolarObject so2 = new SolarObject(250, 0.1, 20, Brushes.Blue);
        SolarObject so3 = new SolarObject(400, 0.05, 15, Brushes.Blue);
        SolarObject so4 = new SolarObject(550, 0.025, 10, Brushes.Blue);

        List<SolarObject> SolarSystem = new List<SolarObject>();
        private void Create_Btn_Click(object sender, RoutedEventArgs e)
        {

            SolarSystem.Add(so);
            SolarSystem.Add(so1);
            SolarSystem.Add(so2);
            SolarSystem.Add(so3);
            SolarSystem.Add(so4);


            foreach (var planet in SolarSystem)
            {
                SolarSystemViewModel.Canvas.Children.Add(planet.Shape);
            }

        }

        private void Stop_Btn_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }

        private void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            var task = Task.Run(() => MoveMyEllipse(tokenSource.Token));
        }

        private void ThreadTimer_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dispatcher_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private CancellationTokenSource tokenSource;



        private void MoveMyEllipse(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {

                foreach (var planet in SolarSystem)
                {
                    planet.Angle = planet.Angle + planet.Velocity;
                    Invoke(() =>
                    {
                        planet.X = planet.X0 + planet.Distance * Math.Sin(planet.Angle);
                        planet.Y = planet.Y0 + planet.Distance * Math.Cos(planet.Angle);
                        planet.Shape.Margin = new Thickness(planet.X, planet.Y, 0, 0);
                    });
                }

                
                Thread.Sleep(50);
            }
        }
        private void Invoke(Action action)
        {
            Dispatcher?.Invoke(action);
        }

        
    }
}
