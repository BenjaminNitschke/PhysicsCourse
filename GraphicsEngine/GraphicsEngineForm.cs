using System.Drawing;
using System.Windows.Forms;
using GraphicsEngine.Datatypes;

namespace GraphicsEngine
{
	public partial class GraphicsEngineForm : Form
	{
		public GraphicsEngineForm()
		{
			InitializeComponent();
			BackColor = Color.Black;
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			MouseClickedHappenend = true;
			MouseClickPosition = new Vector2D(e.X, e.Y);
			base.OnMouseClick(e);
		}

		public bool MouseClickedHappenend { get; set; }
		public Vector2D MouseClickPosition { get; private set; }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
			if (e.KeyCode == Keys.Up)
				UpPressed = true;
			if (e.KeyCode == Keys.Down)
				DownPressed = true;
			base.OnKeyDown(e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
				UpPressed = false;
			if (e.KeyCode == Keys.Down)
				DownPressed = false;
			base.OnKeyUp(e);
		}

		public bool UpPressed { get; private set; }
		public bool DownPressed { get; private set; }
	}
}