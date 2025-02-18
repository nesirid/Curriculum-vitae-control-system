using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _dbContext;

        public CandidateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllCandidatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCandidateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Candidate?> GetCandidateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Candidate>> GetCandidatesAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteCandidateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
