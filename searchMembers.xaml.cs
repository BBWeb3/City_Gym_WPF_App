using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Assess3T2
{
    /// <summary>
    /// Interaction logic for searchMembers.xaml
    /// </summary>
    public partial class searchMembers : Window
    {
        public searchMembers() 
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("citygymdb.sqlite") == true)
            {
                //Object variable for working with SQLite
                SQLiteConnection connectToDB;

                //Store connection information and open the datasource
                connectToDB = new SQLiteConnection("Data Source=citygymdb.sqlite;");
                connectToDB.Open();

                if (NameSearchTerm.Text != "")
                {
                    //Clear search items
                    MemberListView.Items.Clear();

                    //Dropdown box for search by member type
                    var sTermReturn = MemberSearchTerm.Text;
                    string lPayload = "membership";

                    if (sTermReturn == "Basic Membership")
                    {
                        lPayload = "'Basic'";
                    }
                    if (sTermReturn == "Regular Membership")
                    {
                        lPayload = "'Regular'";
                    }
                    if (sTermReturn == "Premium Membership")
                    {
                        lPayload = "'Premium'";
                    }

                    //Search based on persons name, matches first & last name
                    string iPayload = $"SELECT * FROM members WHERE (first_name LIKE '%{NameSearchTerm.Text}%' OR last_name LIKE '%{NameSearchTerm.Text}%') AND (membership = {lPayload});";
                    SQLiteCommand icommand = new SQLiteCommand(iPayload, connectToDB);
                    var result = icommand.ExecuteReader();

                    //Process each result in the returned array
                    while (result.Read())
                    {
                        var resultFirstName = result.GetString(0);
                        var resultLastName = result.GetString(1);
                        var resultAddress = result.GetString(2);
                        var resultMobile = result.GetString(3);
                        var resultMembershipType = result.GetString(4);

                        var resultDuration3M = result.GetString(5);
                        var resultDuration12M = result.GetString(6);
                        var resultDuration24M = result.GetString(7);
                        var result247 = result.GetString(8);
                        var resultPT = result.GetString(9);
                        var resultVideo = result.GetString(10);
                        var resultConsult = result.GetString(11);
                        var resultDDebit = result.GetString(12);
                        var resultPayWkly = result.GetString(13);
                        var resultPayMthly = result.GetString(14);

                        var resultUniqueIDD = result.GetString(15);

                        var gridView = new GridView();
                        this.MemberListView.View = gridView;

                        //Header for first name
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "First Name",
                            Width = 200,
                            DisplayMemberBinding = new Binding("FirstName")
                        });

                        //Header for last name
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Last Name",
                            Width = 200,
                            DisplayMemberBinding = new Binding("LastName")
                        });

                        //Header for address
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Address",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Address")
                        });

                        //Header for phone
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Phone",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Phone")
                        });

                        //Header for 3 month duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 3 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration3M")
                        });

                        //Header for 12 month duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 12 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration12M")
                        });

                        //Header for 24 month duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 24 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration24M")
                        });

                        //Header for access 24/7
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "24/7 Access",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Access247")
                        });

                        //Header for personal trainer
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Personal Trainer",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PT")
                        });

                        //Header for fitness video
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Fitness Video",
                            Width = 0,
                            DisplayMemberBinding = new Binding("FitVideo")
                        });

                        //Header for diet consultation
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Diet Consultation",
                            Width = 0,
                            DisplayMemberBinding = new Binding("DietConsult")
                        });

                        //Header for direct debit
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Direct Debit",
                            Width = 0,
                            DisplayMemberBinding = new Binding("DDebit")
                        });

                        //Header for weekly payment
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Pay Weekly",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PayWeekly")
                        });

                        //Header for monthly payment
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Pay Monthly",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PayMonthly")
                        });

                        //Header for membership
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Membership Type",
                            Width = 370,
                            DisplayMemberBinding = new Binding("MembershipType")
                        });

                        //Header for uniqueID
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "UniqueID",
                            Width = 370,
                            DisplayMemberBinding = new Binding("UniqueID")
                        });

                        // Populate listview with data from 
                        this.MemberListView.Items.Add(new CityGymS
                        {
                            FirstName = resultFirstName,
                            LastName = resultLastName,
                            MembershipType = resultMembershipType,
                            Address = resultAddress,
                            Phone = resultMobile,
                            Duration3M = resultDuration3M,
                            Duration12M = resultDuration12M,
                            Duration24M = resultDuration24M,
                            Access247 = result247,
                            PT = resultPT,
                            FitVideo = resultVideo,
                            DietConsult = resultConsult,
                            DDebit = resultDDebit,
                            PayWeekly = resultPayWkly,
                            PayMonthly = resultPayMthly,
                            UniqueID = resultUniqueIDD
                        });
                    }
                }
                else
                {
                    //Clear search items
                    MemberListView.Items.Clear();

                    //Get membership search term and compile SQL ipayload
                    var sTermReturn = MemberSearchTerm.Text;
                    string iPayload = "SELECT * FROM members;";

                    if (sTermReturn == "All Memberships")
                    {
                        iPayload = "SELECT * FROM members;";
                    }
                    if (sTermReturn == "Basic Membership")
                    {
                        iPayload = "SELECT * FROM members WHERE membership = 'Basic';";
                    }
                    if (sTermReturn == "Regular Membership")
                    {
                        iPayload = "SELECT * FROM members WHERE membership = 'Regular';";
                    }
                    if (sTermReturn == "Premium Membership")
                    {
                        iPayload = "SELECT * FROM members WHERE membership = 'Premium';";
                    }


                    //Perform Search based on persons name. Search for first & last name
                    SQLiteCommand icommand = new SQLiteCommand(iPayload, connectToDB);
                    var result = icommand.ExecuteReader();

                    //Process eeach result in returned array
                    while (result.Read())
                    {

                        //Declare variables and get contents from sql query
                        var resultFirstName = result.GetString(0);
                        var resultLastName = result.GetString(1);
                        var resultAddress = result.GetString(2);
                        var resultMobile = result.GetString(3);
                        var resultMembership = result.GetString(4);

                        var resultDuration3M = result.GetString(5);
                        var resultDuration12M = result.GetString(6);
                        var resultDuration24M = result.GetString(7);
                        var result24Access = result.GetString(8);
                        var resultPT = result.GetString(9);
                        var resultFitnessVideo = result.GetString(10);
                        var resultDietConsult = result.GetString(11);
                        var resultDirectDebit = result.GetString(12);
                        var resultPayWeekly = result.GetString(13);
                        var resultPayMonthly = result.GetString(14);

                        var resultUniqueIDD = result.GetString(15);

                        var gridView = new GridView();
                        this.MemberListView.View = gridView;

                        //Header for first name
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "First Name",
                            Width = 200,
                            DisplayMemberBinding = new Binding("FirstName")
                        });

                        //Header for last name
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Last Name",
                            Width = 200,
                            DisplayMemberBinding = new Binding("LastName")
                        });

                        //Header for address
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Address",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Address")
                        });

                        //Header for phone
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Phone",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Phone")
                        });

                        //Header 3 months duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 3 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration3M")
                        });

                        //Header for 12 months duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 12 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration12M")
                        });

                        //Header for 24 months duration
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Duration 24 Month",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Duration24M")
                        });

                        //Header for access 24/7
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "24/7 Access",
                            Width = 0,
                            DisplayMemberBinding = new Binding("Access247")
                        });

                        //Header for personal trainer
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Personal Trainer",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PT")
                        });

                        //Header for fitness video
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Fitness Video",
                            Width = 0,
                            DisplayMemberBinding = new Binding("FitVideo")
                        });

                        //Header for diet consultation
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Diet Consultation",
                            Width = 0,
                            DisplayMemberBinding = new Binding("DietConsult")
                        });

                        //Header for direct debit
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Direct Debit",
                            Width = 0,
                            DisplayMemberBinding = new Binding("DDebit")
                        });

                        //Header for pay weekly
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Pay Weekly",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PayWeekly")
                        });

                        //Header for pay monthly
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Pay Monthly",
                            Width = 0,
                            DisplayMemberBinding = new Binding("PayMonthly")
                        });

                        //Header for membership
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "Membership",
                            Width = 370,
                            DisplayMemberBinding = new Binding("MembershipType")
                        });

                        //Header for uniqueID
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header = "UniqueID",
                            Width = 370,
                            DisplayMemberBinding = new Binding("UniqueID")
                        });

                        // Populate listview with data from database
                        this.MemberListView.Items.Add(new CityGymS
                        {
                            FirstName = resultFirstName,
                            LastName = resultLastName,
                            MembershipType = resultMembership,
                            Address = resultAddress,
                            Phone = resultMobile,
                            Duration3M = resultDuration3M,
                            Duration12M = resultDuration12M,
                            Duration24M = resultDuration24M,
                            Access247 = result24Access,
                            PT = resultPT,
                            FitVideo = resultFitnessVideo,
                            DietConsult = resultDietConsult,
                            DDebit = resultDirectDebit,
                            PayWeekly = resultPayWeekly,
                            PayMonthly = resultPayMonthly,
                            UniqueID = resultUniqueIDD
                        });
                    }
                }


            }
        }

        //CityGymS class to be called later and for casting
        public class CityGymS
        {
          
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Duration3M { get; set; }
            public string Duration12M { get; set; }
            public string Duration24M { get; set; }
            public string Access247 { get; set; }
            public string PT { get; set; }
            public string FitVideo { get; set; }
            public string DietConsult { get; set; }
            public string DDebit { get; set; }
            public string PayWeekly { get; set; }
            public string PayMonthly { get; set; }
            public string MembershipType { get; set; }
            public string UniqueID { get; set; }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            //Clear search ITEMS
            MemberListView.Items.Clear();

            //Clear search TERMS
            NameSearchTerm.Text = "";
            MemberSearchTerm.SelectedIndex = 0;

            //Clear member display output
            srchFirstName.Text = "";
            srchLastName.Text = "";
            srchPhoneNumber.Text = "";
            srchAddress.Text = "";
            srchMembership.Text = "";

            sDirectDebit.IsChecked = false;
            srchWeekly.IsChecked = false;
            srchMonthly.IsChecked = false;

            srch3Mths.IsChecked = false;
            srch12Mths.IsChecked = false;
            srch24Mths.IsChecked = false;

            srch27Access.IsChecked = false;
            srchDietConsult.IsChecked = false;
            srchOnlineVideos.IsChecked = false;
            srchPT.IsChecked = false;

        }

        private void MemberListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get index of selected item and create another int for incrementing
            int itemSelect = MemberListView.SelectedIndex;
            int itemSelectIncr = 0;

            //Create a cast to pull information from listview
            var cityGymList = MemberListView.Items.Cast<CityGymS>().ToArray<CityGymS>();

            //Loop through array of casted variable list
            foreach (var cityGymItem in cityGymList)
            {
                //If listview selected index == array index then display data
                if (itemSelectIncr == itemSelect)
                {
                    //Cast data from listview to member display fields below
                   srchFirstName.Text = cityGymItem.FirstName.ToString();
                    srchLastName.Text = cityGymItem.LastName.ToString();
                    srchPhoneNumber.Text = cityGymItem.Phone.ToString();
                    srchAddress.Text = cityGymItem.Address.ToString();
                    srchMembership.Text = cityGymItem.MembershipType.ToString();

                    sDirectDebit.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.DDebit));
                    srchWeekly.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.PayWeekly));
                    srchMonthly.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.PayMonthly));

                    srch3Mths.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.Duration3M));
                    srch12Mths.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.Duration12M));
                    srch24Mths.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.Duration24M));

                    srch27Access.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.Access247));
                    srchDietConsult.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.DietConsult));
                    srchOnlineVideos.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.FitVideo));
                    srchPT.IsChecked = Convert.ToBoolean(Convert.ToInt32(cityGymItem.PT));
                }

                itemSelectIncr++;
            }

        }

        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            

            //Load member registration form
            searchHelp SearchHelp = new searchHelp();
            SearchHelp.ShowDialog();

        }

    }


}
