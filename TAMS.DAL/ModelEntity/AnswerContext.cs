using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class AnswerContext : BaseContext
    {
        public static List<Answer> GetByTest(int @IdTest)
        {
            using(var context= MasterDBContext())
            {
                return context.StoredProcedure("Answer_GetByTest")
                    .Parameter("IdTest", @IdTest)
                    .QueryMany<Answer>();
            }
        }
    }
}
