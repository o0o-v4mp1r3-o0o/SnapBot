using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SnapBot
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        List<string> yes = new List<string>();
        List<string> WebSites = new List<string>();
        List<string> Codes = new List<string>();
        String sixtyninelol;
        public Form1()
        {
            InitializeComponent();
        }

        private void Looper()
        {
            yes.Clear();
            WebSites.Clear();
            System.Diagnostics.Debug.WriteLine(LinksAndCodes.Lines.Length);
            for (int i = 0; i < LinksAndCodes.Lines.Length; i++)
            {
                yes.Add(LinksAndCodes.Lines[i]);
                if (LinksAndCodes.Lines[i] == "")
                {
                    yes.Remove(LinksAndCodes.Lines[i]);
                    break;
                }
            }
            System.Diagnostics.Debug.WriteLine(yes.Count);
            for (int i = 0; i < yes.Count; i++)
            {
                //WebSites[i] = yes[i];
                if (i % 2 == 0)
                {
                    WebSites.Add(yes[i]);
                    continue;
                }
                if (i % 2 != 0)
                {
                    Codes.Add(yes[i].Substring(7));
                    continue;
                }
            }
        }

        private void VisitSite()
        {
            for (int i = 0; i < WebSites.Count; i++){
                driver.Url = WebSites[i].ToString();
                driver.Navigate();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(50);
                Thread.Sleep(5000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                while (driver.FindElements(By.ClassName("grey-dropV2")).Count < 1)
                {
                    System.Diagnostics.Debug.WriteLine("plz login lol");
                    Thread.Sleep(2000);
                }
                js.ExecuteScript("document.getElementsByClassName('btn btn-primary mno-contribute-btn wl-btn-primary')[0].click();");
                while (driver.FindElements(By.Id("coupon-search")).Count < 1)
                {
                    Thread.Sleep(500);
                }
                driver.FindElement(By.Id("coupon-search")).SendKeys(Codes[i].ToString());
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByClassName('btn-primary')[4].click();");
                Thread.Sleep(6000);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var directory = Directory.GetCurrentDirectory();
            var directory2 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(directory2, "Resources");
            driver = new ChromeDriver(filePath);
            //richTextBox1.Text = filePath.ToString();
            Looper();
            VisitSite();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
