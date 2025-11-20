using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Entities;
using quiz.hub.Infrastructure.Data;
using quiz.hub.Infrastructure.Repositories.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Infrastructure.Repositories
{
    public class CandidateAnswerRepo : BaseRepository<CandidateAnswer> , ICandidateAnswerRepo
    {
        private readonly AppDbContext _context;
        public CandidateAnswerRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
