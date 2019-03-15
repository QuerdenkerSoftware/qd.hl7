using System;
using System.Collections.Generic;

namespace QD.HL7.Core {
    public class HL7Message {
        public string Message { get; internal set; }
        public string Version { get; internal set; }
        public string MessageControlId { get; internal set; }
        public string ProcessingId { get; internal set; }
        public string SendingApplication { get; internal set; }
        public string SendingFacility { get; internal set; }
        public string ReceivingApplication { get; internal set; }
        public string ReceivingFacility { get; internal set; }
        public DateTime MessageDate { get; internal set; }
        public string MessageType { get; internal set; }
        public List<Segment> Segments { get; internal set; }


        internal HL7Message() {
            Segments = new List<Segment>();
        }

        public override string ToString() {
            return base.ToString();
        }
    }
}