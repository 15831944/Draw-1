using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Draw
{
	public partial class DRAWForm : Form
	{
		private BaseTool currentTool = null;
		private ArrayList currentShapes = null;
		private ArrayList historyShapes = null;
		public Color clr = Color.Black;
		public int lineWidth = 1;
		private int undoIndex = 0;
		private bool isMouseDown = false;

		public ArrayList getCurrentShapes()
		{
			return currentShapes;
		}
		public void setCurrentShapes(ArrayList currentShapes)
		{
			this.currentShapes = currentShapes;
		}
		public BaseTool getCurrentTool()
		{
			return currentTool;
		}
		public void setCurrentTool(BaseTool currentTool)
		{
			this.currentTool = currentTool;
		}
		public ArrayList getHistoryShapes()
		{
			return historyShapes;
		}
		public void setHistoryShapes(ArrayList historyShapes)
		{
			this.historyShapes = historyShapes;
		}


		public DRAWForm()
		{
			InitializeComponent();
			currentShapes = new ArrayList();
			historyShapes = new ArrayList();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void DRAWForm_Load(object sender, EventArgs e)
		{
			txtLineWidth_TextChanged(null, null);
			this.getHistoryShapes().Add(this.cloneShapeArray(this.getCurrentShapes()));
		}

		private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
		{

		}

		private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{

		}

		private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void Black_Click(object sender, EventArgs e)
		{
			clr = Color.Black;
			picCurrentColor.BackColor = clr;
		}

		private void Cyan_Click(object sender, EventArgs e)
		{
			clr = Color.Cyan;
			picCurrentColor.BackColor = clr;
		}

		private void Red_Click(object sender, EventArgs e)
		{
			clr = Color.Red;
			picCurrentColor.BackColor = clr;
		}

		private void Magente_Click(object sender, EventArgs e)
		{
			clr = Color.Magenta;
			picCurrentColor.BackColor = clr;
		}

		private void Green_Click(object sender, EventArgs e)
		{
			clr = Color.Green;
			picCurrentColor.BackColor = clr;
		}

		private void Orange_Click(object sender, EventArgs e)
		{
			clr = Color.Orange;
			picCurrentColor.BackColor = clr;
		}

		private void Yellow_Click(object sender, EventArgs e)
		{
			clr = Color.Yellow;
			picCurrentColor.BackColor = clr;
		}

		private void Blue_Click(object sender, EventArgs e)
		{
			clr = Color.Blue;
			picCurrentColor.BackColor = clr;
		}

		private void btnMoreColor_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				clr = colorDialog1.Color;
				picCurrentColor.BackColor = clr;
			}
		}

		private void tbarLineWidth_Scroll(object sender, EventArgs e)
		{
			this.txtLineWidth.Text = tbarLineWidth.Value.ToString();
		}

		private void txtLineWidth_TextChanged(object sender, EventArgs e)
		{
			Bitmap bit = new Bitmap(picLineWidth.Width, picLineWidth.Height);
			Graphics g = Graphics.FromImage(bit);
			Pen pen = new Pen(clr, int.Parse(this.txtLineWidth.Text));
			Point p1 = new Point();
			Point p2 = new Point();
			p1.X = 0;
			p1.Y = picLineWidth.Height / 2;
			p2.X = picLineWidth.Width;
			p2.Y = picLineWidth.Height / 2;
			g.DrawLine(pen, p1, p2);
			picLineWidth.Image = bit;
			lineWidth = int.Parse(this.txtLineWidth.Text);
		}

		public ArrayList cloneShapeArray(ArrayList shapeArrayList)
		{
			ArrayList returnShapeArrayList = new ArrayList();
			for(int i = 0; i<shapeArrayList.Count; i++)
			{
				returnShapeArrayList.Add(((BaseShape)shapeArrayList[i]).copySelf());
			}
			return returnShapeArrayList;
		}

		private void picCurrentColor_Paint(object sender, PaintEventArgs e)
		{
			txtLineWidth_TextChanged(null, null);
		}

		private void btnLine_Click(object sender, EventArgs e)
		{
			BaseTool setTool = (BaseTool)new LineTool();
			if(setTool != null){
				this.setCurrentTool(setTool);
				setTool.setRefDRAWPanel(this);
				picCurrentTool.Image = btnLine.Image;
			}
		}

		private void btnEllipse_Click(object sender, EventArgs e)
		{
			BaseTool setTool = (BaseTool)new EllipseTool();
			if(setTool != null)
			{
				this.setCurrentTool(setTool);
				setTool.setRefDRAWPanel(this);
				picCurrentTool.Image = btnEllipse.Image;
			}
		}

		private void btnCircle_Click(object sender, EventArgs e)
		{
			BaseTool setTool = (BaseTool)new CircleTool();
			if(setTool != null)
			{
				this.setCurrentTool(setTool);
				setTool.setRefDRAWPanel(this);
				picCurrentTool.Image = btnCircle.Image;
			}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			isMouseDown = true;
			if(this.getCurrentTool() != null)
			{
				this.getCurrentTool().superMouseDown(sender, e, this);
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			isMouseDown = false;
			if (this.getCurrentTool() != null)
			{
				this.getCurrentTool().superMouseUp(sender, e);
			}
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			toolStripStatusLabel1.Text = "鼠标位置：" + e.X.ToString() + "," + e.Y.ToString();
			if (isMouseDown)
			{
				if (this.getCurrentTool() != null)
				{
					this.getCurrentTool().superMouseDrag(sender, e);
				}
			}
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			for(int i = 0; i < currentShapes.Count; i++)
			{
				string Type = ((BaseShape)currentShapes[i]).GetType().ToString();
				//debug
				toolStripStatusLabel1.Text += Type;

				switch (Type)
				{
					case "Draw.LineShape":
						g.DrawLine(new Pen(((BaseShape)currentShapes[i]).penColor, ((BaseShape)currentShapes[i]).penWidth),((BaseShape)currentShapes[i]).getP1(),((BaseShape)currentShapes[i]).getP2());
						break;
					case "Draw.CircleShape":
						int r = (int)Math.Pow(Math.Pow(((BaseShape)currentShapes[i]).getP2().X - ((BaseShape)currentShapes[i]).getP1().X, 2)+Math.Pow(((BaseShape)currentShapes[i]).getP2().Y-((BaseShape)currentShapes[i]).getP1().Y, 2), 0.5);
						g.DrawEllipse(new Pen(((BaseShape)currentShapes[i]).penColor, ((BaseShape)currentShapes[i]).penWidth), ((BaseShape)currentShapes[i]).getP1().X - r, ((BaseShape)currentShapes[i]).getP1().Y - r, 2 * r, 2 * r);
						break;
					case "Draw.EllipseShape":
						Point tempPoint = new Point();
						tempPoint.X = ((BaseShape)currentShapes[i]).getP1().X > ((BaseShape)currentShapes[i]).getP2().X ? ((BaseShape)currentShapes[i]).getP2().X : ((BaseShape)currentShapes[i]).getP1().X;
						tempPoint.Y = ((BaseShape)currentShapes[i]).getP1().Y > ((BaseShape)currentShapes[i]).getP2().Y ? ((BaseShape)currentShapes[i]).getP2().Y : ((BaseShape)currentShapes[i]).getP1().Y;
						int w = Math.Abs(((BaseShape)currentShapes[i]).getP1().X- ((BaseShape)currentShapes[i]).getP2().X);
						int h = Math.Abs(((BaseShape)currentShapes[i]).getP1().Y- ((BaseShape)currentShapes[i]).getP2().Y);
						g.DrawEllipse(new Pen(((BaseShape)currentShapes[i]).penColor, ((BaseShape)currentShapes[i]).penWidth), tempPoint.X, tempPoint.Y, w, h);
						break;
					case "Draw.RectangleShape":
						Point tempP = new Point();
						tempP.X = ((BaseShape)currentShapes[i]).getP1().X > ((BaseShape)currentShapes[i]).getP2().X ? ((BaseShape)currentShapes[i]).getP2().X : ((BaseShape)currentShapes[i]).getP1().X;
						tempP.Y = ((BaseShape)currentShapes[i]).getP1().Y > ((BaseShape)currentShapes[i]).getP2().Y ? ((BaseShape)currentShapes[i]).getP2().Y : ((BaseShape)currentShapes[i]).getP1().Y;
						int ww = Math.Abs(((BaseShape)currentShapes[i]).getP1().X - ((BaseShape)currentShapes[i]).getP2().X);
						int hh = Math.Abs(((BaseShape)currentShapes[i]).getP1().Y - ((BaseShape)currentShapes[i]).getP2().Y);
						g.DrawRectangle(new Pen(((BaseShape)currentShapes[i]).penColor,((BaseShape)currentShapes[i]).penWidth),tempP.X,tempP.Y,ww,hh);
						break;
				}
				((BaseShape)currentShapes[i]).superDraw(g);
			}
		}

		public void record()
		{
			if (undoIndex > 0)
			{
				while (undoIndex != 0)
				{
					this.getHistoryShapes().RemoveAt(this.getHistoryShapes().Count - 1);
					undoIndex--;
				}
			}
			this.getHistoryShapes().Add(this.cloneShapeArray(this.getCurrentShapes()));
		}
		private void btnClear_Click(object sender, EventArgs e)
		{
			undoIndex = 0;
			this.setHistoryShapes(new ArrayList());
			this.setCurrentShapes(new ArrayList());
			record();
			this.pictureBox1.Refresh();
		}

		private void btnUndo_Click(object sender, EventArgs e)
		{
			picCurrentTool.Image = null;
			if ((this.getHistoryShapes().Count - 1 - undoIndex) > 0)
			{
				undoIndex++;
				this.setCurrentShapes((this.cloneShapeArray((ArrayList)this.getHistoryShapes()[this.getHistoryShapes().Count - 1 - undoIndex])));
			}
			this.Refresh();
		}

		private void btnRedo_Click(object sender, EventArgs e)
		{
			picCurrentTool.Image = null;
			if (undoIndex > 0)
			{
				undoIndex--;
				this.setCurrentShapes(this.cloneShapeArray((ArrayList)this.getHistoryShapes()[this.getHistoryShapes().Count - 1 - undoIndex]));
			}
			this.Refresh();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "draw类型(*.draw)|*.draw";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Stream str = File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
				BinaryFormatter bf = new BinaryFormatter();
				ArrayList als = new ArrayList();
				bool forFlat = true;
				for (int i = 0; forFlat; i++)
				{
					try
					{
						als.Add(bf.Deserialize(str));
					}
					catch
					{
						forFlat = false;
					}
				}
				str.Close();
				this.setCurrentShapes(als);
				this.setHistoryShapes(new ArrayList());
				this.record();
				undoIndex = 0;
				this.pictureBox1.Refresh();
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "draw类型(*.draw)|*.draw";
			if(this.saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Stream str = File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
				BinaryFormatter bf = new BinaryFormatter();
				for(int i = 0; i < this.getCurrentShapes().Count; i++)
				{
					bf.Serialize(str, this.getCurrentShapes()[i]);
				}
				str.Close();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnHand_Click(object sender, EventArgs e)
		{
			BaseTool setTool = (BaseTool)new HandTool();
			if(setTool!= null)
			{
				this.setCurrentTool(setTool);
				setTool.setRefDRAWPanel(this);
				picCurrentTool.Image = btnHand.Image;
			}
		}

		private void btnRectangle_Click(object sender, EventArgs e)
		{
			BaseTool setTool = (BaseTool)new RectangleTool();
			if (setTool != null)
			{
				this.setCurrentTool(setTool);
				setTool.setRefDRAWPanel(this);
				picCurrentTool.Image = btnEllipse.Image;
			}
		}

		private void btnArc_Click(object sender, EventArgs e)
		{

		}
	}
}
