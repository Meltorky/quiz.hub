using quiz.hub.Application.Interfaces.IRepositories;
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
    public class AnswerRepo : BaseRepository<Answer> , IAnswerRepo
    {
        private readonly AppDbContext _context;
        public AnswerRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
