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
using System.Windows.Shapes;
using CHART_ACC_BLL;
using CHART_ACC_ENTITY;
using System.IO;
//Author:REFAT
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for PopUpMemberList.xaml
    /// </summary>
    public partial class PopUpMemberList : Window
    {
        BMemberSetup objBMemberSetup = new BMemberSetup();
        List<EMemberSetup> listMemberSetup = new List<EMemberSetup>();
        public PopUpMemberList()
        {
            InitializeComponent();
            PopulateMemberList();
            lvMemberInfo.Focus();
        }
        private void PopulateMemberList()
        {
            try
            {
                listMemberSetup = objBMemberSetup.GetInfoAllMember();
                lvMemberInfo.ItemsSource = listMemberSetup;
            }
            catch (Exception)
            {

            }
        }
        private void lvMemberInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvMemberInfo.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    this.DialogResult = true;
                }
            }
        }

        private void lvMemberInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvMemberInfo.SelectedIndex > -1)
            {
                this.DialogResult = true;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtMemberName.Text = string.Empty;
            txtMemberNo.Text = string.Empty;
            lvMemberInfo.ItemsSource = listMemberSetup;
            lvMemberInfo.SelectedIndex = -1;
            lvMemberAccount.ItemsSource = null;
            txtTotalBalance.Text = string.Empty;
            imgMemberPhoto.SetValue(Image.SourceProperty, null);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchMember();
        }
        private void SearchMember()
        {
            try
            {
                if (txtMemberName.Text.Trim() != string.Empty || txtMemberNo.Text.Trim() != string.Empty)
                {
                    if (txtMemberName.Text.Trim() != string.Empty)
                    {
                        List<EMemberSetup> listSearch = new List<EMemberSetup>();
                        var query = from j in listMemberSetup where (j.Member_Full_Name.ToLower()).Contains(txtMemberName.Text.Trim().ToLower()) orderby j.Member_No select j;
                        foreach (var emp in query)
                        {
                            listSearch.Add(emp);
                        }
                        lvMemberInfo.ItemsSource = listSearch;

                    }
                    else if (txtMemberNo.Text.Trim() != string.Empty)
                    {
                        List<EMemberSetup> listSearch = new List<EMemberSetup>();
                        var query = from j in listMemberSetup where (j.Member_No.ToLower()).Contains(txtMemberNo.Text.Trim().ToLower()) orderby j.Member_No select j;
                        foreach (var emp in query)
                        {
                            listSearch.Add(emp);
                        }
                        lvMemberInfo.ItemsSource = listSearch;
                    }
                }
                else if (txtMemberName.Text.Trim() == string.Empty || txtMemberNo.Text.Trim() == string.Empty)
                {
                    lvMemberInfo.ItemsSource = listMemberSetup;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown Problem Occur.\n" + ex.Message, "Search", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                lvMemberInfo.SelectedIndex = -1;
                lvMemberAccount.ItemsSource = null;
                txtTotalBalance.Text = string.Empty;
                imgMemberPhoto.SetValue(Image.SourceProperty, null);
            }
        }

        private void txtMemberNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.Focus();
                SearchMember();                
            }
        }

        private void txtMemberName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.Focus();
                SearchMember();                
            }
        }

        private void lvMemberInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvMemberInfo.SelectedIndex > -1)
                {
                    EMemberSetup objEmember = lvMemberInfo.SelectedItem as EMemberSetup;

                    MemberAllAccount(objEmember);
                    ImageSet(objEmember);
                }

            }
            catch (Exception)
            {            
            }
           
        }

        private void MemberAllAccount(EMemberSetup objEmember)
        {
            BMemberAssign objBMemberAssign = new BMemberAssign();
            lvMemberAccount.ItemsSource = objBMemberAssign.GetMemberAllAccount(objEmember.Id);
            decimal total = 0;
            for (int i = 0; i < lvMemberAccount.Items.Count; i++)
            {
                total += (lvMemberAccount.Items[i] as EMemberAssign).Balance;
            }
            txtTotalBalance.Text = total.ToString();
        }

        private void ImageSet(EMemberSetup objEmember)
        {
            byte[] imgGet = (byte[])objEmember.Member_Photo;
            if (imgGet.Length > 0)
            {
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                fs1.Write(imgGet, 0, imgGet.Length);
                fs1.Flush();
                fs1.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imgMemberPhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
            }
            else
            {
                imgMemberPhoto.SetValue(Image.SourceProperty, null);
            }
        }
    }
}
