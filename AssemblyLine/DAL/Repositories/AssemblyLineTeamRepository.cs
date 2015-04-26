﻿using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class AssemblyLineTeamRepository : IAssemblyLineTeamRepository
    {
        private readonly ApplicationDbContext _db;

        public AssemblyLineTeamRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<AssemblyLineTeam> EditAsync(AssemblyLineTeam entity)
        {
            var original = await _db.AssemblyLineTeams.FindAsync(entity.Id);
            if (original == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", entity.Id));
            }

            _db.Entry(original).CurrentValues.SetValues(entity);

            // updating navigation properties
            original.Manager = entity.Manager != null ? await _db.Employees.FindAsync(entity.Manager.Id) : null;
            original.Engineers.Clear();
            foreach (var engineer in entity.Engineers)
            {
                var e = await _db.Employees.FindAsync(engineer.Id);
                original.Engineers.Add(e);
            }

            await SaveChangesAsync();

            return original;
        }
    }
}