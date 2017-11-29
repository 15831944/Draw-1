using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw
{
	class RectangleTool : BaseTool
	{
		public override void mouseDown(object sender, MouseEventArgs e, DRAWForm drawForm)
		{
			this.setOperShape(new RectangleShape());
			this.getOperShape().setP1(this.getDownPoint());
			this.getOperShape().penColor = drawForm.clr;
			this.getOperShape().penWidth = drawForm.lineWidth;
			this.getRefDRAWPanel().getCurrentShapes().Add(this.getOperShape());
		}

		public override void mouseDrag(object sender, MouseEventArgs e)
		{
			this.getOperShape().setP2(this.getNewDragPoint());
			this.getRefDRAWPanel().Refresh();
		}
	}
}
