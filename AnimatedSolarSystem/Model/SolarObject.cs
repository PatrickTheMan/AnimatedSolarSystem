﻿using System;
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
		public SolarObject(int distanceFromTheSun, double velocity, int size, Brush color)
		{
			this.Distance = distanceFromTheSun;
			this.Size = size;
			this.Velocity = velocity; // Rotation

			this.X0 = 350;
			this.Y0 = 100;



            Shape.Width = this.Size;
            Shape.Height = this.Size;

            Shape.Fill = color;
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

        

    }
}