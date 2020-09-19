using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class HiredWorkerModel
    {
        public List<HiredWorker> currentWorker { get; set; }
        public List<HiredWorker> previousWorker { get; set; }
    }
}