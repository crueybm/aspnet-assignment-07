/*
 * Name: Brandon Cruey 
 * email: crueybm@mail.uc.edu
 * Class: Web Server Application Development
 * Date: 03/21/2018
 * Assignment: 07
 * Description: In this program, the user will enter a website into a textbox, and press a button.  The program will search all
 *              of the html and text on the page, finding all email addresses, and giving the user an option to select a list of 
 *              addresses inside a checkboxlist.  Once email addresses are selected, the user can send an auto-generated email
 *              by clicking another button.
 * Citation:    https://www.aspsnippets.com/Articles/Send-email-using-Gmail-SMTP-Mail-Server-in-ASPNet.aspx
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    //Creates List EmailList
    List<String> EmailList = new List<String>();
    String txtTo;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Method "Browse()" provided by assignment description
    private String Browse()
    {
        // Can't find System.Windows.Controls.WebBrowser, which might be nice. 
        // See http://stackoverflow.com/questions/8645926/how-to-create-and-use-webbrowser-in-background-thread
        WebBrowser myWebBrowser = null;
        String innerHTML = "";
        txtTitle.Text = "???";
        txtValue.Text = "???";
        Thread thread = new Thread(delegate () {
            int elapsedTime;
            DateTime startTime = new DateTime(), endTime = new DateTime();
            startTime = DateTime.Now;
            System.Windows.Forms.HtmlElement htmlElement = null;

            //WebBrowser foo = new System.Net.HttpWebRequest(); // deprecated. Seems to work for Desktop Apps and .Net Framework 2.0

            // This causes major crashing of the web server
            //foo.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentComplete);
            myWebBrowser = new System.Windows.Forms.WebBrowser();    // Must be instantiated inside the Thread

            myWebBrowser.AllowNavigation = true;
            myWebBrowser.Navigate(txtURL.Text, false);

            while (myWebBrowser.ReadyState != WebBrowserReadyState.Complete)
            //while (foo.ReadyState != WebBrowserReadyState.Interactive)  // We don't care if the graphics are loaded
            //while (foo.IsBusy == true)
            {
                txtValue.Text = myWebBrowser.ReadyState.ToString();
                System.Windows.Forms.Application.DoEvents();
                endTime = DateTime.Now;
                elapsedTime = (endTime - startTime).Seconds;
                if (elapsedTime > 30) break;
            }
            // When we get this far we have hopefully loaded the web page into the browser object
            try
            {
                txtTitle.Text = myWebBrowser.Document.Title;
                //System.Windows.Forms.HtmlElement x = foo.Document.GetElementById("yfs_l84_csco");
                htmlElement = myWebBrowser.Document.Body;
                innerHTML = htmlElement.InnerHtml;
            }
            catch (Exception ex)
            {
                txtValue.Text += "<p> Exception: " + ex.ToString();
            }

        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();

        // We won't get here until the Thread completes.
        // This might be interesting: http://www.charith.gunasekara.web-sphere.co.uk/2010/09/how-to-convert-html-page-to-image-using.html
        return innerHTML;
    }

    //Method to search a page's raw html for email addresses
    private void searchEmail()
    {
        //Turns the return value of the "Browse" method into a string
        String RawHtml = Browse();
        //Creates a regular expression to validate an email address
        Regex EmailRegex = 
            new Regex("\\b[A-Z0-9._%-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}\\b", 
            RegexOptions.IgnoreCase);

        //Checks the raw html for a match against the RegEx
        Match EmailMatch = EmailRegex.Match(RawHtml);

        //while-loop that runs if the RegEx has a match
        while (EmailMatch.Success)
        {
            //Creates a string of the matched RegEx value
            String match = EmailMatch.ToString();
            //Adds String match to lbxEmail
            lbxEmail.Items.Add(EmailMatch.ToString());
            //Adds String match to cblEmails
            cblEmails.Items.Add(EmailMatch.ToString());
            //Sets EmailMatch to the next match in the raw html
            EmailMatch = EmailMatch.NextMatch();
        }
    }

    //Event handler for when btnUrl is clicked
    protected void btnUrl_Click(object sender, EventArgs e)
    {
        //Calls on the "searchEmail" method
        searchEmail();
    }

    //Event handler for when a selected index is changed in cblEmails
    protected void cblEmails_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Foreach loop for all items in cblEmails
        foreach (ListItem addr in cblEmails.Items)
        {
            //If an item is selected
            if (addr.Selected)
            {
                //Add item to EmailList
                EmailList.Add(addr.Value);
            } else {
                //else break
                break;
            }
        }
    }

    //Event handler for btnSubmit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Joins the EmailList into a string
        txtTo = String.Join(", ", EmailList);
        //Calls upon SendEmail method
        SendEmail();
    }

    //SendEmail method from source given in assignment description
    protected void SendEmail()
    {
        using (MailMessage mm = new MailMessage(txtEmail.Text, txtTo))
        {
            mm.Subject = txtSubject.Text;
            mm.Body = txtBody.Text;
            /*if (fuAttachment.HasFile)
            {
                string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
            }*/
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail.Text, txtPassword.Text);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
        }
    }
}