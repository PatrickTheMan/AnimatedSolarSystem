using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace AnimatedSolarSystem.Model
{
	public static class SolarSystemOperations
	{

		public static Ellipse CreateSun()
		{
			Ellipse Sun = new Ellipse();
			Sun.Fill = Brushes.Yellow;
			Sun.Width = 200;
			Sun.Height = 200;
			Sun.Margin = new Thickness(707, 307, 0, 0);
			Sun.StrokeThickness = 2;

			Sun.Stroke = Brushes.White;

			Canvas.SetZIndex(Sun, 0);

			return Sun;
		}

		public static List<SolarObject> CreatePlanets()
		{
			List<SolarObject> SolarSystem = new List<SolarObject>();

			SolarSystem.Add(new SolarObject("Mercury", 75 + 50, 0.479, 10, Brushes.Gray));
			SolarSystem.Add(new SolarObject("Venus", 100 + 50, 0.350, 20, Brushes.LightGoldenrodYellow));
			SolarSystem.Add(new SolarObject("Earth", 130 + 50, 0.298, 20, Brushes.Blue));
			SolarSystem.Add(new SolarObject("Mars", 155 + 50, 0.241, 10, Brushes.OrangeRed));
			SolarSystem.Add(new SolarObject("Jupiter", 275 + 50, 0.131, 100, Brushes.Wheat));
			SolarSystem.Add(new SolarObject("Saturn", 415 + 50, 0.97, 80, Brushes.SandyBrown));
			SolarSystem.Add(new SolarObject("Uranus", 505 + 50, 0.68, 50, Brushes.WhiteSmoke));
			SolarSystem.Add(new SolarObject("Neptun", 590 + 50, 0.54, 50, Brushes.LightBlue));

			int zCount = 1;
			foreach (SolarObject planet in SolarSystem)
			{
				Canvas.SetZIndex(planet.Shape, zCount);
				zCount++;
			}

			return SolarSystem;
		}

	}
}
