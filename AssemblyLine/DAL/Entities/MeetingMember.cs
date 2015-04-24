namespace AssemblyLine.DAL.Entities
{
    public class MeetingMember
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public MeetingMembershipStatus Status { get; set; }
    }
}