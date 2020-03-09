using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
    public interface IAnswerRepository:IRepository<Answer>
    {

         IEnumerable<Answer> GetTop(int count);
    }
}
