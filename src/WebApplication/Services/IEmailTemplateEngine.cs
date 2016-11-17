using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.EmailTemplates
{
    public interface IEmailTemplateEngine
    {
        string GenerateTemplate(string templateName, params object[] args);

    }
}
