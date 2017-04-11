using System;

namespace NtAbcExam.Domain.Models
{
    public class Tester
    {
        public string Id { get; private set; }

        public string TesterName { get; private set; }

        public Tester(string id, string testerName)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(testerName)) throw new ArgumentNullException(nameof(testerName));
            Id = id;
            TesterName = testerName;
        }
    }
}
