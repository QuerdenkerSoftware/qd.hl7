using System;
using System.Collections.Generic;
using System.Linq;

namespace QD.HL7.Core {
    public sealed class Parser {
        private static readonly char[] DefaultSegmentSeparatorString = {'\r', '\n'};
        private static readonly char[] FieldDelimiters = {'|', '^', '~', '&'};

        public HL7Message Parse(string hl7MessageString) {
            var hl7Message = new HL7Message();

            var segmentLines =
                hl7MessageString.Split(DefaultSegmentSeparatorString, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segmentLine in segmentLines) ParseSegment(segmentLine, hl7Message);

            return hl7Message;
        }

        private static void ParseSegment(string segmentLine, HL7Message hl7Message) {
            var segment = new Segment {Name = segmentLine.Substring(0, 3), Value = segmentLine};
            hl7Message.Segments.Add(segment);

            var fieldStrings = segmentLine.Split(FieldDelimiters[0]).ToList();

            ParseFields(segment, fieldStrings);
        }

        private static void ParseFields(Segment segment, IList<string> fieldStrings) {
            if (segment.Name.Equals("MSH"))
                fieldStrings[0] = FieldDelimiters[0].ToString();
            else
                fieldStrings.RemoveAt(0);

            foreach (var fieldEntry in fieldStrings) {
                var field = ParseField(fieldEntry);

                segment.Fields.Add(field);
            }
        }

        private static Field ParseField(string fieldEntry) {
            var field = new Field {
                FieldDelimiters = new[] {FieldDelimiters[1], FieldDelimiters[2], FieldDelimiters[3]},
                Value = fieldEntry
                
            };

            var fieldValues = fieldEntry.Split(FieldDelimiters[1]);
            field.AddRange(fieldValues);
            return field;
        }

    }
}