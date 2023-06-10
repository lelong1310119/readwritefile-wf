using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GUI
{

    public class ProgressDataGridView : DataGridView
    {
        private BackgroundWorker AnimationThread = new BackgroundWorker();
        private BackgroundWorker DataThread = new BackgroundWorker();

        private int CurrentAngle = 0;
        private bool ShowLoadingCursor = false;

        private Bitmap GridCellsImageCopy;
        private ControlsRectangle GridRectangle = new ControlsRectangle();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out ControlsRectangle lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct ControlsRectangle
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }
        }

        public ProgressDataGridView()
        {
            AnimationThread.DoWork += AnimationThread_DoWork;
            DataThread.DoWork += DataThread_DoWork;
            DataThread.RunWorkerCompleted += DataThread_RunWorkerCompleted;
        }

        private void DataThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoadingCursor = false;
            Invalidate();
            GridCellsImageCopy = null;
        }

        private void DataThread_DoWork(object sender, DoWorkEventArgs e)
        {
            
            
        }

        private void AnimationThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (ShowLoadingCursor)
            {
                CurrentAngle += 45;

                if (CurrentAngle > 360)
                    CurrentAngle = 0;

                PaintLoadingCursor();

                Thread.Sleep(75);
            }
        }

        private void PaintLoadingCursor()
        {
            using (Graphics graphics = CreateGraphics())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                int cursorSize = 30;
                int cursorX = (Width / 2) - (cursorSize / 2);
                int cursorY = (Height / 2) - (cursorSize / 2);
                int brushWidth = 6;

                int x = RowHeadersVisible ? RowHeadersWidth : 0;
                int y = ColumnHeadersVisible ? ColumnHeadersHeight : 0;
                graphics.DrawImage(GridCellsImageCopy, x, y);

                using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, Color.FromArgb(93, 93, 93), Color.FromArgb(0, 0, 255), LinearGradientMode.Vertical))
                {
                    using (Pen pen = new Pen(brush, brushWidth))
                    {
                        pen.DashStyle = DashStyle.Dot;
                        graphics.DrawArc(pen, cursorX, cursorY, cursorSize, cursorSize, 0, 360);
                    }
                }

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    using (Pen pen = new Pen(brush, brushWidth))
                    {
                        pen.DashStyle = DashStyle.Dot;
                        graphics.DrawArc(pen, cursorX, cursorY, cursorSize, cursorSize, CurrentAngle, 90);
                    }
                }
            }
        }

        private void GetGridBodyAndSaveToImage()
        {
            GetWindowRect(Handle, out GridRectangle);

            int rowHeadsWidth = RowHeadersVisible ? RowHeadersWidth : 0;
            int columnHeadsHeight = ColumnHeadersVisible ? ColumnHeadersHeight : 0;
            int width = Width - rowHeadsWidth - 1;
            int height = Height - columnHeadsHeight - 1;
            GridCellsImageCopy = new Bitmap(width, height);

            using (Graphics bitmapGraphics = Graphics.FromImage(GridCellsImageCopy))
            {
                bitmapGraphics.CopyFromScreen(GridRectangle.Left + rowHeadsWidth, GridRectangle.Top + columnHeadsHeight,
                    0, 0, new Size(GridRectangle.Width, GridRectangle.Height), CopyPixelOperation.SourceCopy);
            }

            if (Rows.Count > 0)
                GridCellsImageCopy = (Bitmap)ToolStripRenderer.CreateDisabledImage(GridCellsImageCopy);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ShowLoadingCursor) return;
            base.OnPaint(e);
        }

        public void LoadData()
        {
            if (AnimationThread.IsBusy || DataThread.IsBusy) return;

            ShowLoadingCursor = true;

            GetGridBodyAndSaveToImage();

            AnimationThread.RunWorkerAsync();

            DataThread.RunWorkerAsync();
        }
    }
}