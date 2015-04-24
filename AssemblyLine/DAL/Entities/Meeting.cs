using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class Meeting
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual ICollection<MeetingMember> Members { get; set; }
    }
}