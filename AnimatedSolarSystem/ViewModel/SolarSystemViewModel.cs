using AnimatedSolarSystem.Model;
using AnimatedSolarSystem.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AnimatedSolarSystem.ViewModel
{
    public class SolarSystemViewModel
    {

        public Canvas Canvas;

		private double yPerspective;

		public double YPerspective
		{
			get { return yPerspective; }
			set { yPerspective = value; }
		}


        public void MovePlanet(SolarObject planet)
        {
            planet.Angle = planet.Angle + planet.Velocity;
            planet.X = planet.X0 + planet.Distance * Math.Sin(planet.Angle);
            planet.Y = planet.Y0 + planet.Distance * Math.Cos(planet.Angle) * this.YPerspective;
        }


        private List<SolarObject> solarSystem;

        public List<SolarObject> SolarSystem
        {
            get { return solarSystem; }
            set { solarSystem = value; }
        }


        public void CreateSolarSystem()
        {
            this.SolarSystem = new List<SolarObject>();

            this.Canvas = new Canvas();
            Canvas.Background = Brushes.Black;


            Ellipse Sun = new Ellipse();
            Sun.Fill = Brushes.Yellow;
            Sun.Width = 200;
            Sun.Height = 200;
            Sun.Margin = new Thickness(707, 307, 0, 0);
            Sun.StrokeThickness = 2;

            Sun.Stroke = Brushes.White;

            Canvas.Children.Add(Sun);

            SolarSystem.Add(new SolarObject("Mercury", 75 + 50, 0.479, 10, Brushes.Gray));
            SolarSystem.Add(new SolarObject("Venus", 100 + 50, 0.350, 20, Brushes.LightGoldenrodYellow));
            SolarSystem.Add(new SolarObject("Earth", 130 + 50, 0.298, 20, Brushes.Blue));
            SolarSystem.Add(new SolarObject("Mars", 155 + 50, 0.241, 10, Brushes.OrangeRed));
            SolarSystem.Add(new SolarObject("Jupiter", 275 + 50, 0.131, 100, Brushes.Wheat));
            SolarSystem.Add(new SolarObject("Saturn", 415 + 50, 0.97, 80, Brushes.SandyBrown));
            SolarSystem.Add(new SolarObject("Uranus", 505 + 50, 0.68, 50, Brushes.WhiteSmoke));
            SolarSystem.Add(new SolarObject("Neptun", 590 + 50, 0.54, 50, Brushes.LightBlue));


            Canvas.SetZIndex(Sun, 0);

            int count = 1;
            foreach (SolarObject planet in SolarSystem)
            {
                Canvas.SetZIndex(planet.Shape, count);
                count++;

                Debug.WriteLine("Count: " + count);

                Canvas.Children.Add(planet.Shape);
            }
        }

        public void FlipRender(SolarObject planet)
        {
            if (Math.Sin(planet.Angle) % 1.5807 <= -0.99)
            {
                if (!planet.Front)
                {
                    planet.Front = true;
                    Canvas.SetZIndex(planet.Shape, -Canvas.GetZIndex(planet.Shape));
                }
            }
            else if (Math.Sin(planet.Angle) % 1.5807 >= 0.99)
            {
                if (planet.Front)
                {
                    planet.Front = false;
                    Canvas.SetZIndex(planet.Shape, -Canvas.GetZIndex(planet.Shape));
                }
            }
        }


    }
}
