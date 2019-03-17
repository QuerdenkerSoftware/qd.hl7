using System;
using System.Linq;

namespace QD.HL7.Core {
    public class Terser {
        private readonly HL7Message m_hl7Message;

        public Terser(HL7Message hl7Message) {
            m_hl7Message = hl7Message;
        }

        private void Set(string value, string segmentName, int repetition, params int[] indices) {
            var segment = GetOrAddSegment(segmentName, repetition, true);

            if (indices.Length == 1) {
                segment.Value = value;
                if (indices[0] - 1 <= segment.Fields.Count) segment.Fields[indices[0] - 1].Value = value;
            }
            else {
                var field = segment.Fields[indices[0] - 1];
                var subFieldIndex = indices[1] - 1;

                for (var i = 0; i <= subFieldIndex; i++) {
                    var subField = field[i];
                    if (subField == null) field.Add(string.Empty);

                    if (i == subFieldIndex) field.Value = value;
                }
            }
        }

        public void Set(string value, TerserExpression terserExpression) {
            Set(value, terserExpression.GetSegmentName(),
                terserExpression.IsRepetition() ? terserExpression.GetRepetition() : 1,
                terserExpression.GetIndices().ToArray());
        }


        public string Get(TerserExpression terserExpression) {
            return Get<string>(terserExpression.GetSegmentName(),
                terserExpression.IsRepetition() ? terserExpression.GetRepetition() : 1,
                terserExpression.GetIndices().ToArray());
        }

        private T Get<T>(string segmentName, int repetition, params int[] indices) {
            var segment = GetOrAddSegment(segmentName, repetition, false);
            if (segment == null) {
                return default(T);
            }
            var field = segment.Fields[indices[0] - 1];
            if (field == null) return default(T);

            var value = field.Value;

            if (indices.Length != 2) return (T) Convert.ChangeType(value, typeof(T));

            var index = indices[1] - 1;
            if (field.Count <= index) return default(T);
            value = field[index];

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public T Get<T>(TerserExpression terserExpression) {
            return Get<T>(terserExpression.GetSegmentName(),
                terserExpression.IsRepetition() ? terserExpression.GetRepetition() : 1,
                terserExpression.GetIndices().ToArray());
        }

        private bool SegmentExists(string segmentName, int repetition) {
            return m_hl7Message?.Segments != null && m_hl7Message.Segments.Any() &&
                   m_hl7Message.Segments.Exists(
                       segment => segment.Name.ToLowerInvariant().Equals(segmentName.ToLower()) && segment.Repetition == repetition);
        }

        private void AddSegment(string segmentName, int repetition) {
            m_hl7Message.Segments.Add(new Segment {Name = segmentName, Repetition = repetition == -1 ? 1 : repetition});
        }

        private Segment GetOrAddSegment(string segmentName, int repetition, bool addIfnotExists) {
            if (!SegmentExists(segmentName, repetition)) {
                if (addIfnotExists) {
                    AddSegment(segmentName, repetition);
                }
                else {
                    return null;
                }
                
            }


            return m_hl7Message.Segments.First(segment =>
                segment.Name.ToLowerInvariant().Equals(segmentName.ToLower()) && segment.Repetition == repetition);
        }
    }
}