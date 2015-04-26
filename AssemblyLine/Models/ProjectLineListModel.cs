using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectLineListModel
    {
        public int Id { get; set; }

        public virtual Line Line { get; set; }
    }
}