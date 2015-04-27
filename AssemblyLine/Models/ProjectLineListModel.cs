namespace AssemblyLine.Models
{
    public class ProjectLineListModel
    {
        public int Id { get; set; }

        public AssemblyLineModel Line { get; set; }

        public ProjectLineCycleModel Cycle { get; set; }
    }
}