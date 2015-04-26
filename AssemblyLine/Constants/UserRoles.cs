namespace AssemblyLine.Constants
{
    public static class UserRoles
    {
        public const string Administrator = "Administrator";
        public const string ProjectDirector = "ProjectDirector";
        public const string TeamManager = "TeamManager";
        public const string Engineer = "Engineer";

        public static readonly string[] AllRoles = { Administrator, ProjectDirector, TeamManager, Engineer };
    }
}