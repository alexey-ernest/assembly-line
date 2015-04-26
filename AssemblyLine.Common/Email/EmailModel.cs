using System.Collections.Generic;

namespace AssemblyLine.Common.Email
{
    public class EmailModel
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public List<string> To { get; set; }

        public string From { get; set; }
    }
}