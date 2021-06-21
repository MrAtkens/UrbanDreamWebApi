using System;
using System.Threading.Tasks;
using BazarJok.DataAccess.Providers;

namespace BazarJok.Services.Business
{
    public class ProblemPinModeration
    {
        private readonly ProblemPinProvider _problemPinProvider;

        public ProblemPinModeration(ProblemPinProvider problemPinProvider)
        {
            _problemPinProvider = problemPinProvider;
        }
       
    }
}