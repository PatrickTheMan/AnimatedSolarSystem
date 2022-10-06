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
using System.Windows.Documents;
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
            PlanetOperations.MovePlanet(planet, yPerspective);
        }


        private List<SolarObject> solarSystem;

        public List<SolarObject> SolarSystem
        {
            get { return solarSystem; }
            set { solarSystem = value; }
        }


        public void CreateSolarSystem()
        {
            SolarSystem = new List<SolarObject>();

            Canvas = new Canvas();
            Canvas.Background = Brushes.Black;

            Ellipse Sun = SolarSystemOperations.CreateSun();
			Canvas.Children.Add(Sun);

            SolarSystem = SolarSystemOperations.CreatePlanets();

            foreach (var planet in SolarSystem)
            {
				Canvas.Children.Add(planet.Shape);
			}
			
        }

        public void FlipRender(SolarObject planet)
        {
            PlanetOperations.FlipRender(planet);
        }

        public void SetMargin(SolarObject planet)
        {
			planet.Shape.Margin = new Thickness(planet.X, planet.Y, 0, 0);
		}


    }
}
