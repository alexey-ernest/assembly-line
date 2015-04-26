namespace AssemblyLine.DAL.Entities
{
    public class ProjectAssemblyLine
    {
        public int Id { get; set; }

        public virtual Project Project { get; set; }

        public virtual Line Line { get; set; }

        public virtual AssemblyLineTeam ProductionTeam { get; set; }

        public virtual AssemblyLineTeam ProcurementTeam { get; set; }
    }
}