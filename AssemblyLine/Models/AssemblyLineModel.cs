using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class AssemblyLineModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LineStatus Status { get; set; }
    }
}