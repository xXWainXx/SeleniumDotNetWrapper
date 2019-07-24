using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class MailRepository
{ 
    private readonly string mailServer, login, password;
    private readonly int port;
    private readonly bool ssl;
    private readonly IConfiguration config;

    public MailRepository(string mailServer, int port, bool ssl, string login, string password)
    {
        this.mailServer = mailServer;
        this.port = port;
        this.ssl = ssl;
        this.login = login;
        this.password = password;
    }

    public MailRepository()
    {
        config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        mailServer = config["mail_server_address"];
        port = int.Parse(config["mail_server_port"]);
        ssl = true;
        login = config["mail_client_address"];
        password = config["mail_client_password"];
    }

    public IEnumerable<string> GetUnreadMails()
    {
        var messages = new List<string>();

        using (var client = new ImapClient())
        {
            client.Connect(mailServer, port, ssl);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate(login, password);

            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);
            var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
            foreach (var uniqueId in results.UniqueIds)
            {
                var message = inbox.GetMessage(uniqueId);

                messages.Add(message.HtmlBody);

                //Mark message as read and mark as deleted                
                inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                inbox.AddFlags(uniqueId, MessageFlags.Deleted, true);

            }

            client.Disconnect(true);
        }

        return messages;
    }

    public IEnumerable<string> GetAllMails()
    {
        var messages = new List<string>();

        using (var client = new ImapClient())
        {
            client.Connect(mailServer, port, ssl);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate(login, password);

            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            var results = inbox.Search(SearchOptions.All, SearchQuery.NotSeen);
            foreach (var uniqueId in results.UniqueIds)
            {
                var message = inbox.GetMessage(uniqueId);

                messages.Add(message.HtmlBody);

                //Mark message as read
                //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
            }

            client.Disconnect(true);
        }

        return messages;
    }

    public IEnumerable<string> WaitAndGetUnreadEmails()
    {
        var unreadEmails = GetUnreadMails();
        int i = 0;
        while (i < int.Parse(config["time_to_wait_email"]))
        {
            if (unreadEmails != null && unreadEmails.Count() != 0)
                break;

            i++;
            Thread.Sleep(1000);
            unreadEmails = GetUnreadMails();
        }

        //I added reverse here because sometimes previounsy received messages aren't marked as read. 
        //In this case all new messages should be in the top
        return unreadEmails.Reverse();
    }
}