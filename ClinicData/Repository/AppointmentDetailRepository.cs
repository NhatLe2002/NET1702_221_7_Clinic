using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{

    public class AppointmentDetailRepository : GenericRepository<AppoimentDetail>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<AppoimentDetail> _dbSet;
        protected readonly DbSet<Clinic> _ClinicDb;
        protected readonly DbSet<Service> _serviceDb;
        protected readonly DbSet<Dentist> _dentistDb;
        protected readonly DbSet<Appointment> _appointmentDb;
        protected readonly DbSet<ExaminationResult> _examinationResults;





        public AppointmentDetailRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<AppoimentDetail>();
            _ClinicDb = _context.Set<Clinic>();
            _serviceDb = _context.Set<Service>();
            _dentistDb = _context.Set<Dentist>();
            _appointmentDb = _context.Set<Appointment>();
            _examinationResults = _context.Set<ExaminationResult>();


        }
        public AppointmentDetailRepository(NET1702_PRN221_ClinicContext context) => _context = context;

        public async Task<List<Clinic>> GetAllClinicAsync()
        {
            return await _ClinicDb.ToListAsync();
        }

        public async Task<List<Service>> GetAllServiceAsync()
        {
            return await _serviceDb.ToListAsync();
        }

        public async Task<List<Dentist>> GetAllDentistAsync()
        {
            return await _dentistDb.ToListAsync();
        }

        public async Task<List<Appointment>> GetAllAppoinmentAsync()
        {
            return await _appointmentDb.ToListAsync();
        }

        public async Task<List<ExaminationResult>> GetAllExaminationAsync()
        {
            return await _examinationResults.ToListAsync();
        }

        public async Task<AppoimentDetail> GetByIdOkeAsync(int id)
        {
            return await _context.AppoimentDetails
            .Include(p => p.Clinic)
            .Include(p => p.Dentist)
            .Include(p => p.Appointment)
            .Include(p => p.ExaminationResult)
            .Include(p => p.Service)
            .FirstOrDefaultAsync(p => p.AppoimnetDetailId == id);
        }


    }
}
