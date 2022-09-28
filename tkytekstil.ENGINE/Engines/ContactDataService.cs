using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.EmailConfig;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ContactDataData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class ContactDataService : CrudService<ContactData, ContactDataDto>, IContactDataService
    {
        private readonly EmailConfiguration _emailConfig;
        public ContactDataService(IRepository<ContactData> repository, IMapper mapper, IOptions<EmailConfiguration> emailConfig) : base(repository, mapper)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task sendMailContact(ContactDataDto contact)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
                emailMessage.To.Add(MailboxAddress.Parse(contact.To));
                emailMessage.Subject = contact.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<div>{0}</div>", contact.Body) };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task sendMailOrderComplete(ContactDataDto body)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
                emailMessage.To.Add(MailboxAddress.Parse(body.To));
                emailMessage.Subject = body.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<div>{0}</div>", body.Content) };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
