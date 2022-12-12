using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SQLite;

namespace Assess3T2
{
    /// <summary>
    /// Interaction logic forregForm.xaml
    /// </summary>
    public partial class regForm : Window
    {
        public regForm()
        {
            InitializeComponent();
        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            //Submit Button
            SubmitFormInfo();
        }

        private void Button_Calculate(object sender, RoutedEventArgs e)
        {
            //Calculate Button
            UpdateCounters();
        }

        private void Button_Reset(object sender, RoutedEventArgs e) //Reset All Button
        {
            resetForm();
        }

       

        private void SubmitFormInfo()
        {
            //Initialize error var for recall
            var ErrorLevel = 0;

            //Varifies the First Name Field - returns error message and higlights if blank
            if (eFirstName.Text.Length == 0)
            {
                ErrorLevel = 1;
                eFirstName.Background = Brushes.Red;
            }
            //Varifies the Last Name Field - returns error message and higlights if blank
            if (eLastName.Text.Length == 0)
            {
                ErrorLevel = 1;
                eLastName.Background = Brushes.Red;
            }
            //Varifies the Address Field - returns error message and higlights if blank
            if (eAddress.Text.Length == 0)
            {
                ErrorLevel = 1;
                eAddress.Background = Brushes.Red;
            }
            //Varifies the Mobile Number Field - returns error message and higlights if blank
            if (eMobileNumber.Text.Length == 0)
            {
                ErrorLevel = 1;
                eMobileNumber.Background = Brushes.Red;
            }

            if (ErrorLevel == 1)
            {
                MessageBox.Show("Please fill in the missing fields in the form and click submit");
            }
            else
            {   //Collect Data and insert into variables
                var xFirstName = eFirstName.Text;
                var xLastName = eLastName.Text;
                var xAddress = eAddress.Text;
                var xMobileNumber = eMobileNumber.Text;
                var xMembership = "";
                
                //Determines Output to txt or SQLlite DB - For Future Assessment

                var nOutputMethod = 1; // 0 - Output to txt file | 1 - Output to SQLlite database file

                if (rtype_basic.IsChecked == true)
                {
                    xMembership = "Basic";
                }
                if (rtype_regular.IsChecked == true)
                {
                    xMembership = "Regular";
                }
                if (rtype_premium.IsChecked == true)
                {
                    xMembership = "Premium";
                }

                if (nOutputMethod == 0) { //Form Info To Output To txt File
                    string[] q =
                    {
                        "- Registration Form -",
                        "Name: " + xFirstName + " " + xLastName,
                        "Address: " + xAddress,
                        "Mobile Number: " + xMobileNumber,
                        "Membership Type: " + xMembership,
                        "Extra (24/7 Access): " + cextra_access.IsChecked,
                        "Extra (Diet Consultation): " + cextra_dietConsult.IsChecked,
                        "Extra (Personal Trainer): " + cextra_personalTrainer.IsChecked,
                        "Extra (Access to videos): " + conline_videos.IsChecked,
                        "Would like to use Direct Debit: " + cdirect_debit.IsChecked,
                        "Payment (Weekly): " + rtype_frequency_weekly.IsChecked,
                        "Payment (Monthly): " + rtype_frequency_monthly.IsChecked,
                        " - 3 Month's membership term: " + rtype_3months.IsChecked,
                        " - 12 Month's membership term: " + rtype_12months.IsChecked,
                        " - 24 Month's membership term: " + rtype_24months.IsChecked
                    };

                    File.WriteAllLines(xFirstName + " " + xLastName + " Registration.txt", q);
                    MessageBox.Show("Thank you, your registration form has been submitted!  Click OK to exit...");
                    resetForm();
                    
                } 
                else
                {
                    if (nOutputMethod == 1)
                    {
                       
                        // Write information to SQLite DB file
                        var dbCtables = 0;

                        if (File.Exists("citygymdb.sqlite") != true)
                        {
                            //Create New File if none exists - Location *project root*.../bin/debug
                            SQLiteConnection.CreateFile("citygymdb.sqlite");
                            dbCtables = 1;
                        }

                        //Declare object variable for SQLite
                        SQLiteConnection dataConnect;

                        //Store connection info and open datasource
                        dataConnect = new SQLiteConnection("Data Source=citygymdb.sqlite;");
                        dataConnect.Open();

                        if (dbCtables == 1)
                        {
                            //Setup the database tables if the file has just been created
                            string dataLoad = "TABLE members (first_name TEXT, last_name TEXT, address TEXT, mobile TEXT, membership TEXT, duration_3month TEXT, duration_12month TEXT, duration_24month TEXT, extra_247access TEXT, extra_personaltrainer TEXT, extra_fitnessvideo TEXT, extra_dietconsult TEXT, direct_debit TEXT, pay_weekly TEXT, pay_monthly TEXT, uniqueID TEXT)";
                            SQLiteCommand inputCmd = new SQLiteCommand(dataLoad, dataConnect);
                            inputCmd.ExecuteNonQuery();
                        }

                        //Create a Unique ID for the member being registered
                        var uID = genID(xFirstName + xLastName);


                        //Insert form data into database
                        string tbleDBload = $"INSERT INTO members (first_name, last_name, address, mobile, membership, duration_3month, duration_12month, duration_24month, extra_247access, extra_personaltrainer, extra_fitnessvideo, extra_dietconsult, direct_debit, pay_weekly, pay_monthly, uniqueID) values ('{xFirstName}', '{xLastName}', '{xAddress}', '{xMobileNumber}', '{xMembership}', '{Convert.ToInt32(rtype_3months.IsChecked)}', '{Convert.ToInt32(rtype_12months.IsChecked)}', '{Convert.ToInt32(rtype_24months.IsChecked)}', '{Convert.ToInt32(cextra_access.IsChecked)}', '{Convert.ToInt32(cextra_personalTrainer.IsChecked)}', '{Convert.ToInt32(conline_videos.IsChecked)}', '{Convert.ToInt32(cextra_dietConsult.IsChecked)}', '{Convert.ToInt32(cdirect_debit.IsChecked)}', '{Convert.ToInt32(rtype_frequency_weekly.IsChecked)}', '{Convert.ToInt32(rtype_frequency_monthly.IsChecked)}', '{uID}')";
                        SQLiteCommand mcommand = new SQLiteCommand(tbleDBload, dataConnect);
                        mcommand.ExecuteNonQuery();

                        MessageBox.Show("Thank you, your registration form has been submitted!  Click OK to exit.....");
                        resetForm();
                    }
                }
            }



        }

