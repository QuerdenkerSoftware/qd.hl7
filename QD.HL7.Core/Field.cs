using System.Collections.Generic;

namespace QD.HL7.Core {
    public class Field : List<string> {
        public char[] FieldDelimiters { get; set; }
        public string Value { get; set; }
    }
}