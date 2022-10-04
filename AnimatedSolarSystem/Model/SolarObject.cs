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

namespace AnimatedSolarSystem.Model
{
    public class SolarObject
    {
		public Ellipse Shape = new Ellipse();
		public SolarObject(string name, int distanceFromTheSun, double velocity, int size, Brush color)
		{
			this.Name = name;
			this.Distance = distanceFromTheSun;
			this.Size = size;
			this.Velocity = velocity / 30; // Rotation

			this.X0 = 800;
			this.Y0 = 400;

            Shape.Width = this.Size;
            Shape.Height = this.Size;

			Shape.StrokeThickness = 2;

			Shape.Stroke = Brushes.White;
			Shape.Fill = color;

        }

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		private double x;

		public double X
		{
			get { return x; }
			set { x = value; }
		}

		private double y;

		public double Y
		{
			get { return y; }
			set { y = value; }
		}

		private double x0;

		public double X0
		{
			get { return x0; }
			set { x0 = value; }
		}

		private double y0;

		public double Y0
		{
			get { return y0; }
			set { y0 = value; }
		}

		private double angle;

		public double Angle
		{
			get { return angle; }
			set { angle = value; }
		}


		private int distance;

		public int Distance
		{
			get { return distance; }
			set { distance = value; }
		}

		private int size;

		public int Size
		{
			get { return size; }
			set { size = value; }
		}

		private double velocity;

		public double Velocity
        {
			get { return velocity; }
			set { velocity = value; }
		}

		private bool front = true;

		public bool Front
		{
			get { return front; }
			set { front = value; }
		}


	}
}
