using PipeGridNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterDistributionPixels;

namespace FillPipes {
    public partial class PipePlayForm : Form {
        private PipeGrid PipeGrid;
        private PixelField PixelField;
        public PipePlayForm() {
            InitializeComponent();
            PipeGrid = new PipeGridNamespace.PipeGrid(50, 30, pictureBoxPipeGrid);
            PipeGrid.DrawGrid();
        }

        private void pictureBoxPipeGrid_Click(object sender, EventArgs e) {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            if (me.Button == MouseButtons.Left) {
                if (PipeGrid.IsPipe(coordinates)) {
                    PipeGrid.ClearPipeCell(coordinates);
                } else {
                    PipeGrid.SetPipeCell(coordinates);
                }
            } else if (me.Button == MouseButtons.Right) {
                if (PipeGrid.IsWater(coordinates)) {
                    PipeGrid.ClearWaterCell(coordinates);
                } else {
                    PipeGrid.SetWaterCell(coordinates, WaterField.FULL_WATER);
                }
            }
            PipeGrid.DrawGrid();
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            PipeGrid.Reset();
        }

        private bool SimulateOn = false;

        private void pictureBoxPipeGrid_MouseMove(object sender, MouseEventArgs e) {
            if (checkBoxSimulateOn.Checked) return;
            Point coordinates = e.Location;
            if (e.Button == MouseButtons.Left) {
                PipeGrid.SetPipeCell(coordinates);
            } else if (e.Button == MouseButtons.Right) {
                PipeGrid.SetWaterCell(coordinates, WaterField.FULL_WATER);
            }
            PipeGrid.DrawGrid();
        }

        private void buttonTransferToPixels_Click(object sender, EventArgs e) {
            PixelField = new PixelField(PipeGrid, pictureBoxPixelField);
            PixelField.Draw();
        }

        private void pictureBoxPipeGrid_DoubleClick(object sender, EventArgs e) {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            PipeGrid.SetStartPixel(coordinates);
            PipeGrid.DrawGrid();
        }

        private void buttonResetWater_Click(object sender, EventArgs e) {
            PipeGrid.ResetWater();
        }

        private BackgroundWorker SimulateBW;
        private void buttonSimulate_Click(object sender, EventArgs e) {
            PipeGrid.RefreshWaterCellsGraph();
            SimulateBW = new BackgroundWorker();
            SimulateBW.DoWork += SimulateBW_DoWork;
            SimulateBW.RunWorkerAsync();
        }

        private void SimulateBW_DoWork(object sender, DoWorkEventArgs e) {
            //for (int i = 0; i < 10000; i++) {
            //Thread.Sleep(200);
            bool success = false;
                PipeGrid.Simulate(PipeGrid.StartPoint.X, PipeGrid.StartPoint.Y, out success);
                //PipeGrid.DrawGrid();
            //}
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            PipeGrid.SaveMap();
        }

        private void buttonLoad_Click(object sender, EventArgs e) {
            PipeGrid.LoadMap();
        }


        private void buttonSimulateD_Click(object sender, EventArgs e) {
            //PipeGrid.Simulate();
            //PipeGrid._DrawGrid();
        }

        private void PipePlayForm_Load(object sender, EventArgs e) {
            PipeGrid.LoadMap(@"C:\ded\test1.ppm");
            PipeGrid.RefreshWaterCellsGraph();
            PipeGrid.SimulateStepHandler += PipeGrid_SimulateStepHandler;
        }

        int cs = 0;

        private void PipeGrid_SimulateStepHandler(object sender, SimulateEventArgs e) {

            if (richTextBox1.InvokeRequired) {
                {
                    richTextBox1.Invoke(new MethodInvoker(
                    delegate () {
                        richTextBox1.Text = richTextBox1.Text.Insert(0, cs.ToString() + ". " + e.Message + "\n");
                        cs++;
                    }));
                    return;
                }
            } else {
                richTextBox1.Text = richTextBox1.Text.Insert(0, cs.ToString() + ". " + e.Message + "\n");
                cs++;
            }

            
        }
    }
}
