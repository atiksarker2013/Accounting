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
using SPIDER_SECURITY.BLL;
using SPIDER_SECURITY.Entity;
//Author:Refat
namespace SPIDER_SECURITY.UI
{
    /// <summary>
    /// Interaction logic for SecurityControl.xaml
    /// </summary>
    public partial class SecurityControl : Window
    {

        public SecurityControl()
        {
            InitializeComponent();
        }
        /************************************************************************************************************************************************************/
        /************************************************ This Portion for SAVING,MODIFICATON,DELETION of USER GROUP************************************************/


        BUserGroup aBuserGroup = new BUserGroup();
        long UserGroupIdModify = 0;

        private void InitialTaskforUserGroup()
        {
            txtUserGroupName.Text = string.Empty;
            btnAddUserGroup.Content = "Add";
            PopulateUserGroupList();
        }

        private void PopulateUserGroupList()
        {
            try
            {
                lvUserGroup.ItemsSource = aBuserGroup.GetAllUserGroup();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Retrieving user Group Information.", "User Group", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddUserGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofUserGroup())
                {
                    if (btnAddUserGroup.Content.ToString() == "Add")
                    {
                        SaveOperationofUserGroup();
                    }
                    if (btnAddUserGroup.Content.ToString() == "Modify")
                    {
                        UpdateOperationofUserGroup();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Storing Information.", "User Group", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveOperationofUserGroup()
        {
            bool stat = false;
            if (aBuserGroup.DoesExistGroupinSaveMode(txtUserGroupName.Text.Trim()) == false)
            {
                EUserGroup anUserGroup = new EUserGroup();
                anUserGroup.GroupName = txtUserGroupName.Text.Trim();
                stat = aBuserGroup.SaveUserGroup(anUserGroup);
                if (stat)
                {
                    MessageBox.Show("Group Name has been Saved", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitialTaskforUserGroup();
                }
            }
            else
            {
                MessageBox.Show("Group Name Already Exist.", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateOperationofUserGroup()
        {
            bool stat = false;
            if (aBuserGroup.DoesExistGroupinUpdateMode(txtUserGroupName.Text.Trim(), UserGroupIdModify) == false)
            {
                EUserGroup anUserGroup = new EUserGroup();
                anUserGroup.Id = UserGroupIdModify;
                anUserGroup.GroupName = txtUserGroupName.Text.Trim();
                stat = aBuserGroup.UpdateUserGroup(anUserGroup);
                if (stat)
                {
                    MessageBox.Show("Group Name has been Updated", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitialTaskforUserGroup();
                }
            }
            else
            {
                MessageBox.Show("Group Name Already Exist.", "User Group", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CheckFieldofUserGroup()
        {
            if (txtUserGroupName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Group Name.", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUserGroupName.Focus();
                return false;
            }
            return true;
        }

        private void EditUserGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvUserGroup.SelectedIndex > -1)
                {
                    if ((lvUserGroup.SelectedItem as EUserGroup).GroupName.ToLower() == "Super Admin".ToLower())
                    {
                        MessageBox.Show(" This Group can not be Modified.", "User Group", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        EUserGroup anEUserGroup = lvUserGroup.SelectedItem as EUserGroup;
                        UserGroupIdModify = anEUserGroup.Id;
                        txtUserGroupName.Text = anEUserGroup.GroupName;
                        btnAddUserGroup.Content = "Modify";
                    }
                }
                else
                {
                    MessageBox.Show("Select an User Group First.", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvUserGroup.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Selection.", "User Group", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RemoveUserGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvUserGroup.SelectedIndex > -1)
                {
                    if ((lvUserGroup.SelectedItem as EUserGroup).GroupName.ToLower() == "Super Admin".ToLower())
                    {
                        MessageBox.Show(" This Group can not be Removed.", "User Group", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure want to Delete the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (MessageBox.Show("After Deletion You will Lost  This Group Related All Information.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                            {
                                bool stat = false;
                                EUserGroup anEUserGroup = lvUserGroup.SelectedItem as EUserGroup;
                                stat = aBuserGroup.DeleteUserGroup(anEUserGroup.Id);
                                if (stat)
                                {
                                    MessageBox.Show("Group Information has been Deleted", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                                    InitialTaskforUserGroup();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select an User Group First.", "User Group", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Deletion.", "User Group", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnResetUsergroup_Click(object sender, RoutedEventArgs e)
        {
            InitialTaskforUserGroup();
        }

        /************************************************************************************************************************************************************/
        /************************************************ END of USER GROUP*****************************************************************************************/

        /************************************************************************************************************************************************************/
        /************************************************ USER CREATION,MODIFICATION*****************************************************************************************/
        BUser aBUser = new BUser();
        long userIdforModify = 0;

        private void InitialTaskforUser()
        {
            ResetUser();
        }

        private void PopulateUserList()
        {
            try
            {
                lvUser.ItemsSource = aBUser.GetAllUser();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Retrieving Information.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateUserGroupCombo()
        {
            try
            {
                BUserGroup aBUserGroup = new BUserGroup();
                cmbUserGroup.ItemsSource = aBUserGroup.GetAllUserGroup();
                cmbUserGroup.DisplayMemberPath = "GroupName";
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading User Group Name.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofUser())
                {
                    if (btnAddUser.Content.ToString() == "Add")
                    {
                        SaveOperationofUser();
                    }
                    else if (btnAddUser.Content.ToString() == "Modify")
                    {
                        UpdateOperationofUser();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Storing Information.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveOperationofUser()
        {
            bool stat = false;
            if (aBUser.DoesExistUserinSaveMode(txtUserName.Text.Trim()) == false)
            {
                EUser anUser = new EUser();
                anUser.UserName = txtUserName.Text.Trim();
                anUser.UserPassword = txtPassword.Password;
                anUser.UserGroupId = (cmbUserGroup.SelectedItem as EUserGroup).Id;
                stat = aBUser.SaveUserInfo(anUser);
                if (stat)
                {
                    MessageBox.Show("User Info has been Saved.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetUser();
                }
            }
            else
            {
                MessageBox.Show("User Already Exist.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateOperationofUser()
        {
            bool stat = false;
            if (aBUser.DoesExistUserinUpdateMode(txtUserName.Text.Trim(), userIdforModify) == false)
            {
                EUser anUser = new EUser();
                anUser.Id = userIdforModify;
                anUser.UserName = txtUserName.Text.Trim();
                anUser.UserPassword = txtPassword.Password;
                anUser.UserGroupId = (cmbUserGroup.SelectedItem as EUserGroup).Id;
                stat = aBUser.UpdateUserInfo(anUser);
                if (stat)
                {
                    MessageBox.Show("User Info has been Updated.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetUser();
                }
            }
            else
            {
                MessageBox.Show("User Already Exist.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CheckFieldofUser()
        {
            if (txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter User Name.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUserName.Focus();
                return false;
            }
            if (txtPassword.Password == string.Empty)
            {
                MessageBox.Show("Please Enter Password.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPassword.Focus();
                return false;
            }
            if (txtConfirmPass.Password == string.Empty)
            {
                MessageBox.Show("Please Enter Confirm Password.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtConfirmPass.Focus();
                return false;
            }
            if (txtPassword.Password != txtConfirmPass.Password)
            {
                MessageBox.Show("Password and Confirm Password Must be Same.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtConfirmPass.Focus();
                return false;
            }
            if (cmbUserGroup.Text == string.Empty)
            {
                MessageBox.Show("Please Select User Group.", "User Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbUserGroup.Focus();
                return false;
            }
            return true;
        }

        private void btnResetUser_Click(object sender, RoutedEventArgs e)
        {
            ResetUser();
        }

        private void ResetUser()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtConfirmPass.Password = string.Empty;
            btnAddUser.Content = "Add";
            PopulateUserGroupCombo();
            PopulateUserList();
        }
      
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvUser.SelectedIndex > -1)
                {
                    EUser anEUser = lvUser.SelectedItem as EUser;
                    userIdforModify = anEUser.Id;
                    txtUserName.Text = anEUser.UserName;
                    txtPassword.Password = anEUser.UserPassword;
                    txtConfirmPass.Password = anEUser.UserPassword;
                    cmbUserGroup.Text = anEUser.UserGroupName;
                    btnAddUser.Content = "Modify";
                }
                else
                {
                    MessageBox.Show("Select an User First.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Updating.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvUser.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Are you sure want to Delete the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        bool stat = false;
                        EUser anEUser = lvUser.SelectedItem as EUser;
                        stat = aBUser.DeleteUser(anEUser.Id);
                        if (stat)
                        {
                            MessageBox.Show("user Information has been Deleted.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetUser();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select an User First.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Deletion.", "Modify User", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /************************************************************************************************************************************************************/
        /************************************************ END of USER***********************************************************************************************/


        /************************************************************************************************************************************************************/
        /************************************************ MODULE PERMISSION*****************************************************************************************/
        BModulePermission objBModulePermission = new BModulePermission();
        List<string> listAvailablemodule = new List<string>();
        List<string> listPermittedModule = new List<string>();

        private void InitialTaskforModule()
        {
            PopulateUserGroupComboforModule();
            LoadAvailableModule();
            lbNotPermittedModule.Items.Clear();
            lbPermittedModule.Items.Clear();
        }
        
        //string[] availableModule = new string[] { "Accounting", "HRM", "Security", "Admin Tools" };
        string[] availableModule = new string[] { "Accounting","Security", "Admin Tools" };
        private void LoadAvailableModule()
        {
            try
            {
                foreach (string item in availableModule)
                {
                    listAvailablemodule.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Available Module.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PopulateUserGroupComboforModule()
        {
            try
            {
                BUserGroup aBUserGroup = new BUserGroup();
                cmbUserGroupofModule.Items.Clear();
                foreach (var userGroup in aBUserGroup.GetAllUserGroup())
                {
                    if (userGroup.GroupName.ToLower() != "Super Admin".ToLower())
                    {
                        cmbUserGroupofModule.Items.Add(userGroup);
                    }
                }
                cmbUserGroupofModule.DisplayMemberPath = "GroupName";
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading User Group Name.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbUserGroupofModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbUserGroupofModule.SelectedIndex > -1)
                {
                    long groupId = (cmbUserGroupofModule.SelectedItem as EUserGroup).Id;
                    
                    lbPermittedModule.Items.Clear();
                    lbNotPermittedModule.Items.Clear();
                    listPermittedModule.Clear();

                    foreach (EModulePermission objPermittedModule in objBModulePermission.GetPermittedModule(groupId))
                    {
                        lbPermittedModule.Items.Add(objPermittedModule.ModuleName);
                        listPermittedModule.Add(objPermittedModule.ModuleName);
                    }

                    if (listPermittedModule.Count > 0)
                    {
                        btnSaveModulePermission.Content = "Update";
                    }
                    else
                    {
                        btnSaveModulePermission.Content = "Save";
                    }

                    foreach (string NotPermittedModule in objBModulePermission.GetNOTPermittedModule(listAvailablemodule, listPermittedModule))
                    {
                        lbNotPermittedModule.Items.Add(NotPermittedModule);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading User Groups Permittted Module.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMoveModule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbNotPermittedModule.SelectedIndex > -1)
                {
                    lbPermittedModule.Items.Add(lbNotPermittedModule.SelectedItem);
                    lbNotPermittedModule.Items.Remove(lbNotPermittedModule.SelectedItem);
                }
                else
                {
                    MessageBox.Show("Please Select Not Permitted Module First.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbNotPermittedModule.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDeleteModule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbPermittedModule.SelectedIndex > -1)
                {
                    lbNotPermittedModule.Items.Add(lbPermittedModule.SelectedItem);
                    lbPermittedModule.Items.Remove(lbPermittedModule.SelectedItem);
                }
                else
                {
                    MessageBox.Show("Please Select Permitted Module First.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbPermittedModule.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSaveModulePermission_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofModule())
                {
                    if (btnSaveModulePermission.Content.ToString() == "Save")
                    {
                        if (SaveModulePermission())
                        {
                            MessageBox.Show("Saved Success.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetModulePermission();
                        }
                    }
                    else
                    {
                        if (UpdateModulePermission())
                        {
                            MessageBox.Show("Updated Success.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetModulePermission();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Saving.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool UpdateModulePermission()
        {
            bool stat = false;
            //Insert
            foreach (string module in lbPermittedModule.Items)
            {
                if (DuplicacyCheckPermittedModuleforInsert(module))
                {
                    EModulePermission objEmodule = new EModulePermission();
                    objEmodule.ModuleName = module.ToString();
                    objEmodule.UserGroupId = (cmbUserGroupofModule.SelectedItem as EUserGroup).Id;
                    stat = objBModulePermission.SaveModulePermission(objEmodule);
                }
            }
            //Delete

            foreach (string module in listPermittedModule)
            {
                if (DuplicacyCheckPermittedModuleforDelete(module))
                {
                    EModulePermission objEmodule = new EModulePermission();
                    objEmodule.ModuleName = module.ToString();
                    objEmodule.UserGroupId = (cmbUserGroupofModule.SelectedItem as EUserGroup).Id;
                    stat = objBModulePermission.DeleteSingleModulePermission(objEmodule);
                }
            }           
            return stat;
        }
        
        /*This checking is needed for Update Module*/
        private bool DuplicacyCheckPermittedModuleforInsert(string moduleName)
        {
            foreach (string itm in listPermittedModule)
            {
                if (itm == moduleName)
                {
                    return false;
                }
            }
                return true;
        }
        
        private bool DuplicacyCheckPermittedModuleforDelete(string moduleName)
        {
            foreach (string itm in lbPermittedModule.Items)
            {
                if (itm == moduleName)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckFieldofModule()
        {
            if (cmbUserGroupofModule.Text == string.Empty)
            {
                MessageBox.Show("Please Select User Group.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbUserGroupofModule.Focus();
                return false;
            }
            if (lbPermittedModule.Items.Count == 0)
            {
                MessageBox.Show("Please Fillup Permitted List.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                lbPermittedModule.Focus();
                return false;
            }
            return true;
        }

        private bool SaveModulePermission()
        {
            bool stat = false;
            foreach (var item in lbPermittedModule.Items)
            {
                EModulePermission objEmodule = new EModulePermission();
                objEmodule.ModuleName = item.ToString();
                objEmodule.UserGroupId = (cmbUserGroupofModule.SelectedItem as EUserGroup).Id;
                stat = objBModulePermission.SaveModulePermission(objEmodule);
            }
            return stat;
        }

        private void btnDeleteModulePermission_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofModule())
                {
                    if (MessageBox.Show("Are you sure want to Delete the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (DeleteModulePermission())
                        {
                            MessageBox.Show("Deletion Success.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Deletion.", "Module Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool DeleteModulePermission()
        {
            return objBModulePermission.DeleteModulePermission((cmbUserGroupofModule.SelectedItem as EUserGroup).Id);
        }

        private void btnResetModulePermission_Click(object sender, RoutedEventArgs e)
        {
            ResetModulePermission();
        }

        private void ResetModulePermission()
        {
            PopulateUserGroupComboforModule();
            lbPermittedModule.Items.Clear();
            lbNotPermittedModule.Items.Clear();
            listPermittedModule.Clear();
            btnSaveModulePermission.Content = "Save";
        }

        /************************************************************************************************************************************************************/
        /************************************************ MENU PERMISSION*****************************************************************************************/

        BMenuPermission objBMenuPermission = new BMenuPermission();        
        Menu mnu = new Menu();
        TreeView  treeAvailableMenu = new TreeView();
       
        private void InitialTaskforMenu()
        {            
            PopulateUserGroupComboforMenu();
            cmbModuleName.Items.Clear();
            treeNotPermittedMenu.Items.Clear();
            treePermittedMenu.Items.Clear();
        }

        private void LoadMenuIntoTree()
        {
            treeAvailableMenu.Items.Clear();
            try
            {
                foreach (MenuItem it in mnu.Items)
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Foreground = Brushes.Maroon;
                    treeItem.Header = it.Header;
                    treeItem.Name = it.Name;
                    ProcessTree(it, treeItem);
                    treeAvailableMenu.Items.Add(treeItem);
                    treeItem.IsExpanded = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Available Menu", "Security", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(MenuItem it, TreeViewItem newChild)
        {
            foreach (MenuItem obj in it.Items)
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = obj.Header;
                child.Name = obj.Name;
                newChild.Items.Add(child);                
                ProcessTree(obj, child);
            }
        }

        private void PopulateUserGroupComboforMenu()
        {
            try
            {
                BUserGroup aBUserGroup = new BUserGroup();
                cmbUserGroupMenuPerm.Items.Clear();
                foreach (var userGroup in aBUserGroup.GetAllUserGroup())
                {
                    if (userGroup.GroupName.ToLower() != "Super Admin".ToLower())
                    {
                        cmbUserGroupMenuPerm.Items.Add(userGroup);
                    }
                }
                cmbUserGroupMenuPerm.DisplayMemberPath = "GroupName";
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading User Group Name.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbUserGroupMenuPerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbUserGroupMenuPerm.SelectedIndex > -1)
                {
                    cmbModuleName.Items.Clear();
                    BModulePermission objBMP = new BModulePermission();
                    long groupId = (cmbUserGroupMenuPerm.SelectedItem as EUserGroup).Id;
                    foreach (EModulePermission objPermittedModule in objBMP.GetPermittedModule(groupId))
                    {
                        cmbModuleName.Items.Add(objPermittedModule);
                    }
                    treeNotPermittedMenu.Items.Clear();
                    treePermittedMenu.Items.Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading User Groups Permittted Module.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbModuleName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbModuleName.SelectedIndex > -1)
                {
                    if ((cmbModuleName.SelectedItem as EModulePermission).ModuleName == "Accounting")
                    {
                        CHART_ACC_UI.MainWindow window = new CHART_ACC_UI.MainWindow();
                        mnu.Items.Clear();
                        mnu = window.GetAllAccountingMenu();
                    }
                    else if ((cmbModuleName.SelectedItem as EModulePermission).ModuleName == "HRM")
                    {
                        //from Atik HRM module
                        mnu.Items.Clear();
                    }
                    else
                    {
                        mnu.Items.Clear();
                    }
                    LoadMenuIntoTree();
                    PopulateTreeGroupWise();
                    PopulateNotPermittedTree();
                    if (treePermittedMenu.Items.Count > 0)
                    {
                        btnSaveMenuPermission.Content = "Update";
                    }
                    else
                    {
                        btnSaveMenuPermission.Content = "Save";
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Permittted Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*****************Most Logical Part of this File***************************
         * Check Available tree(get from Module UI) and treePermittedMenu item
         * If portion task:if available tree item does not exist in the permittedtree
         * Else portion task: if exist then count their child and if missmatch child then checking and add mismatch item into NotPermitted tree.        
        ************************************************************************************/

        private void PopulateNotPermittedTree()
        {
            treeNotPermittedMenu.Items.Clear();
            if (treeAvailableMenu.Items.Count > 0)
            {
                for (int i = 0; i < treeAvailableMenu.Items.Count; i++)
                {
                    TreeViewItem tree = treeAvailableMenu.Items[i] as TreeViewItem;
                    if (DuplicacyCheck(tree, treePermittedMenu))
                    {
                        TreeViewItem trItem = new TreeViewItem();
                        trItem.Header = tree.Header;
                        trItem.Name = tree.Name;
                        trItem.Foreground = Brushes.Maroon;
                        trItem.IsExpanded = true;
                        treeNotPermittedMenu.Items.Add(trItem);
                        if (tree.HasItems)
                        {
                            foreach (TreeViewItem it in tree.Items)
                            {
                                TreeViewItem subNode = new TreeViewItem();
                                subNode.Header = it.Header;
                                subNode.Name = it.Name;
                                trItem.Items.Add(subNode);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < treePermittedMenu.Items.Count; j++)
                        {
                            TreeViewItem objTree = treePermittedMenu.Items[j] as TreeViewItem;
                            if (objTree.Name == tree.Name && objTree.Items.Count != tree.Items.Count)
                            {
                                TreeViewItem trItem = new TreeViewItem();
                                trItem.Header = tree.Header;
                                trItem.Name = tree.Name;
                                trItem.Foreground = Brushes.Maroon;
                                trItem.IsExpanded = true;
                                treeNotPermittedMenu.Items.Add(trItem);
                                if (tree.HasItems)
                                {
                                    foreach (TreeViewItem objFirstTreeItem in tree.Items)
                                    {
                                        if (duplicacyCheckChild(objFirstTreeItem))
                                        {
                                            TreeViewItem subNode = new TreeViewItem();
                                            subNode.Header = objFirstTreeItem.Header;
                                            subNode.Name = objFirstTreeItem.Name;
                                            trItem.Items.Add(subNode);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        private bool duplicacyCheckChild(TreeViewItem child)
        {
            if (treePermittedMenu.Items.Count > 0)
            {
                for (int i = 0; i < treePermittedMenu.Items.Count; i++)
                {
                    TreeViewItem obj = treePermittedMenu.Items[i] as TreeViewItem;
                    if (obj.HasItems)
                    {
                        foreach (TreeViewItem it in obj.Items)
                        {
                            if (it.Header.ToString() == child.Header.ToString() && it.Name == child.Name)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        
        private void PopulateTreeGroupWise()
        {
            treePermittedMenu.Items.Clear();
            try
            {
                EMenuPermission objEMP = new EMenuPermission();
                objEMP.UserGroupId = (cmbUserGroupMenuPerm.SelectedItem as EUserGroup).Id;
                objEMP.ModuleId = (cmbModuleName.SelectedItem as EModulePermission).Id;
                foreach (EMenuPermission objEParent in objBMenuPermission.GetAccParentMenuGroupWise(objEMP))
                {
                    TreeViewItem newChild = new TreeViewItem();
                    newChild.Header = objEParent.ParentMenuContent;
                    newChild.Name = objEParent.ParentMenuName;
                    newChild.Foreground = Brushes.Maroon;
                    newChild.IsExpanded = true;
                    treePermittedMenu.Items.Add(newChild);
                    objEMP.ParentMenuName = objEParent.ParentMenuName;
                    List<EMenuPermission> listChild = objBMenuPermission.GetAccChildMenuGroupWise(objEMP);
                    if (listChild.Count > 0)
                    {
                        foreach (EMenuPermission objEchild in listChild)
                        {
                            TreeViewItem child = new TreeViewItem();
                            child.Header = objEchild.ChildMenuContent;
                            child.Name = objEchild.ChildMenuName;
                            newChild.Items.Add(child);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void treeNotPermittedMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = treeNotPermittedMenu.SelectedItem as TreeViewItem;
            if (item != null)
            {
                if (item.HasItems)
                {
                    btnMoveMenuItem.IsEnabled = false;
                    btnMoveAllMenuItem.IsEnabled = true;
                }
                else
                {
                    btnMoveMenuItem.IsEnabled = true;
                    btnMoveAllMenuItem.IsEnabled = false;
                }
            }
        }

        private void treePermittedMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = treePermittedMenu.SelectedItem as TreeViewItem;
            if (item != null)
            {
                if (item.HasItems)
                {
                    btnDeleteMenuItem.IsEnabled = false;
                    btnDeleteAllMenuItem.IsEnabled = true;
                }
                else
                {
                    btnDeleteMenuItem.IsEnabled = true;
                    btnDeleteAllMenuItem.IsEnabled = false;
                }
            }
        }

        private void btnMoveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Move_DeleteMenu(treeNotPermittedMenu, treePermittedMenu);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Move Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Move_DeleteMenu(TreeView treeSource, TreeView treeDestination)
        {
            TreeViewItem item = treeSource.SelectedItem as TreeViewItem;
            if (item != null)
            {
                /*****************************Parent****************************************************/

                TreeViewItem parentItem = item.Parent as TreeViewItem;
                if (parentItem != null)
                {
                    TreeViewItem PermittedParent = new TreeViewItem();
                    PermittedParent.Header = parentItem.Header;
                    PermittedParent.Name = parentItem.Name;
                    PermittedParent.Foreground = Brushes.Maroon;
                    /**************************************************************************************/
                    TreeViewItem child = new TreeViewItem();
                    child.Header = item.Header;
                    child.Name = item.Name;
                    if (DuplicacyCheck(PermittedParent, treeDestination))
                    {
                        treeDestination.Items.Add(PermittedParent);
                        PermittedParent.Items.Add(child);
                        PermittedParent.IsExpanded = true;
                        parentItem.Items.Remove(item);
                        if (parentItem.HasItems == false)
                        {
                            treeSource.Items.Remove(parentItem);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < treeDestination.Items.Count; i++)
                        {
                            var obj = treeDestination.Items[i] as TreeViewItem;
                            if (obj.Name == PermittedParent.Name)
                            {
                                obj.Items.Add(child);
                                parentItem.Items.Remove(item);
                                if (parentItem.HasItems == false)
                                {
                                    treeSource.Items.Remove(parentItem);
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    TreeViewItem child = new TreeViewItem();
                    child.Header = item.Header;
                    child.Name = item.Name;
                    child.Foreground = Brushes.Maroon;
                    treeDestination.Items.Add(child);
                    treeSource.Items.Remove(item);
                }
            }
        }

        private bool DuplicacyCheck(TreeViewItem node, TreeView tr)
        {
            if (tr.Items.Count > 0)
            {
                for (int i = 0; i < tr.Items.Count; i++)
                {
                    TreeViewItem tree = tr.Items[i] as TreeViewItem;
                    bool check = (tree.Header.ToString() == node.Header.ToString());
                    bool checkName = (tree.Name == node.Name);
                    if (check && checkName)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnMoveAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Move_Delete_AllMenu(treeNotPermittedMenu, treePermittedMenu);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Move Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Move_Delete_AllMenu(TreeView treeSource, TreeView treeDestination)
        {
            TreeViewItem item = treeSource.SelectedItem as TreeViewItem;
            if (item != null)
            {
                /*****************************Parent****************************************************/
                if (DuplicacyCheck(item, treeDestination))
                {
                    TreeViewItem node = new TreeViewItem();
                    node.Header = item.Header;
                    node.Name = item.Name;
                    node.Foreground = Brushes.Maroon;
                    node.IsExpanded = true;
                    treeDestination.Items.Add(node);
                    /**************************************************************************************/
                    if (item.HasItems)
                    {
                        foreach (TreeViewItem it in item.Items)
                        {
                            TreeViewItem subNode = new TreeViewItem();
                            subNode.Header = it.Header;
                            subNode.Name = it.Name;
                            node.Items.Add(subNode);
                        }
                    }
                    treeSource.Items.Remove(item);
                }
            }
        }

        private void btnDeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Move_DeleteMenu(treePermittedMenu, treeNotPermittedMenu);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Delete Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Move_Delete_AllMenu(treePermittedMenu, treeNotPermittedMenu);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Delete Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSaveMenuPermission_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofMenu())
                {
                    if (btnSaveMenuPermission.Content.ToString() == "Save")
                    {

                        bool SaveStatus = false;
                        SaveStatus = SaveMenuPermission(SaveStatus);
                        if (SaveStatus == true)
                        {
                            MessageBox.Show("User menu has been successfully saved", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetMenu();
                        }
                    }
                    else if (btnSaveMenuPermission.Content.ToString() == "Update")
                    {
                        objBMenuPermission.DeleteUserRole((cmbUserGroupMenuPerm.SelectedItem as EUserGroup).Id, (cmbModuleName.SelectedItem as EModulePermission).Id);
                        bool Status = false;
                        Status = SaveMenuPermission(Status);
                        if (Status == true)
                        {
                            MessageBox.Show("User menu has been successfully Updated.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetMenu();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool SaveMenuPermission(bool SaveStatus)
        {
            for (int i = 0; i < treePermittedMenu.Items.Count; i++)
            {
                TreeViewItem objTreeItem = treePermittedMenu.Items[i] as TreeViewItem;
                EMenuPermission objMenu = new EMenuPermission();
                objMenu.UserGroupId = ((EUserGroup)cmbUserGroupMenuPerm.SelectedItem).Id;
                objMenu.ModuleId = (cmbModuleName.SelectedItem as EModulePermission).Id;
                objMenu.ParentMenuName = objTreeItem.Name;
                objMenu.ParentMenuContent = objTreeItem.Header.ToString();
                if (objTreeItem.HasItems)
                {
                    foreach (TreeViewItem it in objTreeItem.Items)
                    {
                        objMenu.ChildMenuName = it.Name;
                        objMenu.ChildMenuContent = it.Header.ToString();
                        SaveStatus = objBMenuPermission.SaveMenuUserGroupWise(objMenu);
                    }
                }
                else
                {
                    SaveStatus = objBMenuPermission.SaveMenuUserGroupWise(objMenu);
                }
            }
            return SaveStatus;
        }

        private bool CheckFieldofMenu()
        {
            if (cmbUserGroupMenuPerm.Text == string.Empty)
            {
                MessageBox.Show("Please Select User Group.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbUserGroupMenuPerm.Focus();
                return false;
            }
            if (treePermittedMenu.Items.Count == 0)
            {
                MessageBox.Show("Please Fillup Permitted Menu.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                treePermittedMenu.Focus();
                return false;
            }
            return true;
        }

        private void btnDeleteMenuPermission_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldofMenu())
                {
                    if (MessageBox.Show("Are you sure want to Delete the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (objBMenuPermission.DeleteUserRole((cmbUserGroupMenuPerm.SelectedItem as EUserGroup).Id, (cmbModuleName.SelectedItem as EModulePermission).Id))
                        {
                            MessageBox.Show("Deletion Success.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetMenu();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur while Deletion.", "Menu Permission", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetMenu()
        {
            treeAvailableMenu.Items.Clear();
            treeNotPermittedMenu.Items.Clear();
            treePermittedMenu.Items.Clear();
            PopulateUserGroupComboforMenu();
            cmbModuleName.Items.Clear();
            btnSaveMenuPermission.Content = "Save";
        }

        private void btnResetMenuPermission_Click(object sender, RoutedEventArgs e)
        {
            ResetMenu();
        }

        private void tabControlSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is TabControl)
                {
                    string moduleUserGroup = cmbUserGroupofModule.Text;
                    string menuUserGoup = cmbUserGroupMenuPerm.Text;
                    string moduleName = cmbModuleName.Text;

                    if (tabItemUserGroup.IsSelected)
                    {
                        InitialTaskforUserGroup();
                    }
                    else if (tabItemUser.IsSelected)
                    {
                        InitialTaskforUser();
                    }
                    else if (tabItemModule.IsSelected)
                    {
                        InitialTaskforModule();
                    }
                    else if (tabItemMenu.IsSelected)
                    {
                        InitialTaskforMenu();
                    }
                    LoadUserSelectedcontrol(moduleUserGroup, menuUserGoup, moduleName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur","Security",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void LoadUserSelectedcontrol(string moduleUserGroup, string menuUserGoup, string moduleName)
        {
            if (moduleUserGroup != string.Empty)
            {
                BUserGroup objBuserGroup = new BUserGroup();
                if (objBuserGroup.DoesExistGroupinSaveMode(moduleUserGroup))
                {
                    cmbUserGroupofModule.Text = moduleUserGroup;
                }
            }
            if (menuUserGoup != string.Empty)
            {
                BUserGroup objBuserGroup = new BUserGroup();
                if (objBuserGroup.DoesExistGroupinSaveMode(moduleUserGroup))
                {
                    cmbUserGroupMenuPerm.Text = moduleUserGroup;
                }
                for (int i = 0; i < cmbModuleName.Items.Count; i++)
                {
                    if (moduleName == (cmbModuleName.Items[i] as EModulePermission).ModuleName)
                    {
                        cmbModuleName.Text = moduleName;
                        break;
                    }
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

    }
}