        private void UpdateCounters()
        {
            //Declare Variables for Counters, Extras and Discounts
            const double MembershipBasic = 10;
            const double MembershipRegular = 15;
            const double MembershipPremium = 20;

            const double Extras_Access = 1;
            const double Extras_Videos = 2;
            const double Extras_Trainer = 20;
            const double Extras_DietConsult = 20;

            const double Discount12M = 2;
            const double Discount24M = 5;

            const double WeeksIn3M = 12;
            const double WeeksIn12M = 52;
            const double WeeksIn24M = 104;

            double CounterA = 0; //Base Membership Counter
            double CounterB = 0; //Weekly Membership Amount Counter
            double CounterC = 0; //Individual Discount Counter
            double CounterD = 0; //Extras Counter
            double CounterE = 0; //Weeks in Membership Counter
            double CounterF = 0; //Total Membership Counter
            double CounterG = 0; //Net Membership Counter
            double CounterH = 0; //Total Discount Counter


            if (rtype_basic.IsChecked == true) //Basic Membership Selected
            {
                CounterA += MembershipBasic; //Holds 1 Week Basic Membership Amount For Memberhsip Total
                CounterB += MembershipBasic; //Holds 1 Week Basic Membership Amount for Memberhip + Extras
            }
            if (rtype_regular.IsChecked == true) //Regular Membership Selected
            {
                CounterA += MembershipRegular; //Holds 1 Week Regular Membership Amount For Total
                CounterB += MembershipRegular; //Holds 1 Week Regular Membership Amount for Memberhip + Extras
            }
            if (rtype_premium.IsChecked == true) //Premium Membership
            {
                
                CounterA += MembershipPremium; //Holds 1 Week Premium Membership Amount For Total
                CounterB += MembershipPremium; //Holds 1 Week Premium Membership Amount for Memberhip + Extras
            }

            if (rtype_3months.IsChecked == true)//3 Month Membership Selected
            {
                CounterE += WeeksIn3M; //Contains Weeks in 3M
            }

            if (rtype_12months.IsChecked == true) //12 Month Membership DISCOUNT + Set Weeks in 12M
            {

                CounterA -= Discount12M; //Subtracts Discount From Membership Total     
                CounterC += Discount12M; //Holds Individual Discount Amount
                CounterE += WeeksIn12M; //Contains Weeks in 12M
                


            }
            if (rtype_24months.IsChecked == true) //24 Month Membership DISCOUNT + Set Weeks in 24M
            {
                CounterA -= Discount24M; //Subtracts Discount From Membership
                CounterC += Discount24M; //Holds Individual Discount Amount
                CounterE += WeeksIn24M; //Contains Weeks in 24M
                

            }
            if (cdirect_debit.IsChecked == true) //Direct Debit 1% (of Base Membership) DISCOUNT 
            {
                CounterA -= 0.01 * CounterB; //Subtracts Base Discount From Total Membership
                CounterC += CounterE * (0.01 * CounterB); //Holds Total 1% Discount
                
                
            }

            if (cextra_access.IsChecked == true) //Access EXTRA
            {
                
                CounterD += Extras_Access; //Holds Extra Amount
                CounterA += Extras_Access; //Adds Extra To Base Membership
                
            }
            if (conline_videos.IsChecked == true) //Videos EXTRA
            {
                
                CounterD += Extras_Videos; //Holds Extra Amount
                CounterA += Extras_Videos; //Adds Extra To Base Membership

            }
            if (cextra_dietConsult.IsChecked == true) //Diet EXTRA
            {
                
                CounterD += Extras_DietConsult; //Holds Extra Amount
                CounterA += Extras_DietConsult; //Adds Extra To Base Membership

            }
            if (cextra_personalTrainer.IsChecked == true) //PT EXTRA
            {
               
                CounterD += Extras_Trainer; //Holds Extra Amount
                CounterA += Extras_Trainer; //Adds Extra To Membership

            }

            CounterG = CounterA * CounterE; //Calculate Net Membership
            CounterF = (CounterB + CounterD) * CounterE; //Calculate Total Membership
            CounterH = CounterF - CounterG; //Calculate Total Discount
           
           

            if (rtype_frequency_weekly.IsChecked == true)
            {
                
            }

            if (rtype_frequency_monthly.IsChecked == true)
            {
                CounterA *= 4;
                
            }

            //Counters to auto update if data is correct
            einfo_regularPayment.Text = "$" + CounterA;
            einfo_extraCharges.Text = "$" + CounterD;
            einfo_totalDiscount.Text = "$" + Math.Round (CounterH, 2);
            einfo_membershipTotal.Text = "$" + CounterF;
            einfo_netCost.Text = "$" + CounterG;
        }

        void resetForm()
        {
            //Reset customers details fields
            eFirstName.Text = String.Empty;
            eLastName.Text = String.Empty;
            eAddress.Text = String.Empty;
            eMobileNumber.Text = String.Empty;

            //Reset membership to basic - default
            rtype_basic.IsChecked = true;
            rtype_regular.IsChecked = false;
            rtype_premium.IsChecked = false;

            //Reset membership duration to 3 months - default
            rtype_3months.IsChecked = true;
            rtype_12months.IsChecked = false;
            rtype_24months.IsChecked = false;

            //Reset direct debit to false and frequency to weekly - default
            cdirect_debit.IsChecked = false;
            rtype_frequency_weekly.IsChecked = true;
            rtype_frequency_monthly.IsChecked = false;

            //Reset optional extra addons back to false - default
            cextra_access.IsChecked = false;
            conline_videos.IsChecked = false;
            cextra_dietConsult.IsChecked = false;
            cextra_personalTrainer.IsChecked = false;

            //Update Counter Values
            UpdateCounters();
        }

        //Increments New Unique ID For Members
        public string genID(string sName)
        {
            return sName = System.Guid.NewGuid().ToString();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

    }
}
