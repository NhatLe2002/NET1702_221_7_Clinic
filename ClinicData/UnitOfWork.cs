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
        private CustomerRepository _customer;
        private UserRepository _user;
        private AppointmentRepository _appointment;
        private AppointmentDetailRepository _appointmentDetailRepository;




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

        public AppointmentRepository AppointmentRepository
        {
            get
            {
                return _appointment ??= new AppointmentRepository(_context);
            }
        }
        public AppointmentDetailRepository AppointmentDetailRepository
        {
            get
            {
                return _appointmentDetailRepository ??= new AppointmentDetailRepository(_context);
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ??= new CustomerRepository(_context);
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
