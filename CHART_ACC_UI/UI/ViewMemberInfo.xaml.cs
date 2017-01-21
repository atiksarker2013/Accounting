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
using CHART_ACC_ENTITY;
using CHART_ACC_BLL;
using System.IO;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for ViewMemberInfo.xaml
    /// </summary>
    public partial class ViewMemberInfo : Window
    {
        List<EMemberSetup> listMemberSetup = new List<EMemberSetup>();
        public ViewMemberInfo()
        {
            InitializeComponent();
            LoadMemberListView();
        }

        #region WINDOW
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            //this.Owner = null;
        }
        #endregion

        private void LoadMemberListView()
        {
            try
            {
                listMemberSetup = new BMemberSetup().GetInfoAllMember();
                lvMemberInfo.ItemsSource = listMemberSetup;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : LoadMemberListView().\n" + ex.Message,"Member View",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        long memberId = 0;
        private void lvMemberInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvMemberInfo.SelectedIndex > -1)
                {
                    EMemberSetup objEmember = lvMemberInfo.SelectedItem as EMemberSetup;
                    ImageSet(objEmember);
                    memberId = objEmember.Id;
                }

            }
            catch (Exception)
            {
            }
        }

        private void ImageSet(EMemberSetup objEmember)
        {
            byte[] MPhoto = (byte[])objEmember.Member_Photo;
            byte[] NPhoto = (byte[])objEmember.Nominee_Photo;
            if (MPhoto.Length > 0)
            {
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                fs1.Write(MPhoto, 0, MPhoto.Length);
                fs1.Flush();
                fs1.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imgMemberPhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
            }
            else
            {
                imgMemberPhoto.SetValue(Image.SourceProperty, null);
            }
            if (NPhoto.Length > 0)
            {
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                fs1.Write(NPhoto, 0, NPhoto.Length);
                fs1.Flush();
                fs1.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imgNomineePhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
            }
            else
            {
                imgNomineePhoto.SetValue(Image.SourceProperty, null);
            }
        }

        private void SearchMember()
        {
            try
            {
                if (txtMemberNo.Text.Trim() != string.Empty)
                {
                    List<EMemberSetup> listSearch = new List<EMemberSetup>();
                    var query = from j in listMemberSetup where (j.Member_No.ToLower()).Contains(txtMemberNo.Text.Trim().ToLower()) orderby j.Member_No select j;
                    foreach (var emp in query)
                    {
                        listSearch.Add(emp);
                    }
                    lvMemberInfo.ItemsSource = listSearch;

                }
                else
                {
                    lvMemberInfo.ItemsSource = listMemberSetup;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown Problem Occur.\n" + ex.Message, "Search Member", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                lvMemberInfo.SelectedIndex = -1;
                imgMemberPhoto.SetValue(Image.SourceProperty, null);
                imgNomineePhoto.SetValue(Image.SourceProperty, null);
            }
        }

        private void txtMemberNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.Focus();
                SearchMember();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchMember();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtMemberNo.Text = string.Empty;
            lvMemberInfo.ItemsSource = listMemberSetup;
            lvMemberInfo.SelectedIndex = -1;
            imgMemberPhoto.SetValue(Image.SourceProperty, null);
            imgNomineePhoto.SetValue(Image.SourceProperty, null);
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvMemberInfo.SelectedIndex > -1)
                {
                    if (new BMemberAssign().DoesExistsInAssign(memberId) == false)
                    {
                        if (MessageBox.Show("Are you want to delete this  member?\nThis member's information will be permanently deleted.", "Member View", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (new BMemberSetup().DeleteMember(memberId) == true)
                            {
                                MessageBox.Show("Member information deleted successfully.", "Member View", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Member information deletion failed.", "Member View", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("This member already assign for transaction.\nYou can not delete this member.", "Member View", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select any member to delete.", "Member View", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unknown Problem Occur.\n" + ex.Message, "Delete Member", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                lvMemberInfo.SelectedIndex = -1;
                imgMemberPhoto.SetValue(Image.SourceProperty, null);
                imgNomineePhoto.SetValue(Image.SourceProperty, null);
            }
        }
    }
}
