using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QD.HL7.Core {
    public class TerserExpression {
        private static readonly Regex RepetitionRegex = new Regex(@"^.*\((\d)\)");
        private readonly string m_value;

        public TerserExpression(string value) {
            m_value = value;
        }

        public bool IsValid => !string.IsNullOrWhiteSpace(m_value) && m_value.Length >= 5 && m_value.Contains("-");


        public static implicit operator string(TerserExpression expression) {
            return expression.m_value;
        }

        public static implicit operator TerserExpression(string expression) {
            return new TerserExpression(expression);
        }

        public string GetSegmentName() {
            return !IsValid ? string.Empty : m_value.Substring(0, 3);
        }

        public bool IsRepetition() {
            return RepetitionRegex.IsMatch(m_value);
        }

        public int GetRepetition() {
            if (!IsValid) return 0;

            return int.Parse(RepetitionRegex.Match(m_value).Groups[1].Value);
        }

        public IList<int> GetIndices() {
            if (!IsValid) return new List<int>();

            return IsRepetition()
                ? m_value.Substring(7, m_value.Length - 7).Split('-').Select(int.Parse).ToList()
                : m_value.Substring(4, m_value.Length - 4).Split('-').Select(int.Parse).ToList();
        }
    }
}