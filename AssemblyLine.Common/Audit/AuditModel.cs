using System;

namespace AssemblyLine.Common.Audit
{
    public class AuditModel
    {
        public DateTime DateTime { get; set; }

        public string User { get; set; }

        public string ObjectType { get; set; }

        public string ObjectId { get; set; }

        public string OriginalValue { get; set; }

        public string UpdatedValue { get; set; }
    }
}