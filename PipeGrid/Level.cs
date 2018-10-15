using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public class Level : IEquatable<Level> {

        public int Id { get; set; }
        private List<WaterCell> Cells = null;
        public List<WaterCell> UpCells = null;
        private PipeGrid PipeGrid = null;

        public int LevelY {
            get; set;
        }

        public int PressureNeedToUp { get; set; }

        private int _Pressure = 0;
        public int Pressure {
            get {
                _Pressure = 0;
                Cells.ForEach(c => _Pressure += c.Volume);
                _Pressure = _Pressure - Cells.Count;
                if (_Pressure < 0) return 0;
                return _Pressure;
            }
        }

        public bool OverPressurized {
            get {
                return Pressure >= PressureNeedToUp;
            }
        }
        public Level(int id, PipeGrid pipeGrid, WaterCell initCell) {
            Id = id;
            Cells = new List<WaterCell>();
            UpCells = new List<WaterCell>();
            PipeGrid = pipeGrid;
            UpCellsFilled = false;
            InitCell = initCell;
        }

        public void AddCell(WaterCell cell) {
            Cells.Add(cell);
            cell.Level = this;
        }

        private WaterCell InitCell { get; set; }

        public void SortAndFindUps() {
            Cells.Sort((c1, c2) => c1.X.CompareTo(c2.X));
            PressureNeedToUp = 0;
            foreach (WaterCell c in Cells) {
                if (c != InitCell) {
                    if (c.Up != null) {
                        UpCells.Add(c.Up);
                        PressureNeedToUp++;
                    }
                }
            }
        }

        public override string ToString() {
            return string.Format("ID={0}, Cells={1}, UpCells={2}", Id, Cells.Count, UpCells.Count);
        }

        public void AddUpCell(WaterCell cell) {
            UpCells.Add(cell);
        }
        public bool UpCellsFilled { get; set; } = false;

        private void RemoveOverWaterFromLevel() {
            foreach (WaterCell c in Cells) {
                if (c.Volume > 1) c.RemoveWater();
                return;
            }
        }

        public void FillUpCells() {
            foreach (var c in UpCells) {
                if (c.Volume > 0) continue;
                c.AddWater();
                RemoveOverWaterFromLevel();
            }
            UpCellsFilled = true;
        }

        public bool CellBelongsTo(WaterCell wc) {
            return Cells.Contains(wc);
        }

        public bool IsFull {
            get {
                return Cells.All(c => c.Volume > 0);
            }
        }

        public WaterCell RightMost {
            get {
                return Cells[Cells.Count - 1];
            }
        }

        public WaterCell LeftMost {
            get {
                return Cells[0];
            }
        }

        public object UpCellsCount {
            get {
                return UpCells.Count;
            }
        }

        public WaterCell NextEmptyRight(WaterCell cell) {
            int ci = Cells.IndexOf(cell);
            for (int i = 1; i + ci < Cells.Count; i++) {
                if (Cells[ci + i].Volume == 0) return Cells[ci + i];
            }
            return null;
        }

        public WaterCell NextEmptyLeft(WaterCell cell) {
            int ci = Cells.IndexOf(cell);
            for (int i = 1; ci - i >= 0; i++) {
                if (Cells[ci - i].Volume == 0) return Cells[ci - i];
            }
            return null;
        }

        public WaterCell NextLowerVolumeRight(WaterCell cell) {
            int ci = Cells.IndexOf(cell);
            for (int i = 1; i + ci < Cells.Count; i++) {
                if (Cells[ci + i].Volume < cell.Volume) return Cells[ci + i];
            }
            return null;
        }

        public WaterCell NextLowerVolumeLeft(WaterCell cell) {
            int ci = Cells.IndexOf(cell);
            for (int i = 1; ci - i >= 0; i++) {
                if (Cells[ci - i].Volume < cell.Volume) return Cells[ci - i];
            }
            return null;
        }

        public bool Equals(Level other) {
            if (other == null) return false;
            foreach (WaterCell c in Cells) {
                if (!other.Cells.Contains(c)) {
                    return false;
                }
            }
            return true;
        }
    }

    public class LevelCollection : List<Level> {
        private int NextId = 0;

        public int GetNextId() {
            NextId++;
            return NextId;
        }
    }
}
