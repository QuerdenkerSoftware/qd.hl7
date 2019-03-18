using System;
using System.Collections.Generic;
using System.Text;

namespace QD.HL7.Core {
    public class Segment {
        public string Name { get; set; }
        public int Repetition { get; set; }
        public string Value { get; set; }
        public List<Field> Fields { get; set; }

        public Segment() {
            Fields = new List<Field>();
        }

        public override string ToString() {
            var sb = new StringBuilder();

            sb.Append(Name);

            foreach (var field in Fields) {
                sb.Append("|" + field);
            }

            if (Name.Equals("MSH", StringComparison.InvariantCultureIgnoreCase)) {
                sb = sb.Replace("MSH||", "MSH|");
            }

            return sb.ToString();
        }
    }
}