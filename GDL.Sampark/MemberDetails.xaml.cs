using GDL.Sampark.DataAccess;
using GDL.Sampark.Models;
using GDL.Sampark.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GDL.Sampark
{
    /// <summary>
    /// Interaction logic for MemberDetails.xaml
    /// </summary>
    public partial class MemberDetails : Window
    {
        string HouseNumber;
        private List<Members> list = new List<Members>();

        public MemberDetails(string houseNo)
        {
            HouseNumber = houseNo;
            InitializeComponent();
            LoadRowData();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public async void LoadRowData()
        {
            MembersService members = new MembersService();
            list = await members.GetMembersDetials(HouseNumber);
            rowDataGrid.ItemsSource = list;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            this.Visibility = Visibility.Hidden;
           
        }
    }
}
