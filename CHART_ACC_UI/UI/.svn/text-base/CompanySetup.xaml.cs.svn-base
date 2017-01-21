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
using Microsoft.Win32;
using System.IO;
using CHART_ACC_BLL;
using CHART_ACC_ENTITY;
using CHART_ACC_UI.Validation;
//Author:REFAT
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for CompanySetup.xaml
    /// </summary>
    public partial class CompanySetup : Window
    {       
        BCompanySetup objBCompanySetup = new BCompanySetup();
        int id = 0;
        byte[] data = new byte[0];
        byte[] imgGet;
        public CompanySetup()
        {
            InitializeComponent();
            FillAllField();
        }

        private void FillAllField()
        {
            try
            {
                ECompanySetup objEcompany = objBCompanySetup.GetCompanyAllInfo();
                if (objEcompany.Id > 0)
                {
                    txtCompanyName.Text = objEcompany.CompanyName;
                    txtAddress.Text = objEcompany.Address;
                    txtPhone.Text = objEcompany.Phone;
                    txtFax.Text = objEcompany.Fax;
                    txtEmail.Text = objEcompany.Email;
                    txtWebsite.Text = objEcompany.Website;
                    txtPropName.Text = objEcompany.PropName;
                    id = objEcompany.Id;
                    imgGet = (byte[])objEcompany.Logo;
                    if (imgGet.Length > 0)
                    {
                        string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                        fs1.Write(imgGet, 0, imgGet.Length);
                        fs1.Flush();
                        fs1.Close();
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        imageCompany.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
                    }
                    btnCompanySetupSave.Content = "Update";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loadin Information of This Company", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCompanySetupSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField())
                {
                    SaveInformation();
                }
            }
            catch (Exception)
            {
                
                MessageBox.Show("Problem Occur While Storing This Information into Database", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SaveInformation()
        {
            bool stat = false;
            ECompanySetup objECompanySetup = new ECompanySetup();
            objECompanySetup.CompanyName = txtCompanyName.Text.Trim();
            objECompanySetup.Address = txtAddress.Text.Trim();
            objECompanySetup.Phone = txtPhone.Text.Trim();
            objECompanySetup.Fax = txtFax.Text.Trim();
            objECompanySetup.Email = txtEmail.Text.Trim();
            objECompanySetup.Website = txtWebsite.Text.Trim();
            if (data.Length > 0)
            {
                objECompanySetup.Logo = data;
            }
            else
            {
                objECompanySetup.Logo = imgGet;
            }
            objECompanySetup.PropName = txtPropName.Text.Trim();

            if (btnCompanySetupSave.Content.ToString() == "Save")
            {
                stat = objBCompanySetup.SaveCompanyInfo(objECompanySetup);
                if (stat)
                {
                    FillAllField();
                    MessageBox.Show("Company Setup has been Saved", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                objECompanySetup.Id = Convert.ToInt32(id);
                stat = objBCompanySetup.UpdateCompanyInfo(objECompanySetup);
                if (stat)
                {
                    MessageBox.Show("Company Setup has been Updated", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool CheckField()
        {
            if (txtCompanyName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Company Name.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCompanyName.Focus();
                return false;
            }
            if (txtAddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Company Address.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAddress.Focus();
                return false;
            }
            if (txtPhone.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Phone.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPhone.Focus();
                return false;
            }
            if (txtFax.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Fax.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFax.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Email.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtEmail.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() != string.Empty)
            {
                if (Validations.IsEmail(txtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Please Enter Valid Email.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtEmail.Focus();
                    return false;
                }
            }
            if (txtWebsite.Text.Trim() != string.Empty)
            {
                if (Validations.IsWebsite(txtWebsite.Text.Trim()) == false)
                {
                    MessageBox.Show("Please Enter Valid Website.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtWebsite.Focus();
                    return false;
                }
            }
            if (txtPropName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Proprietor Name.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPropName.Focus();
                return false;
            }
            return true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id > 0)
                {
                    FillAllField();
                }
                else
                {
                    ResetInfo();
                }
            }
            catch { }
        }

        private void ResetInfo()
        {
            txtCompanyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtPropName.Text = string.Empty;
            id = 0;
            data = new byte[0];
            imgGet = new byte[0];
            imageCompany.SetValue(Image.SourceProperty, null);
            txtImagePath.Text = "";
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg;
                FileStream fs;
                dlg = new OpenFileDialog();
                dlg.ShowDialog();

                if (dlg.FileName != "")
                {
                    fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                    txtImagePath.Text = fs.Name;
                    data = new byte[fs.Length].ToArray();
                    if (data.Length < 100 * 1024)
                    {
                        fs.Read(data, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        byte[] barrImg = (byte[])data;
                        string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                        fs1.Write(barrImg, 0, barrImg.Length);
                        fs1.Flush();
                        fs1.Close();
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        imageCompany.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));

                    }
                    else
                    {
                        MessageBox.Show("Filesize of image is too large. Maximum file size permitted is 100KB", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Error);
                        data = new byte[0];
                    }
                }
            }
            catch (Exception ex)
            {                
                data = new byte[0];
                MessageBox.Show("Error while uploading Image File.", "Company Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsPhoneFax(e.Text);
            base.OnPreviewTextInput(e);
        }

        private void txtFax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsPhoneFax(e.Text);
            base.OnPreviewTextInput(e);
        }
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
            this.Owner = null;
        }
    }
}
