namespace AssemblyLine.DAL.Entities
{
    public class ProjectAssemblyLine
    {
        public int Id { get; set; }

        public virtual Project Project { get; set; }

        public virtual ProjectTeam ProductionTeam { get; set; }

        public virtual ProjectTeam ProcurementTeam { get; set; }
    }
}