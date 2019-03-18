using System.Collections.Generic;
using System.Text;

namespace QD.HL7.Core {
    public class Field : List<string> {
        public char[] FieldDelimiters { get; set; }
        public string Value { get; set; }

        public override string ToString() {
            if (Count == 0) {
                return Value;
            }

            var sb = new StringBuilder();

            foreach (var subField in this) {
                sb.AppendFormat("{0}{1}", subField, "^");
            }

            

            return sb.ToString().Remove(sb.Length-1);
        }
    }
}