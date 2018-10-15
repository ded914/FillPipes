using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public class WaterField {
        public const int FULL_WATER = 12;
        private int Width = 0;
        private int Height = 0;
        private WaterCell[,] WaterArray = null;
        private PipeGrid PipeGrid;

        public WaterField(int width, int height, PipeGrid pipeGrid) {
            Width = width;
            Height = height;
            Reset();
            PipeGrid = pipeGrid;
        }

        

        public void SetWater(int x, int y) {
            WaterArray[x, y].Volume = 1;
        }

        public bool IsWater(int x, int y) {
            return WaterArray[x, y].Volume > 0;
        }

        public void ClearWaterCell(int x, int y) {
            WaterArray[x, y].Volume = 0;
        }

        public void Reset() {
            WaterArray = new WaterCell[Width, Height];
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    WaterArray[x, y] = new WaterCell(x, y, PipeGrid);
                }
            }
        }

        public int Volume(int x, int y) {
            return WaterArray[x, y].Volume;
        }

        public bool LastDistibutedToLeft(int x, int y) {
            return WaterArray[x, y].LastDistributedToLeft;
        }

        public WaterCell GetCell(int x, int y) {
            if (PipeGrid.IsPipe(x, y)) {
                return WaterArray[x, y];
            } else {
                return null;
            }
        }
    }
}
