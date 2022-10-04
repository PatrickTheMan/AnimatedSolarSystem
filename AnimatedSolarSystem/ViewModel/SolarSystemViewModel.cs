using AnimatedSolarSystem.Model;
using AnimatedSolarSystem.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AnimatedSolarSystem.ViewModel
{
    public class SolarSystemViewModel
    {

        public Canvas Canvas;

        public void NewCanvas()
        {
            this.Canvas = new Canvas();
			Canvas.Background = Brushes.Black;
		}



		private double yPerspective;

		public double YPerspective
		{
			get { return yPerspective; }
			set { yPerspective = value; }
		}



	}
}
