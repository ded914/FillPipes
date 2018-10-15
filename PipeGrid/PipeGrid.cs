using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipeGridNamespace {
    public class PipeGrid : INotifyPropertyChanged {

        const int VALVE = 2;

        private int Width = 0;
        private int Height = 0;
        private PipesField PipesField = null;
        public WaterField WaterField = null;
        private Graphics GridGraphics;
        private PictureBox GridPictureBox;

        public Bitmap Buffer { get; set; }
        public PipeGrid(int width, int height, PictureBox pictureBox) {
            Width = width;
            Height = height;
            PipesField = new PipesField(Width, Height);
            WaterField = new WaterField(Width, Height, this);
            Buffer = new Bitmap(Width * _CellSize, Height * _CellSize);
            GridGraphics = Graphics.FromImage(Buffer);
            GridPictureBox = pictureBox;
            ValveBrush = new SolidBrush(ValveColor);
            WaterBrush = new SolidBrush(WaterColor);
            BaseBrush = new SolidBrush(BaseColor);
            BasePen = new Pen(BaseBrush);
            StartBrush = new SolidBrush(StartColor);
            GridPen = new Pen(new SolidBrush(GridColor));
            PipePen = new Pen(new SolidBrush(PipeColor));

            BlueColors[0] = Color.LightBlue;
            for (int i = 1; i < 100; i++) {
                BlueColors[i] = ControlPaint.Dark(BlueColors[i - 1]);
            }
        }

        private Color[] BlueColors = new Color[100];

        public Point StartPoint { get; set; }
        public Point StartPointInPixels { get; set; }

        public int DeepestLevel {
            get {
                return CalcDeepestLevel();
            }
        }

        private bool LevelExists(int y) {
            for (int x = 0; x < Width; x++) {
                if (PipesField.IsPipe(x, y)) {
                    return true;
                }
            }
            return false;
        }

        private int CalcDeepestLevel() {
            int deepestLevel = 0;
            bool levelExists = false;
            int y = 0;
            while (!LevelExists(y)) { y++; };

            for (; y < Height; y++) {
                levelExists = false;
                for (int x = 0; x < Width; x++) {
                    if (PipesField.IsPipe(x, y)) {
                        levelExists = true;
                        deepestLevel = y;
                        break;
                    }
                }
                if (!levelExists) {
                    break;
                }
            }
            _DeepestLevelInPixels = y * _CellSize - 1;
            return y;
        }

        int _DeepestLevelInPixels = 0;
        public int DeepestLevelInPixels {
            get {
                CalcDeepestLevel();
                return _DeepestLevelInPixels;
            }
        }

        public void SetStartPixel(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            StartPoint = new Point(x, y);
            StartPointInPixels = new Point(x * _CellSize + 1, y * _CellSize + 1);
        }

        public void SetWaterCell(Point coordinates, float volume) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            WaterField.SetWater(x, y);
        }

        public bool IsWater(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            return WaterField.IsWater(x, y);
        }

        public bool IsWater(int x, int y) {
            return WaterField.IsWater(x, y);
        }
        public void ClearWaterCell(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            WaterField.ClearWaterCell(x, y);
        }

        public void SetPipeCell(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            PipesField.SetPipeCell(x, y);
        }

        public void SetPipeCell(int x, int y) {
            PipesField.SetPipeCell(x, y);
        }

        public bool IsPipe(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            return PipesField.IsPipe(x, y);
        }

        public bool IsPipe(int x, int y) {
            return PipesField.IsPipe(x, y);
        }


        public void ClearPipeCell(Point coordinates) {
            int x = coordinates.X / _CellSize;
            int y = coordinates.Y / _CellSize;
            PipesField.ClearPipeCell(x, y);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private int _CellSize = 32;

        public int CellSize {
            get {
                return _CellSize;
            }
            set {
                _CellSize = value;
                OnPropertyChanged("CellSize");
            }
        }

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private Pen GridPen;

        private Color _GridColor = Color.LightGray;
        public Color GridColor {
            get {
                return _GridColor;
            }
            set {
                GridColor = value;
                GridPen = new Pen(new SolidBrush(GridColor));
                OnPropertyChanged("GridColor");
            }
        }

        private Pen PipePen;

        private Color _PipeColor = Color.Black;
        public Color PipeColor {
            get {
                return _PipeColor;
            }
            set {
                PipeColor = value;
                PipePen = new Pen(new SolidBrush(PipeColor));
                OnPropertyChanged("PipeColor");
            }
        }

        private SolidBrush StartBrush;

        private Color _StartColor = Color.LightGreen;
        public Color StartColor {
            get {
                return _StartColor;
            }
            set {
                StartColor = value;
                StartBrush = new SolidBrush(StartColor);
                OnPropertyChanged("StartColor");
            }
        }

        private SolidBrush BaseBrush;
        private Pen BasePen;

        private Color _BaseColor = Color.White;
        public Color BaseColor {
            get {
                return _BaseColor;
            }
            set {
                BaseColor = value;
                BaseBrush = new SolidBrush(BaseColor);
                BasePen = new Pen(BaseBrush);
                OnPropertyChanged("BaseColor");
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
                OnPropertyChanged("WaterColor");
            }
        }

        private SolidBrush ValveBrush;

        private Color _ValveColor = Color.Black;
        public Color ValveColor {
            get {
                return _ValveColor;
            }
            set {
                ValveColor = value;
                ValveBrush = new SolidBrush(ValveColor);
                OnPropertyChanged("ValveColor");
            }
        }

        static FontFamily fontFamily = new FontFamily("Arial");
        Font fontVP = new Font(
           fontFamily,
           16,
           FontStyle.Regular,
           GraphicsUnit.Pixel);

        private SolidBrush VPBrush = new SolidBrush(Color.White);

        private void DrawWater(int x, int y, int volume) {
            //SolidBrush sb = new SolidBrush(BlueColors[volume]);
            GridGraphics.FillRectangle(WaterBrush, x * _CellSize + 3, y * _CellSize + 3, _CellSize - 6, _CellSize - 6);
            GridGraphics.DrawString(volume.ToString(), fontVP, VPBrush, x * _CellSize + 1, y * _CellSize + 1);
        }

        SolidBrush FromWaterBrush = new SolidBrush(Color.LightBlue);
        SolidBrush ToWaterBrush = new SolidBrush(Color.Brown);

        private void DrawFrom(int x, int y, int volume) {
            //SolidBrush sb = new SolidBrush(BlueColors[volume]);
            GridGraphics.FillRectangle(FromWaterBrush, x * _CellSize + 3, y * _CellSize + 3, _CellSize - 6, _CellSize - 6);
            GridGraphics.DrawString(volume.ToString(), fontVP, VPBrush, x * _CellSize + 1, y * _CellSize + 1);
        }

        private void DrawTo(int x, int y, int volume) {
            //SolidBrush sb = new SolidBrush(BlueColors[volume]);
            GridGraphics.FillRectangle(ToWaterBrush, x * _CellSize + 3, y * _CellSize + 3, _CellSize - 6, _CellSize - 6);
            GridGraphics.DrawString(volume.ToString(), fontVP, VPBrush, x * _CellSize + 1, y * _CellSize + 1);
        }

        private void DrawPipe(int x, int y) {
            bool drawUpLine = true;
            //look if there is a pipe above
            if (y - 1 > 0) {
                if (PipesField.IsPipeAbove(x, y)) {
                    drawUpLine = false;
                }
            } else {
                //terminate hall
                drawUpLine = false;
            }
            bool drawBottomLine = true;
            //look if there is a pipe above
            if (y + 1 < Height) {
                if (PipesField.IsPipeDown(x, y)) {
                    drawBottomLine = false;
                }
            } else {
                //terminate hall
                drawBottomLine = false;
            }
            bool drawLeftLine = true;
            //look if there is a pipe above
            if (x - 1 > 0) {
                if (PipesField.IsPipeLeft(x, y)) {
                    drawLeftLine = false;
                }
            } else {
                //terminate hall
                drawLeftLine = false;
            }
            bool drawRightLine = true;
            //look if there is a pipe above
            if (x + 1 < Width) {
                if (PipesField.IsPipeRight(x, y)) {
                    drawRightLine = false;
                }
            } else {
                //terminate hall
                drawRightLine = false;
            }
            if (drawUpLine) {
                GridGraphics.DrawLine(PipePen, x * _CellSize, y * _CellSize, (x + 1) * _CellSize, y * _CellSize);
            } else {
                GridGraphics.DrawLine(BasePen, x * _CellSize, y * _CellSize, (x + 1) * _CellSize, y * _CellSize);
            }
            if (drawBottomLine) {
                GridGraphics.DrawLine(PipePen, x * _CellSize, (y + 1) * _CellSize, (x + 1) * _CellSize, (y + 1) * _CellSize);
            } else {
                GridGraphics.DrawLine(BasePen, x * _CellSize, (y + 1) * _CellSize, (x + 1) * _CellSize, (y + 1) * _CellSize);
            }
            if (drawLeftLine) {
                GridGraphics.DrawLine(PipePen, x * _CellSize, y * _CellSize, x * _CellSize, (y + 1) * _CellSize);
            } else {
                GridGraphics.DrawLine(BasePen, x * _CellSize, y * _CellSize + 1, x * _CellSize, (y + 1) * _CellSize - 1);
            }
            if (drawRightLine) {
                GridGraphics.DrawLine(PipePen, (x + 1) * _CellSize, y * _CellSize, (x + 1) * _CellSize, (y + 1) * _CellSize);
            } else {
                GridGraphics.DrawLine(BasePen, (x + 1) * _CellSize, y * _CellSize + 1, (x + 1) * _CellSize, (y + 1) * _CellSize - 1);
            }
        }

        public void DrawGrid() {
            DrawGrid(null, null);
        }

        public void DrawGrid(WaterCell wcFrom, WaterCell wcTo) {
            if (GridPictureBox.InvokeRequired) {
                {
                    GridPictureBox.Invoke(new MethodInvoker(
                    delegate () {
                        _DrawGrid(wcFrom, wcTo);
                    }));
                    return;
                }
            } else {
                _DrawGrid(wcFrom, wcTo);
            }
        }

        public void _DrawGrid(WaterCell wcFrom, WaterCell wcTo) {
            //Clear
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    GridGraphics.FillRectangle(BaseBrush, x * _CellSize, y * _CellSize, _CellSize, _CellSize);
                }
            }
            //Grid
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (PipesField.IsBase(x, y)) {
                        GridGraphics.DrawRectangle(GridPen, x * _CellSize, y * _CellSize, _CellSize, _CellSize);
                    }
                }
            }
            //Pipes
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (PipesField.IsPipe(x, y)) {
                        DrawPipe(x, y);
                    }
                }
            }
            //Water
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    WaterCell wc = WaterField.GetCell(x, y);
                    if (wcFrom != null && wc == wcFrom) {
                        DrawFrom(wc.X, wc.Y, wc.Volume);
                    }
                    if (wc != null) {
                        if (!wc.IsEmpty) {
                            //DrawWater(x, y, wc.Volume, wc.Level.Pressure);
                            DrawWater(x, y, wc.Volume);
                        }
                    }
                    if (wcTo != null && wc == wcTo) {
                        DrawTo(wc.X, wc.Y, wc.Volume);
                    }
                }
            }

            for (int x = 0; x < Width; x++) {
                GridGraphics.DrawString(x.ToString(), fontVP, ValveBrush, x * _CellSize + 2, 2);
            }

            for (int y = 1; y < Width; y++) {
                GridGraphics.DrawString(y.ToString(), fontVP, ValveBrush, 2, y * _CellSize - 2);
            }

            //GridGraphics.FillRectangle(StartBrush, StartPoint.X * _CellSize + 1, StartPoint.Y * _CellSize + 1, _CellSize - 2, _CellSize - 2);
            //GridGraphics.FillRectangle(WaterBrush, StartPointInPixels.X, StartPointInPixels.Y, 1, 1);

            GridPictureBox.Image = Buffer;
            GridPictureBox.Refresh();
        }

        private LevelCollection Levels = new LevelCollection();

        public void Reset() {
            PipesField.Reset();
            WaterField.Reset();
            GridPictureBox.Image = null;
            DrawGrid();
        }

        public void ResetWater() {
            WaterField.Reset();
            DrawGrid();
        }

        public void SaveMap() {
            SaveFileDialog saveMatFileDialog = new SaveFileDialog();
            saveMatFileDialog.Filter = "Pipe Map|*.ppm";
            saveMatFileDialog.Title = "Save a map File";
            saveMatFileDialog.InitialDirectory = @"C:\ded";

            if (saveMatFileDialog.ShowDialog() == DialogResult.OK) {
                StreamWriter writer = new StreamWriter(saveMatFileDialog.OpenFile());

                writer.WriteLine(string.Format("{0}, {1}", StartPoint.X, StartPoint.Y));

                for (int x = 0; x < Width; x++) {
                    for (int y = 0; y < Height; y++) {
                        if (IsPipe(x, y)) {
                            writer.WriteLine(string.Format("{0}, {1}", x, y));
                        }
                    }
                }

                writer.Dispose();
                writer.Close();
            }
        }

        public void LoadMap(string pathToMap) {
            Reset();
            ResetWater();
            StreamReader file = new System.IO.StreamReader(pathToMap);
            string line = file.ReadLine();
            char[] cd = new char[] { ',' };
            string[] sarr = line.Split(cd);
            StartPoint = new Point(int.Parse(sarr[0]), int.Parse(sarr[1]));

            while ((line = file.ReadLine()) != null) {
                sarr = line.Split(cd);
                SetPipeCell(int.Parse(sarr[0]), int.Parse(sarr[1]));
            }

            file.Close();
            _DrawGrid(null, null);
        }

        public void LoadMap() {
            OpenFileDialog openMapFileDialog = new OpenFileDialog();
            openMapFileDialog.Title = "Open map file";
            openMapFileDialog.Filter = "Pipe Map|*.ppm";
            openMapFileDialog.InitialDirectory = @"C:\ded";
            if (openMapFileDialog.ShowDialog() == DialogResult.OK) {
                LoadMap(openMapFileDialog.FileName);
            }
        }

        public void RefreshWaterCellsGraph() {
            WaterCell wc = null;
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (!IsPipe(x, y)) {
                        continue;
                    }

                    wc = WaterField.GetCell(x, y);
                    wc.SetNeighbors();
                }
            }
        }

        public Level FindAndCreateLevel(WaterCell wc) {
            int x = wc.X;
            int y = wc.Y;
            Level level = new Level(Levels.GetNextId(), this, wc);
            level.LevelY = y;
            Levels.Add(level); //TODO do we really need this?

            WaterCell initCell = WaterField.GetCell(x, y);
            level.AddCell(initCell);
            WaterCell cellRight = initCell.Right;
            while (cellRight != null) {
                level.AddCell(cellRight);
                cellRight = cellRight.Right;
            }
            WaterCell cellLeft = initCell.Left;
            while (cellLeft != null) {
                level.AddCell(cellLeft);
                cellLeft = cellLeft.Left;
            }
            level.SortAndFindUps();
            return level;
        }


        public event EventHandler<SimulateEventArgs> SimulateStepHandler;

        protected virtual void OnSimulateStep(SimulateEventArgs e) {
            SimulateStepHandler?.Invoke(this, e);
        }

        public void Simulate(int xStart, int yStart, out bool success) {
            success = false;

            //TODO: move out of method
            WaterCell wcStart = WaterField.GetCell(xStart, yStart);
            wcStart.AddWater();

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {

                    if (!IsPipe(x, y)) continue;
                    if (!IsWater(x, y)) continue;

                    WaterCell wc = WaterField.GetCell(x, y);

                    //Thread.Sleep(50);
                    if (wc.Down != null && wc.Down.Volume == 0) {
                        wc.RemoveWater();
                        wc.Down.AddWater();
                        DrawGrid(wc, wc.Down);
                    } 
                    else if (wc.Down != null && wc.Down.Volume == 1 && !wc.Down.LevelIsFull) {
                        wc.RemoveWater();
                        var wcDownNextAside = wc.Down.NextAside;
                        wcDownNextAside.AddWater();
                        DrawGrid(wc, wcDownNextAside);
                        OnSimulateStep(new SimulateEventArgs("wc.Down != null && wc.Down.Volume == 1 && !wc.Down.LevelIsFull"));
                    } else if (wc.Down != null && wc.Down.Volume == 1 && wc.LevelIsFull && !wc.MustBeSkipped) {
                        var emptyConnectedNeighbor = wc.EmptyConnectedNeighbor;
                        if (emptyConnectedNeighbor != null) {
                            wc.RemoveWater();
                            emptyConnectedNeighbor.AddWater();
                            emptyConnectedNeighbor.MustBeSkipped = true;
                            DrawGrid(wc, emptyConnectedNeighbor);
                            OnSimulateStep(new SimulateEventArgs("emptyConnectedNeighbor"));
                        }
                    }
                }
            }
        }

        public Color[,] GetColorArray() {
            Color[,] colorArray = new Color[Buffer.Width, Buffer.Height];

            for (int x = 0; x < Buffer.Width; x++) {
                for (int y = 0; y < Buffer.Height; y++) {
                    colorArray[x, y] = Buffer.GetPixel(x, y);
                }
            }

            return colorArray;
        }
    }
}
