using System;

namespace CodePant.DotNet.Helper.Email
{
    public interface IEmailHelper
    {
        void SendEmail(string fromAddress, string toAddress, string subject, string body);
    }
}