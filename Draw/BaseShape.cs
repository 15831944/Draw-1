using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Draw
{
	[Serializable]
	public abstract class BaseShape
	{
		public abstract BaseShape copySelf();
		public abstract bool catchShape(Point testPoint);

		private bool isSelected = false;
		private Point p1;
		private Point p2;
		public Color penColor;
		public int penWidth;
		public void setSelected()
		{
			this.isSelected = true;
		}
		public void setUnSelected()
		{
			this.isSelected = false;
		}
		public Point getP1()
		{
			return p1;
		}
		public Point getP2()
		{
			return p2;
		}
		public void setP1(Point p1)
		{
			this.p1 = p1;
		}
		public void setP2(Point p2)
		{
			this.p2 = p2;
		}

		public void setHitPoint(int hitPointIndex,Point newPoint)
		{
			switch (hitPointIndex)
			{
				case 0:
					{
						Point tempPoint = new Point();
						tempPoint.X = this.getP1().X + newPoint.X;
						tempPoint.Y = this.getP1().Y + newPoint.Y;
						this.setP1(tempPoint);
						tempPoint.X = this.getP2().X + newPoint.X;
						tempPoint.Y = this.getP2().Y + newPoint.Y;
						this.setP2(tempPoint);
						break;
					}
				case 1:
					{
						this.setP1(newPoint);
						break;
					}
				case 2:
					{
						this.setP2(newPoint);
						break;
					}
			}
		}

		private Point[] getAllHitPoint()
		{
			Point[] allHitPoint = new Point[2];
			allHitPoint[0] = this.getP1();
			allHitPoint[1] = this.getP2();
			return allHitPoint;
		}

		public int catchShapePoint(Point testPoint)
		{
			int hitPointIndex = -1;
			Point[] allHitPoint = this.getAllHitPoint();
			for(int i = 0; i < allHitPoint.Length; i++)
			{
				if (Math.Pow((allHitPoint[i].X - testPoint.X), 2) + Math.Pow(allHitPoint[i].Y - testPoint.Y, 2) < 36)
				{
					return (i + 1);
				}
			}
			if (this.catchShape(testPoint))
			{
				return 0;
			}
			return hitPointIndex;
		}

		public void superDraw(Graphics g)
		{
			if (this.isSelected)
			{
				Point[] allHitPoint = this.getAllHitPoint();
				for(int i = 0; i < allHitPoint.Length; i++)
				{
					g.DrawEllipse(new Pen(Color.Red, 1), allHitPoint[i].X - 6, allHitPoint[i].Y - 6, 12, 12);
				}
			}
		}
	}
}
