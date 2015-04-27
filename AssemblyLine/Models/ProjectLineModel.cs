namespace AssemblyLine.Models
{
    public class ProjectLineModel
    {
        public int Id { get; set; }

        public AssemblyLineModel Line { get; set; }

        public virtual AssemblyLineTeamModel ProductionTeam { get; set; }

        public virtual AssemblyLineTeamModel ProcurementTeam { get; set; }
    }
}