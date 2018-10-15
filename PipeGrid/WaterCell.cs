using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public class WaterCell : IEquatable<WaterCell> {

        private int _Volume = 0;
        public int Volume {
            get {
                return _Volume;
            }
            set {
                _Volume = value;
            }
        }
        
        public bool LastDistributedToLeft { get; set; }

        private Level _Level = null;
        public Level Level {
            get {
                if (_Level == null) {
                    _Level = PipeGrid.FindAndCreateLevel(this);
                }
                return _Level;
            }

            set {
                _Level = value;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        private WaterCell _Up = null;
        public WaterCell Up {
            get {
                return _Up;
            }
            set {
                _Up = value;
            }
        }

        private WaterCell _Down = null;
        public WaterCell Down {
            get {
                return _Down;
            }
            set {
                _Down = value;
            }
        }

        private WaterCell _Left = null;
        public WaterCell Left {
            get {
                return _Left;
            }
            set {
                _Left = value;
            }
        }

        private WaterCell _Right = null;
        public WaterCell Right {
            get {
                return _Right;
            }
            set {
                _Right = value;
            }
        }

        private PipeGrid PipeGrid;

        public WaterCell(int x, int y, PipeGrid pipeGrid) {
            X = x;
            Y = y;
            Volume = 0;
            LastDistributedToLeft = false;
            PipeGrid = pipeGrid;
        }

        public void AddWater() {
            Volume += 1;
        }

        public void RemoveWater() {
            if (Volume == 0) return;
            Volume -= 1;
        }

        public bool Equals(WaterCell other) {
            if (other == null) return false;
            return (X == other.X && Y == other.Y);
        }

        public bool IsEmpty {
            get {
                return Volume == 0;
            }
        }



        public void SetNeighbors() {
            if (PipeGrid.IsPipe(X, Y - 1)) {
                Up = PipeGrid.WaterField.GetCell(X, Y - 1);
            }

            if (PipeGrid.IsPipe(X, Y + 1)) {
                Down = PipeGrid.WaterField.GetCell(X, Y + 1);
            }

            if (PipeGrid.IsPipe(X - 1, Y)) {
                Left = PipeGrid.WaterField.GetCell(X - 1, Y);
            }

            if (PipeGrid.IsPipe(X + 1, Y)) {
                Right = PipeGrid.WaterField.GetCell(X + 1, Y);
            }
        }

        public override string ToString() {
            //return string.Format("X={0},Y={1},LevelId={2},LevelUpCells={3},V={4}", X, Y, Level.Id, Level.UpCellsCount,Volume);
            return string.Format("X={0},Y={1},V={2}", X, Y, Volume);
        }

        public WaterCell NextEmptyLeft {
            get {
                WaterCell waterCell = Left;
                while (waterCell != null) {
                    if (waterCell.IsEmpty) {
                        return waterCell;
                    }
                    waterCell = waterCell.Left;
                }
                return waterCell;
            }
        }

        public WaterCell NextEmptyConnectedNeighborLeft {
            get {
                if (this.IsEmpty) {
                    return this;
                }
                int index = AllConnectedNeighbors.IndexOf(this);
                WaterCell nextLeft = null;
                while (index >= 0) {
                    nextLeft = AllConnectedNeighbors[index];
                    if (nextLeft.IsEmpty) {
                        return nextLeft;
                    }
                    index--;
                }
                return nextLeft;
            }
        }

        public WaterCell NextEmptyConnectedNeighborRight {
            get {
                if (this.IsEmpty) {
                    return this;
                }
                int index = AllConnectedNeighbors.IndexOf(this);
                WaterCell nextRight = null;
                while (index < AllConnectedNeighbors.Count) {
                    nextRight = AllConnectedNeighbors[index];
                    if (nextRight.IsEmpty) {
                        return nextRight;
                    }
                    index++;
                }
                return nextRight;
            }
        }

        private bool LastNeighborDistributedToLeft { get; set; } = false;

        public WaterCell NextAsideConnectedNeighbor {
            get {
                var wcLeft = NextEmptyConnectedNeighborLeft;
                var wcRight = NextEmptyConnectedNeighborRight;
                if (wcLeft != null && wcRight != null) {
                    if (LastNeighborDistributedToLeft) {
                        LastNeighborDistributedToLeft = false;
                        return wcRight;
                    } else {
                        LastNeighborDistributedToLeft = true;
                        return wcLeft;
                    }
                } else if (wcLeft != null) {
                    return wcLeft;
                } else if (wcRight != null) {
                    return wcRight;
                }
                return null;
            }
        }


        public WaterCell NextEmptyRight {
            get {
                WaterCell waterCell = Right;
                while (waterCell != null) {
                    if (waterCell.IsEmpty) {
                        return waterCell;
                    }
                    waterCell = waterCell.Right;
                }
                return waterCell;
            }
        }

        public bool LevelIsFull {
            get {
                return (NextEmptyLeft == null && NextEmptyRight == null);
            }
        }

        private int _ID = -1;
        private int ID {
            get {
                if (_ID == -1) {
                    _ID = (X * 0x1f1f1f1f) ^ Y;
                }
                return _ID;
            }
        }

        public WaterCell NextAside {
            get {
                var wcLeft = NextEmptyLeft;
                var wcRight = NextEmptyRight;
                if (wcLeft != null && wcRight != null) {
                    if (LastDistributedToLeft) {
                        LastDistributedToLeft = false;
                        return wcRight;
                    } else {
                        LastDistributedToLeft = true;
                        return wcLeft;
                    }
                } else if (wcLeft != null) {
                    return wcLeft;
                } else if (wcRight != null) {
                    return wcRight;
                }
                return null;
            }
        }


        public WaterCell EmptyConnectedNeighbor {
            get {
                Queue<WaterCell> nextToVisit = new Queue<WaterCell>();
                HashSet<int> visited = new HashSet<int>();

                nextToVisit.Enqueue(Down);

                while (nextToVisit.Count > 0) {
                    WaterCell wc = nextToVisit.Dequeue();
                    if (wc.Up != null && wc.Up.IsEmpty && wc.Up.Y == Y) {
                        return wc.Up;
                    } else if (wc.Up != null && wc.Up.Y == Y && !wc.Up.IsEmpty && !wc.Up.LevelIsFull) {
                        var wcUpNextAside = wc.Up.NextAside;
                        if (wcUpNextAside != null) return wcUpNextAside;
                    }

                    if (visited.Contains(wc.ID)) {
                        continue;
                    }
                    visited.Add(wc.ID);

                    if (wc.Down != null && !wc.Down.IsEmpty) nextToVisit.Enqueue(wc.Down);
                    if (wc.Up != null && !wc.Up.IsEmpty) nextToVisit.Enqueue(wc.Up);
                    if (wc.Left != null && !wc.Left.IsEmpty) nextToVisit.Enqueue(wc.Left);
                    if (wc.Right != null && !wc.Right.IsEmpty) nextToVisit.Enqueue(wc.Right);
                }

                return null;

            }
        }

        private List<WaterCell> _AllConnectedNeighbors = null;
        public List<WaterCell> AllConnectedNeighbors {
            get {
                if (_AllConnectedNeighbors == null) {
                    _AllConnectedNeighbors = GetAllConnectedNeighbors();
                    _AllConnectedNeighbors = _AllConnectedNeighbors.OrderBy(o => o.X).ToList();
                }
                return _AllConnectedNeighbors;
            }
            set {
                _AllConnectedNeighbors = null;
            }
        }

        public bool MustBeSkipped { get; internal set; } = false;

        public List<WaterCell> GetAllConnectedNeighbors() {
            List<WaterCell> allConnectedNeighbors = new List<WaterCell>();

            if (Down == null) return allConnectedNeighbors;

            Queue<WaterCell> nextToVisit = new Queue<WaterCell>();
            HashSet<int> visited = new HashSet<int>();

            nextToVisit.Enqueue(Down);

            while (nextToVisit.Count > 0) {
                WaterCell wc = nextToVisit.Dequeue();
                if (wc.Up != null && wc.Up.Y == Y) {
                    allConnectedNeighbors.Add(wc.Up);
                }

                if (visited.Contains(wc.ID)) {
                    continue;
                }
                visited.Add(wc.ID);

                if (wc.Down != null && !wc.Down.IsEmpty) nextToVisit.Enqueue(wc.Down);
                if (wc.Up != null && !wc.Up.IsEmpty) nextToVisit.Enqueue(wc.Up);
                if (wc.Left != null && !wc.Left.IsEmpty) nextToVisit.Enqueue(wc.Left);
                if (wc.Right != null && !wc.Right.IsEmpty) nextToVisit.Enqueue(wc.Right);
            }

            return allConnectedNeighbors;
        }

    }
}
