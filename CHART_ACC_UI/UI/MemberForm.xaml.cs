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
using CHART_ACC_ENTITY;
using CHART_ACC_BLL;
using CHART_ACC_UI.Validation;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for MemberForm.xaml
    /// </summary>
    public partial class MemberForm : Window
    {
        BMemberSetup objBMemberSetup = new BMemberSetup();
        string user_Name = "AMIN";
        byte[] MemberPhoto = new byte[0];
        byte[] DigitalSignature = new byte[0];
        byte[] NomineePhoto = new byte[0];

        public MemberForm()
        {
            InitializeComponent();
            MaritalStatusCombo();
            dtpDate.SelectedDate = DateTime.Now;
            dtpMemberBirthDate.SelectedDate = DateTime.Now;
            dtpNomineeBirthDate.SelectedDate = DateTime.Now;
        }

        #region Window
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
        #endregion

        private void MaritalStatusCombo()
        {
            string[] MaritalStatus = {"Married","Un-Married" };
            cmbMaritalStatus.Items.Add(MaritalStatus[0]);
            cmbMaritalStatus.Items.Add(MaritalStatus[1]);
        }

        private void btnMemberPhoto_Click(object sender, RoutedEventArgs e)
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
                    MemberPhoto = new byte[fs.Length].ToArray();
                    if (MemberPhoto.Length < 800 * 1024)
                    {
                        fs.Read(MemberPhoto, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        byte[] barrImg = (byte[])MemberPhoto;
                        string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                        fs1.Write(barrImg, 0, barrImg.Length);
                        fs1.Flush();
                        fs1.Close();
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        imgMemberPhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));

                    }
                    else
                    {
                        MessageBox.Show("Filesize of image is too large. Maximum file size permitted is 800KB", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
                        MemberPhoto = new byte[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MemberPhoto = new byte[0];
                MessageBox.Show("Error while uploading Image File.\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDigitalSignature_Click(object sender, RoutedEventArgs e)
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
                    DigitalSignature = new byte[fs.Length].ToArray();
                    if (DigitalSignature.Length < 800 * 1024)
                    {
                        fs.Read(DigitalSignature, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        byte[] barrImg = (byte[])DigitalSignature;
                        string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                        fs1.Write(barrImg, 0, barrImg.Length);
                        fs1.Flush();
                        fs1.Close();
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        imgDigitalSignature.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));

                    }
                    else
                    {
                        MessageBox.Show("Filesize of image is too large. Maximum file size permitted is 800KB", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
                        DigitalSignature = new byte[0];
                    }
                }
            }
            catch (Exception ex)
            {
                DigitalSignature = new byte[0];
                MessageBox.Show("Error while uploading Image File.\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnNomineePhoto_Click(object sender, RoutedEventArgs e)
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
                    NomineePhoto = new byte[fs.Length].ToArray();
                    if (NomineePhoto.Length < 800 * 1024)
                    {
                        fs.Read(NomineePhoto, 0, Convert.ToInt32(fs.Length));
                        fs.Close();
                        byte[] barrImg = (byte[])NomineePhoto;
                        string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                        fs1.Write(barrImg, 0, barrImg.Length);
                        fs1.Flush();
                        fs1.Close();
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        imgNomineePhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));

                    }
                    else
                    {
                        MessageBox.Show("Filesize of image is too large. Maximum file size permitted is 800KB", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
                        NomineePhoto = new byte[0];
                    }
                }
            }
            catch (Exception ex)
            {
                NomineePhoto = new byte[0];
                MessageBox.Show("Error while uploading Image File.\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveMemberInfo()
        {
            try
            {
                EMemberSetup objEMemberSetup = new EMemberSetup();

                objEMemberSetup.Member_No = txtMemberNo.Text;
                objEMemberSetup.No_Of_Share = txtNoShare.Text;
                objEMemberSetup.Member_Full_Name = txtFullname.Text;
                objEMemberSetup.Father_Name = txtFatherName.Text;
                objEMemberSetup.Mother_Name = txtMotherName.Text;
                objEMemberSetup.Husband_Name = txtHusbandName.Text;
                objEMemberSetup.Present_Address = txtPresentAddress.Text;
                objEMemberSetup.Permanent_Address = txtPermanentAddress.Text;
                if (txtMobileNo.Text != string.Empty)
                {
                    objEMemberSetup.Mobile_No = Convert.ToInt32(txtMobileNo.Text);
                }
                else
                {
                    objEMemberSetup.Mobile_No = 0;
                }
                objEMemberSetup.Education_Status = txtEducationStatus.Text;
                objEMemberSetup.Member_Birth_Date = Convert.ToDateTime(dtpMemberBirthDate.SelectedDate);
                objEMemberSetup.Religion = txtReligion.Text;
                objEMemberSetup.Member_Occupation = txtOccupationMember.Text;
                objEMemberSetup.Marital_Status = cmbMaritalStatus.Text;
                objEMemberSetup.Nationality = txtNationality.Text;
                objEMemberSetup.Nominee_Name = txtNomineeName.Text;
                objEMemberSetup.Nominee_Birth_Date = Convert.ToDateTime(dtpNomineeBirthDate.SelectedDate);
                objEMemberSetup.Nominee_Occupation = txtOccupationNominee.Text;
                objEMemberSetup.Relation_With_Nominee = txtRelation.Text;
                objEMemberSetup.Member_Photo = MemberPhoto;
                objEMemberSetup.Digital_Signature = DigitalSignature;
                objEMemberSetup.Nominee_Photo = NomineePhoto;
                objEMemberSetup.Access_By = user_Name;

                if (objBMemberSetup.DoesExist(txtMemberNo.Text) == true)
                {
                    MessageBox.Show("This membership no already exists.\nPlease try another no.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtMemberNo.Focus();
                }
                else
                {
                    if (objBMemberSetup.SaveMemberInfo(objEMemberSetup) == true)
                    {
                        MessageBox.Show("Member information saved successfully.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Member information saving failed.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : SaveMemberInfo().\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField() == true)
            {
                if (btnSave.Content.ToString() == "Save")
                {
                    SaveMemberInfo();
                }
                else
                {
                    UpdateMemberInfo();
                }
            }
        }

        private void UpdateMemberInfo()
        {
            try
            {
                EMemberSetup objEMemberSetup = new EMemberSetup();

                objEMemberSetup.Id = id;
                objEMemberSetup.Member_No = txtMemberNo.Text;
                objEMemberSetup.No_Of_Share = txtNoShare.Text;
                objEMemberSetup.Member_Full_Name = txtFullname.Text;
                objEMemberSetup.Father_Name = txtFatherName.Text;
                objEMemberSetup.Mother_Name = txtMotherName.Text;
                objEMemberSetup.Husband_Name = txtHusbandName.Text;
                objEMemberSetup.Present_Address = txtPresentAddress.Text;
                objEMemberSetup.Permanent_Address = txtPermanentAddress.Text;
                if (txtMobileNo.Text != string.Empty)
                {
                    objEMemberSetup.Mobile_No = Convert.ToInt32(txtMobileNo.Text);
                }
                else
                {
                    objEMemberSetup.Mobile_No = 0;
                }
                objEMemberSetup.Education_Status = txtEducationStatus.Text;
                objEMemberSetup.Member_Birth_Date = Convert.ToDateTime(dtpMemberBirthDate.SelectedDate);
                objEMemberSetup.Religion = txtReligion.Text;
                objEMemberSetup.Member_Occupation = txtOccupationMember.Text;
                objEMemberSetup.Marital_Status = cmbMaritalStatus.Text;
                objEMemberSetup.Nationality = txtNationality.Text;
                objEMemberSetup.Nominee_Name = txtNomineeName.Text;
                objEMemberSetup.Nominee_Birth_Date = Convert.ToDateTime(dtpNomineeBirthDate.SelectedDate);
                objEMemberSetup.Nominee_Occupation = txtOccupationNominee.Text;
                objEMemberSetup.Relation_With_Nominee = txtRelation.Text;
                objEMemberSetup.Member_Photo = MemberPhoto;
                objEMemberSetup.Digital_Signature = DigitalSignature;
                objEMemberSetup.Nominee_Photo = NomineePhoto;

                if (DoesExistMemberNOUpdateMode(objEMemberSetup.Member_No, id) == false)
                {
                    if (objBMemberSetup.UpdateMemberInfo(objEMemberSetup) == true)
                    {
                        MessageBox.Show("Member information updated successfully.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Member information updating failed.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : UpdateMemberInfo().\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool DoesExistMemberNOUpdateMode(string memberNo, long id)
        {
            foreach (var objEData in objBMemberSetup.GetInfoAllMember())
            {
                if (objEData.Id != id && objEData.Member_No == memberNo)
                {
                    MessageBox.Show("This Member no already exists.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtMemberNo.Focus();
                    return true;
                }
            }
            return false;
        }

        #region CHECK & RESET FIELD

        private bool CheckField()
        {
            if (txtMemberNo.Text == string.Empty)
            {
                MessageBox.Show("Please enter membership no.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMemberNo.Focus();
                return false;
            }
            if (txtFullname.Text == string.Empty)
            {
                MessageBox.Show("Please write members name.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFullname.Focus();
                return false;
            }
            if (txtFatherName.Text == string.Empty)
            {
                MessageBox.Show("Please write members father name.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFatherName.Focus();
                return false;
            }
            //if (txtMotherName.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members mother name.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtMotherName.Focus();
            //    return false;
            //}
            //if (txtPresentAddress.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members present address.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtPresentAddress.Focus();
            //    return false;
            //}
            //if (txtPermanentAddress.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members permanent address.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtPermanentAddress.Focus();
            //    return false;
            //}
            //if (!Validations.IsInteger(txtMobileNo.Text))
            //{
            //    MessageBox.Show("Please write numbers only in mobile no.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtMobileNo.Focus();
            //    return false;
            //}
            //if (txtEducationStatus.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members educational activity.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtEducationStatus.Focus();
            //    return false;
            //}
            //if (dtpMemberBirthDate.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members date of birth.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    dtpMemberBirthDate.Focus();
            //    return false;
            //}
            //if (txtReligion.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members religion.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtReligion.Focus();
            //    return false;
            //}
            //if (txtOccupationMember.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members occupation.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtOccupationMember.Focus();
            //    return false;
            //}
            //if (cmbMaritalStatus.Text == string.Empty)
            //{
            //    MessageBox.Show("Please select members marital status.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    cmbMaritalStatus.Focus();
            //    return false;
            //}
            //if (txtNationality.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members nationality.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtNationality.Focus();
            //    return false;
            //}
            //if (txtNomineeName.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write nominees name.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtNomineeName.Focus();
            //    return false;
            //}
            //if (txtOccupationNominee.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write nominees occupation.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtOccupationNominee.Focus();
            //    return false;
            //}
            //if (txtRelation.Text == string.Empty)
            //{
            //    MessageBox.Show("Please write members relation with nominee.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtRelation.Focus();
            //    return false;
            //}
            //if (MemberPhoto.Length <=0)
            //{
            //    MessageBox.Show("Please provide members photo.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtRelation.Focus();
            //    return false;
            //}
            //if (NomineePhoto.Length <= 0)
            //{
            //    MessageBox.Show("Please provide nominees photo.", "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtRelation.Focus();
            //    return false;
            //}
            return true;
        }

        private void ResetField()
        {
            txtMemberNo.Text = "";
            txtNoShare.Text = "";
            txtFullname.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtHusbandName.Text = "";
            txtPresentAddress.Text = "";
            txtPermanentAddress.Text = "";
            txtMobileNo.Text = "";
            txtEducationStatus.Text = "";
            dtpMemberBirthDate.SelectedDate = null;
            txtReligion.Text = "";
            txtOccupationMember.Text = "";
            cmbMaritalStatus.Text = "";
            txtNationality.Text = "";
            txtNomineeName.Text = "";
            dtpNomineeBirthDate.SelectedDate = null;
            txtOccupationNominee.Text = "";
            txtRelation.Text = "";
            MemberPhoto = new byte[0];
            DigitalSignature = new byte[0];
            NomineePhoto = new byte[0];
            imgMemberPhoto.SetValue(Image.SourceProperty, null);
            imgDigitalSignature.SetValue(Image.SourceProperty, null);
            imgNomineePhoto.SetValue(Image.SourceProperty, null);

            btnSave.Content = "Save";
            txtMemberNoUpdate.Text = "";
        }

        #endregion

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

        long id = 0;
        private void txtMemberNoUpdate_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtMemberNoUpdate.Text != string.Empty)
                    {
                        List<EMemberSetup> MemberList = objBMemberSetup.GetInfoMemberNoWise(txtMemberNoUpdate.Text.Trim());

                        foreach (EMemberSetup objMember in MemberList)
                        {
                            id = objMember.Id;
                            dtpDate.SelectedDate = objMember.Access_Date;
                            txtMemberNo.Text = objMember.Member_No;
                            txtNoShare.Text = objMember.No_Of_Share;
                            txtFullname.Text = objMember.Member_Full_Name;
                            txtFatherName.Text = objMember.Father_Name;
                            txtMotherName.Text = objMember.Mother_Name;
                            txtHusbandName.Text = objMember.Husband_Name;
                            txtPresentAddress.Text = objMember.Present_Address;
                            txtPermanentAddress.Text = objMember.Permanent_Address;
                            txtMobileNo.Text = objMember.Mobile_No.ToString();
                            txtEducationStatus.Text = objMember.Education_Status;
                            dtpMemberBirthDate.SelectedDate = objMember.Member_Birth_Date;
                            txtReligion.Text = objMember.Religion;
                            txtOccupationMember.Text = objMember.Member_Occupation;
                            cmbMaritalStatus.Text = objMember.Marital_Status;
                            txtNationality.Text = objMember.Nationality;
                            txtNomineeName.Text = objMember.Nominee_Name;
                            dtpNomineeBirthDate.SelectedDate = objMember.Nominee_Birth_Date;
                            txtOccupationNominee.Text = objMember.Nominee_Occupation;
                            txtRelation.Text = objMember.Relation_With_Nominee;

                            MemberPhoto = (byte[])objMember.Member_Photo;
                            if (MemberPhoto.Length > 0)
                            {
                                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                                fs1.Write(MemberPhoto, 0, MemberPhoto.Length);
                                fs1.Flush();
                                fs1.Close();
                                ImageSourceConverter imgs = new ImageSourceConverter();
                                imgMemberPhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
                            }

                            DigitalSignature = (byte[])objMember.Digital_Signature;
                            if (DigitalSignature.Length > 0)
                            {
                                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                                fs1.Write(DigitalSignature, 0, DigitalSignature.Length);
                                fs1.Flush();
                                fs1.Close();
                                ImageSourceConverter imgs = new ImageSourceConverter();
                                imgDigitalSignature.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
                            }

                            NomineePhoto = (byte[])objMember.Nominee_Photo;
                            if (NomineePhoto.Length > 0)
                            {
                                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                                FileStream fs1 = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                                fs1.Write(NomineePhoto, 0, NomineePhoto.Length);
                                fs1.Flush();
                                fs1.Close();
                                ImageSourceConverter imgs = new ImageSourceConverter();
                                imgNomineePhoto.SetValue(Image.SourceProperty, imgs.ConvertFromString(strfn));
                            }

                            btnSave.Content = "Update";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Fill-up MemberInfo().\n" + ex.Message, "Member/Customer Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewMemberInfo objViewMember = new ViewMemberInfo();
            objViewMember.ShowDialog();
        }
    }
}
