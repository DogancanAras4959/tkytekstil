using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.EmailConfig;

namespace tkytekstil.ENGINE.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(AppoinmentMessage message);
    }
}
