using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace Draw
{
	class HandTool : BaseTool
	{
		public int catchPointIndex = -1;
		public override void mouseDown(object sender, MouseEventArgs e, DRAWForm drawForm) {
			catchPointIndex = -1;
			if (this.getOperShape() != null)
			{
				this.getOperShape().setUnSelected();
			}
			ArrayList allShapes = this.getRefDRAWPanel().getCurrentShapes();
			int catchPoint = -1;
			int i = 0;
			for (; i < allShapes.Count; i++)
			{
				catchPoint = ((BaseShape)allShapes[i]).catchShapePoint(this.getOldDragPoint());
				if (catchPoint > -1)
				{
					break;
				}
			}
			if (catchPoint > -1)
			{
				catchPointIndex = catchPoint;
				((BaseShape)allShapes[i]).setSelected();
				this.setOperShape(((BaseShape)allShapes[i]));
			}
			this.getRefDRAWPanel().Refresh();
		}

		public override void mouseDrag(object sender, MouseEventArgs e)
		{
			if (this.getOperShape() != null)
			{
				Point setPoint = this.getNewDragPoint();
				if (catchPointIndex == 0)
				{
					setPoint = new Point();
					setPoint.X = this.getNewDragPoint().X - this.getOldDragPoint().X;
					setPoint.Y = this.getNewDragPoint().Y - this.getOldDragPoint().Y;
				}
				this.getOperShape().setHitPoint(catchPointIndex, setPoint);
				this.getRefDRAWPanel().Refresh();
			}
		}

	}

}
