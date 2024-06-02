using ClinicData.Models;
using ClinicData.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class UnitOfWork
    {
        private NET1702_PRN221_ClinicContext _context;
        private ClinicRepository _clinic;
        private ServiceRepository _service;
        private UserRepository _user;

        public UnitOfWork()
        {
            _context = new NET1702_PRN221_ClinicContext();
        }
        public ClinicRepository ClinicRepository
        {
            get
            {
                return _clinic ??= new ClinicRepository(_context);
            }
        }
        public ServiceRepository ServiceRepository
        {
            get
            {
                return _service ??= new ServiceRepository(_context);
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                return _user ??= new UserRepository(_context);
            }
        }
    }
}
