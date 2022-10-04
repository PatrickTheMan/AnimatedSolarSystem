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
		}

        SolarObject Mercury = new SolarObject(75 + 50, 0.479, 10, Brushes.Gray);
        SolarObject Venus = new SolarObject(100 + 50, 0.350, 20, Brushes.LightGoldenrodYellow);
        SolarObject Earth = new SolarObject(130 + 50, 0.298, 20, Brushes.Blue);
        SolarObject Mars = new SolarObject(155 + 50, 0.241, 10, Brushes.OrangeRed);
		SolarObject Jupiter = new SolarObject(305 + 50, 0.131, 100, Brushes.Wheat);
		SolarObject Saturn = new SolarObject(415 + 50, 0.97, 80, Brushes.SandyBrown);
		SolarObject Uranus = new SolarObject(505 + 50, 0.68, 50, Brushes.WhiteSmoke);
		SolarObject Neptun = new SolarObject(590 + 50, 0.54, 50, Brushes.LightBlue);

        List<SolarObject> SolarSystem;
        private void Create_Btn_Click(object sender, RoutedEventArgs e)
        {
			this.SolarSystem = new List<SolarObject>();
			this.SolarSystemViewModel.NewCanvas();
			this.CControl_Canvas.Content = SolarSystemViewModel.Canvas;


			Ellipse Sun = new Ellipse();
            Sun.Fill = Brushes.Yellow;
			Sun.Width = 200;
			Sun.Height = 200;
			Sun.Margin = new Thickness(707, 307, 0, 0);
			Sun.StrokeThickness = 2;

			Sun.Stroke = Brushes.White;

			SolarSystemViewModel.Canvas.Children.Add(Sun);


			SolarSystem.Add(Mercury);
			//SolarSystem.Add(Venus);
			//SolarSystem.Add(Earth);
			//SolarSystem.Add(Mars);
			//SolarSystem.Add(Jupiter);
			//SolarSystem.Add(Saturn);
			//SolarSystem.Add(Uranus);
			//SolarSystem.Add(Neptun);


			Canvas.SetZIndex(Sun, 9);

			int count = 8;
			foreach (SolarObject planet in SolarSystem)
            {
				Canvas.SetZIndex(planet.Shape, count);
				count--;
				SolarSystemViewModel.Canvas.Children.Add(planet.Shape);
			}

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
			tokenSource = new CancellationTokenSource();
			var task = Task.Run(() => MoveMyEllipse(tokenSource.Token));
		}

		private void ScrollBar_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.SolarSystemViewModel.YPerspective = e.NewValue;
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

                        if (Math.Sin(planet.Angle) % 1.5807 <= -0.99)
                        {
							if (!planet.Front)
							{
								planet.Front = true;
								BringToFront(SolarSystemViewModel.Canvas, planet.Shape);
							}
						} else if (Math.Sin(planet.Angle) % 1.5807 >= 0.99)
                        {
							if (planet.Front)
							{
								planet.Front = false;
								BringToBack(SolarSystemViewModel.Canvas, planet.Shape);
							}
						}

                        planet.Y = planet.Y0 + planet.Distance * Math.Cos(planet.Angle) * SolarSystemViewModel.YPerspective;
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

		public void BringToFront(Canvas pParent, Ellipse pToMove)
		{

			int currentIndex = Canvas.GetZIndex(pToMove);

			int zIndex = 0;
			int maxZ = 0;
			Ellipse child;
			for (int i = 0; i < pParent.Children.Count; i++)
			{
				Debug.WriteLine(pParent.Children[i].GetType());

				if (pParent.Children[i] is Ellipse)
				{
					if (pParent.Children[i] != pToMove)
					{
						child = pParent.Children[i] as Ellipse;
						zIndex = Canvas.GetZIndex(child);
						maxZ = Math.Max(maxZ, zIndex);
						if (zIndex > currentIndex)
						{
							Canvas.SetZIndex(child, zIndex - 1);

							Debug.WriteLine(zIndex - 1);
						}
					} else
					{
						Debug.WriteLine("Dont move");
					}
				}
				else
				{
					Debug.WriteLine("Not a ellipse");
				}
			}
			Canvas.SetZIndex(pToMove, maxZ);
		}

		public void BringToBack(Canvas pParent, Ellipse pToMove)
		{

			int currentIndex = Canvas.GetZIndex(pToMove);

			int zIndex = 0;
			int maxZ = 0;
			Ellipse child;
			for (int i = pParent.Children.Count - 1; i > 0; i--)
			{
				Debug.WriteLine(pParent.Children[i].GetType());

				if (pParent.Children[i] is Ellipse)
				{
					if (pParent.Children[i] != pToMove)
					{
						child = pParent.Children[i] as Ellipse;
						zIndex = Canvas.GetZIndex(child);
						maxZ = Math.Max(maxZ, zIndex);
						if (zIndex > currentIndex)
						{
							Canvas.SetZIndex(child, zIndex + 1);

							Debug.WriteLine(zIndex + 1);
						}
					} else
					{
						Debug.WriteLine("Dont move");
					}
				}
				else
				{
					Debug.WriteLine("Not a ellipse");
				}
			}
			Canvas.SetZIndex(pToMove, maxZ);

		}

	}
}
