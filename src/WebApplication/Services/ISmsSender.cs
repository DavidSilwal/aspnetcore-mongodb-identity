using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);

    }
}
