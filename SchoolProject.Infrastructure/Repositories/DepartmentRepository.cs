﻿using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDBContext _context;
        public DepartmentRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
