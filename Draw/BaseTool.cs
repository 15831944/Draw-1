using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	public abstract class BaseTool
	{
		public abstract void mouseDown(object sender, MouseEventArgs e, DRAWForm drawForm);
		public abstract void mouseDrag(object sender, MouseEventArgs e);

		private bool isDraged = false;
		private DRAWForm refDRAWPanel = null;
		private Point downPoint = new Point();
		private Point newDragPoint = new Point();
		private Point oldDragPoint = new Point();
		private BaseShape operShape = null;

		public Point getDownPoint()
		{
			return downPoint;
		}
		public void setDownPoint(Point downPoint)
		{
			this.downPoint = downPoint;
		}
		public Point getNewDragPoint()
		{
			return newDragPoint;
		}
		public void setNewDragPoint(Point newDragPoint)
		{
			this.newDragPoint = newDragPoint;
		}
		public Point getOldDragPoint()
		{
			return oldDragPoint;
		}
		public void setOldDragPoint(Point oldDragPoint)
		{
			this.oldDragPoint = oldDragPoint;
		}
		public DRAWForm getRefDRAWPanel()
		{
			return refDRAWPanel;
		}
		public void setRefDRAWPanel(DRAWForm refDRAWPanel)
		{
			this.refDRAWPanel = refDRAWPanel;
		}
		public BaseShape getOperShape()
		{
			return operShape;
		}
		public void setOperShape(BaseShape operShape)
		{
			this.operShape = operShape;
		}

		public void superMouseUp(object sender, MouseEventArgs e)
		{
			this.setDownPoint(new Point());
			this.setOldDragPoint(new Point());
			this.setNewDragPoint(new Point());
			this.getRefDRAWPanel().record();
			this.getRefDRAWPanel().Refresh();
		}
		public void superMouseDown(object sender, MouseEventArgs e, DRAWForm drawForm)
		{
			this.setDownPoint(new Point(e.X, e.Y));
			this.setOldDragPoint(new Point(e.X, e.Y));
			this.setNewDragPoint(new Point(e.X, e.Y));
			this.mouseDown(sender, e, drawForm);
		}
		public void superMouseDrag(object sender, MouseEventArgs e)
		{
			isDraged = true;
			this.setNewDragPoint(new Point(e.X, e.Y));
			this.mouseDrag(sender, e);
			this.setOldDragPoint(this.getNewDragPoint());
		}
	}
}
