using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public class PipesField {
        const int PIPE = 1;
        public const int BASE = 0;
        private byte[,] PipeArray = null;
        private int Width = 0;
        private int Height = 0;
        public PipesField(int width, int height) {
            Width = width;
            Height = height;
            PipeArray = new byte[Width, Height];
        }

        public void Reset() {
            PipeArray = new byte[Width, Height];
        }
        public void SetPipeCell(int x, int y) {
            PipeArray[x, y] = PIPE;
        }

        public void ClearPipeCell(int x, int y) {
            PipeArray[x, y] = 0;
        }

        public bool IsPipe(int x, int y) {
            return PipeArray[x, y] == PIPE;
        }

        public bool IsPipeAbove(WaterSource ws) {
            return IsPipeAbove(ws.X, ws.Y);
        }
        public bool IsPipeAbove(int x, int y) {
            return PipeArray[x, y - 1] == PIPE;
        }

        public bool IsPipeDown(WaterSource ws) {
            return IsPipeDown(ws.X, ws.Y);
        }

        public bool IsPipeDown(int x, int y) {
            return PipeArray[x, y + 1] == PIPE;
        }

        public bool IsPipeLeft(WaterSource ws) {
            return IsPipeLeft(ws.X, ws.Y);
        }
        public bool IsPipeLeft(int x, int y) {
            return PipeArray[x - 1, y] == PIPE;
        }

        public bool IsPipeRight(WaterSource ws) {
            return IsPipeRight(ws.X, ws.Y);
        }
        public bool IsPipeRight(int x, int y) {
            return PipeArray[x + 1, y] == PIPE;
        }

        public bool IsBase(int x, int y) {
            return PipeArray[x, y] == BASE;
        }

        public bool IsLeftClosed(WaterSource source) {
            return IsBase(source.X - 1, source.Y);
        }

        public bool IsRightClosed(WaterSource source) {
            return IsBase(source.X + 1, source.Y);
        }
    }
}
