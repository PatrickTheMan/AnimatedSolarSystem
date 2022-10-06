using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimatedSolarSystem.Model
{
    public static class PlanetOperations
    {

        public static void FlipRender(SolarObject planet)
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

		public static void MovePlanet(SolarObject planet, double yPerspective)
		{
			planet.Angle = planet.Angle + planet.Velocity;
			planet.X = planet.X0 + planet.Distance * Math.Sin(planet.Angle);
			planet.Y = planet.Y0 + planet.Distance * Math.Cos(planet.Angle) * yPerspective;
		}

	}
}
