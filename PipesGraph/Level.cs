using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesGraph {
    public struct LevelPixel {
        public LevelPixel(int absX, bool isSet) {
            AbsX = absX;
            IsSet = isSet;
        }
        public int AbsX;
        public bool IsSet;
    }

    public struct LevelPosition {
        public LevelPosition(int arrPosition, int posInArr) {
            ArrPosition = arrPosition;
            PositionItArray = posInArr;
        }
        public int ArrPosition;
        public int PositionItArray;
    }
    public class Level : List<LevelPixel[]> {
        public int AbsY;
        public Level(int xDrop, int xLeft, int xRight) {
            int size = xRight - xLeft;
            LevelPixel[] dropArr = new LevelPixel[size];
            for (int i = 0; i < size; i++) {
                dropArr[i] = new LevelPixel(xLeft + i, false);
            }
            NextPosition = new LevelPosition(0, xRight - xDrop);
        }

        private bool LastDropOnRight = false;

        private LevelPosition NextPosition;

        public bool SetNextDrop() {
            return false;
        }

        
    }
}
