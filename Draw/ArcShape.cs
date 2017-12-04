using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Draw
{
	[Serializable]
	class ArcShape : BaseShape
	{
		public override BaseShape copySelf()        //重写复制自身方法
		{
			ArcShape copyArcShape = new ArcShape();
			copyArcShape.setP1(this.getP1());        //复制起点
			copyArcShape.setP2(this.getP2());        //复制终点
			copyArcShape.penColor = this.penColor;
			copyArcShape.penWidth = this.penWidth;
			return copyArcShape;
		}

		public override bool catchShape(Point p3)
		{
			Point p1 = this.getP1();
			Point p2 = this.getP2();
			//int r = (int)Math.Pow(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2), 0.5);  //计算圆的半径
			int dx = (int)Math.Abs(getP2().X - getP1().X);
			int dy = (int)Math.Abs(getP2().Y - getP1().Y);
			int d = Math.Max(dx, dy);
			if (Math.Abs(Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2), 0.5) - d) < 3)
			{ return true; }
			else
			{ return false; }
		}
	}
}
