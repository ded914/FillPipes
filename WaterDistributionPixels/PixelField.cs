using PipeGridNamespace;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterDistributionPixels {
    public class PixelField {
        private int Width = 0;
        private int Height = 0;
        private Pixel[,] Pixels = null;
        private Bitmap Buffer = null;
        private Graphics GridGraphics;
        private PictureBox GridPictureBox;

        private SolidBrush BaseBrush;

        private Color _BaseColor = Color.White;
        public Color BaseColor {
            get {
                return _BaseColor;
            }
            set {
                BaseColor = value;
                BaseBrush = new SolidBrush(BaseColor);
            }
        }

        private SolidBrush PipeBrush;

        private Color _PipeColor = Color.Black;
        public Color PipeColor {
            get {
                return _PipeColor;
            }
            set {
                PipeColor = value;
                PipeBrush = new SolidBrush(PipeColor);
            }
        }

        private SolidBrush WaterBrush;

        private Color _WaterColor = Color.Blue;
        public Color WaterColor {
            get {
                return _WaterColor;
            }
            set {
                WaterColor = value;
                WaterBrush = new SolidBrush(WaterColor);
            }
        }
        public PixelField(int width, int height) {
            Width = width;
            Height = height;
            Pixels = new Pixel[Width, Height];
            Buffer = new Bitmap(Width, Height);
        }

        public bool IsWater(int x, int y) {
            return Pixels[x, y] is WaterPixel;
        }

        public bool IsWall(int x, int y) {
            return Pixels[x, y] is WallPixel;
        }

        private Point StartPointInPixels { get; set; }
        private int DeepestLevelInPixels = 0;

        public PixelField(PipeGrid pipeGrid, PictureBox pictureBox) {
            Color[,] colorArray = pipeGrid.GetColorArray();
            Width = colorArray.GetLength(0);
            Height = colorArray.GetLength(1);
            Pixels = new Pixel[Width, Height];
            Buffer = new Bitmap(Width, Height);
            BaseBrush = new SolidBrush(BaseColor);
            PipeBrush = new SolidBrush(PipeColor);
            WaterBrush = new SolidBrush(WaterColor);
            GridPictureBox = pictureBox;
            GridGraphics = Graphics.FromImage(Buffer);
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (colorArray[x, y].ToArgb().Equals(pipeGrid.PipeColor.ToArgb())) {
                        Pixels[x, y] = new WallPixel();
                    }
                }
            }
            StartPointInPixels = pipeGrid.StartPointInPixels;
            DeepestLevelInPixels = pipeGrid.DeepestLevelInPixels;

            Pixels[StartPointInPixels.X, StartPointInPixels.Y] = new WaterPixel();
        }

        Pen DeepPen = new Pen(Color.Green);

        public void Draw() {

            GridGraphics.FillRectangle(BaseBrush, 0, 0, Width, Height);

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (Pixels[x, y] is WallPixel) {
                        GridGraphics.FillRectangle(PipeBrush, x, y, 1, 1);
                    } else if (Pixels[x, y] is WaterPixel) {
                        GridGraphics.FillRectangle(WaterBrush, x, y, 1, 1);
                    }
                }
            }

            GridGraphics.DrawLine(DeepPen, 0, DeepestLevelInPixels, Width, DeepestLevelInPixels);

            GridPictureBox.Image = Buffer;
        }

        private bool IsWallAbove(int x, int y) {
            return Pixels[x, y - 1] is WallPixel;
        }

        private bool IsWallDown(int x, int y) {
            return Pixels[x, y + 1] is WallPixel;
        }

        private bool IsWallLeft(int x, int y) {
            return Pixels[x - 1, y] is WallPixel;
        }

        private bool IsWallRight(int x, int y) {
            return Pixels[x + 1, y] is WallPixel;
        }

        private bool IsWaterAbove(int x, int y) {
            return Pixels[x, y - 1] is WaterPixel;
        }

        private bool IsWaterDown(int x, int y) {
            return Pixels[x, y + 1] is WaterPixel;
        }

        private bool IsWaterLeft(int x, int y) {
            return Pixels[x - 1, y] is WaterPixel;
        }

        private bool IsWaterRight(int x, int y) {
            return Pixels[x + 1, y] is WaterPixel;
        }

        private Point GoMaxDown(int x, int y) {
            int xd = x;
            int yd = y;
            //looking down
            while (!IsWallDown(xd, yd)) {
                yd++;
            }
            //looking left
            int xl = xd;
            int yl = yd;
            while (!IsWallLeft(xl, yl)) {
                xl--;
                //if a hall down - fill the hall
                if (!IsWallDown(x, y)) {
                    return GoMaxDown(xl, yl);
                }
            }
            //looking right
            int xr = xd;
            int yr = yd;
            while (!IsWallRight(xr, yr)) {
                xr++;
                if (!IsWallDown(xr, yr)) {
                    return GoMaxDown(xr, yr);
                }
            }
            //if here then it is most down wall
            return new Point(xd, yd);
        }

        //private void Drop(int x, int y) {
        //    int wy = y;
        //    while (!(IsWallDown(x, wy) || IsWaterDown(x, wy))) {
        //        wy++;
        //    }

        //    if (wy == y) {
        //        return;
        //    }

        //    if (IsWallDown(x, y)) {

        //    } else { //IsWaterDown
        //        //looking right
        //        if (!IsWallRight(x, wy)) {
        //            x++;
        //            if (!IsWaterDown(x, wy)) {
        //                foundPoint = new Point
        //            }
        //        }
        //    }


        //}

        

        private bool FindLevel(int x, int y) {
            return false;
        }

        void FindCandidateAndFilling(int pressure) {
        }
    }
}
