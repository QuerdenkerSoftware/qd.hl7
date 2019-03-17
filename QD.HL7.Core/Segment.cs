using System.Collections.Generic;

namespace QD.HL7.Core {
    public class Segment {
        public string Name { get; set; }
        public int Repetition { get; set; }
        public string Value { get; set; }
        public List<Field> Fields { get; set; }

        public Segment() {
            Fields = new List<Field>();
        }
    }
}