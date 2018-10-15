using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public class SimulateEventArgs : EventArgs {
        public SimulateEventArgs(string message) {
            Message = message;
        }
        public string Message { get; set; }
    }
}
