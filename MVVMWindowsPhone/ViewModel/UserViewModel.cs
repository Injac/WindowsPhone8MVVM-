using GalaSoft.MvvmLight.Command;
using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.Services;
using MVVMWindowsPhone.Core.Portable.ViewModel;
using MVVMWindowsPhone.DAL;
using MVVMWindowsPhone.Model;
using Ninject;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using Windows.Storage;
using Windows.UI.Core;

namespace VVMWindowsPhone.ViewModel
{
    /// <summary>
    /// The user view model class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class UserViewModel:BaseViewModel<SQLiteUser, SQLiteAsyncConnection>
    {

        /// <summary>
        /// Gets or sets the add new user command.
        /// </summary>
        /// <value>
        /// The add new user command.
        /// </value>
        public RelayCommand<SQLiteUser> AddNewUserCommand { get; set; }

        /// <summary>
        /// Gets or sets the delete user command.
        /// </summary>
        /// <value>
        /// The delete user command.
        /// </value>
        public RelayCommand DeleteUserCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit user command.
        /// </summary>
        /// <value>
        /// The edit user command.
        /// </value>
        public RelayCommand EditUserCommand { get; set; }


        /// <summary>
        /// Gets or sets the filter users command.
        /// </summary>
        /// <value>
        /// The filter users command.
        /// </value>
        public RelayCommand<object> FilterUsersCommand { get; set; }

        /// <summary>
        /// Gets or sets the dialog result command.
        /// </summary>
        /// <value>
        /// The dialog result command.
        /// </value>
        public RelayCommand<string> DialogResultCommand { get; set; }

               

        /// <summary>
        /// Gets or sets the initialize.
        /// </summary>
        /// <value>
        /// The initialize.
        /// </value>
        public RelayCommand Init { get; set; }

      
        private SQLiteUser crudUser;

        /// <summary>
        /// Gets or sets the crud user.
        /// </summary>
        /// <value>
        /// The crud user.
        /// </value>
        public SQLiteUser CrudUser
        {
            get { return crudUser; }
            set 
            {
                if (crudUser != value)
                {
                    crudUser = value;

                    RaisePropertyChanged("CrudUser");
                }
            }
        }

        
        private string dialogTitle;

        /// <summary>
        /// Gets or sets the dialog title.
        /// </summary>
        /// <value>
        /// The dialog title.
        /// </value>
        public string DialogTitle
        {
            get { return dialogTitle; }
            set
            {
                if (dialogTitle != value)
                {
                    dialogTitle = value;

                    RaisePropertyChanged("DialogTitle");
                }
            }
        }

        private string filterText;

        public string FilterExpression
        {
            get { return filterText; }
            set 
            {
               
                    filterText = value;
                    RaisePropertyChanged("FilterText");
                

                this.FilterUsersCommand.RaiseCanExecuteChanged();
            }
        }


        private bool userControlVisibility;

        /// <summary>
        /// Gets or sets a value indicating whether [user control visibility].
        /// </summary>
        /// <value>
        /// <c>true</c> if [user control visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool UserControlVisibility
        {
            get { return userControlVisibility; }
            set 
            {
                if (userControlVisibility != value)
                {
                    userControlVisibility = value;
                    RaisePropertyChanged("UserControlVisibility");
                }
            }
        }

        private object selectedItem;

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public object SelectedItem
        {
            get { return selectedItem; }
            set 
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }

                this.EditUserCommand.RaiseCanExecuteChanged();
                this.DeleteUserCommand.RaiseCanExecuteChanged();
            }
        }


        private bool AddOrEdit;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="navService">The nav service.</param>
        [Inject]
        public UserViewModel(ITAPRepository<SQLiteUser, SQLiteAsyncConnection> repo, INavigationService navService)
            : base(repo, navService) 
        {
            
            this.AddNewUserCommand = new RelayCommand<SQLiteUser>(this.AddNewUser, this.CanExecuteAddNewUser);
            this.DeleteUserCommand = new RelayCommand(this.DeleteUser, this.CanExecuteEditCommand);
            this.EditUserCommand = new RelayCommand(this.EditUser, this.CanExecuteEditCommand);
            this.FilterUsersCommand = new RelayCommand<object>(this.FilterUsers, this.CanExecuteFilterCommand);
            this.DialogResultCommand = new RelayCommand<string>(this.DialogResultCheck, this.CanExecuteCommandWithParameter);
            this.Init = new RelayCommand(this.InitData, this.CanExecuteCommand);
          
            this.CrudUser = new SQLiteUser();

            var users = this.repository.GetAllEntries();
            
        }

        /// <summary>
        /// Determines whether this instance [can execute filter command] the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        private bool CanExecuteFilterCommand(object arg)
        {

                    var evtArgs = (System.Windows.Input.KeyEventArgs)arg;

                   if (evtArgs.Key == System.Windows.Input.Key.Enter)
                   {
                       return true;
                   }
              
                                 
          

           return false;
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private async void InitData()
        {
                var users = await this.repository.GetAllEntries();

                if (users != null)
                {
                    RefreshDataSource(users);
                }
                               
        }

        /// <summary>
        /// Dialogs the result check.
        /// </summary>
        /// <param name="obj">The object.</param>
        private async void DialogResultCheck(string obj)
        {
           if(obj == "ok")
           {
               if(this.AddOrEdit)
               {
                    var user = await this.repository.AddEntry(this.CrudUser);
            
                    this.Data.Clear();
            
                    var users = await this.repository.GetAllEntries();

                    RefreshDataSource(users);

                    this.CrudUser = new SQLiteUser();
               }
               else
               {
                   var updatedUser = await this.repository.UpdateEntry(
                           (SQLiteUser)this.SelectedItem, this.CrudUser
                       );

                   this.Data.Clear();

                   var users = await this.repository.GetAllEntries();

                   RefreshDataSource(users);

                   this.CrudUser = new SQLiteUser();
               }


               this.UserControlVisibility = false;
               this.RaiseExecuteChangedDialog();
           }
           
           if(obj == "cancel")
           {
               this.CrudUser = new SQLiteUser();
               this.UserControlVisibility = false;
               this.RaiseExecuteChangedDialog();
           }
           
        }

        /// <summary>
        /// Raises the execute changed dialog.
        /// </summary>
        private void RaiseExecuteChangedDialog()
        {
            this.AddNewUserCommand.RaiseCanExecuteChanged();
            this.EditUserCommand.RaiseCanExecuteChanged();
            this.DeleteUserCommand.RaiseCanExecuteChanged();
            this.DialogResultCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines whether this instance [can execute add new user] the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        private bool CanExecuteAddNewUser(SQLiteUser arg)
        {
            if (this.CanExecuteCommand())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the new user.
        /// </summary>
        /// <param name="obj">The object.</param>
        private  void AddNewUser(SQLiteUser obj)
        {

            this.DialogTitle = "Add new user";
            this.AddOrEdit = true;
            this.UserControlVisibility = true;
            this.RaiseExecuteChangedDialog();
            
        }

        /// <summary>
        /// Refreshes the data source.
        /// </summary>
        /// <param name="list">The list.</param>
        public void RefreshDataSource(IQueryable<SQLiteUser> list)
        {
            list.ToList().ForEach(this.Data.Add);
        }

        /// <summary>
        /// Determines whether this instance [can execute command with parameter] the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        private bool CanExecuteCommandWithParameter(string arg)
        {
            if (!arg.Equals("cancel") && !arg.Equals("ok"))
            {
                if (this.CanExecuteCommand())
                {
                    if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (this.CanExecuteCommandDialogResult())
                {
                    if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

           return false;
        }

        /// <summary>
        /// Filters the users.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private async void FilterUsers(object obj)
        {
            if (this.FilterExpression.Length == 1 || string.IsNullOrEmpty(this.FilterExpression))
            {
                
                    //clear
                    this.Data.Clear();

                    var users = await this.repository.GetAllEntries();

                    RefreshDataSource(users);
                
            }
            else
            {
                //clear
                this.Data.Clear();

                var users = await this.repository.GetFilteredEntries(u => u.UserName.ToLower().Contains(this.FilterExpression.ToLower())
                    || u.Image.ToLower().Contains(this.filterText.ToLower()) || u.Url.ToLower().Contains(this.FilterExpression.ToLower())
                    );

                RefreshDataSource(users);
            }
              
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        private void EditUser()
        {

            var user = new SQLiteUser();

            user.Image = ((SQLiteUser)this.SelectedItem).Image;
            user.Url = ((SQLiteUser)this.SelectedItem).Url;
            user.UserName = ((SQLiteUser)this.SelectedItem).UserName;

            this.CrudUser = user;

            this.DialogTitle = "Edit user";
            this.AddOrEdit = false;
            this.UserControlVisibility = true;
            this.RaiseExecuteChangedDialog();
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private async void DeleteUser()
        {
            var result = MessageBox.Show("Delete this item?","CAUTION",MessageBoxButton.OKCancel);

            if(result == MessageBoxResult.OK)
            {
                   var deletedUser = this.repository.DeleteEntry((SQLiteUser)this.SelectedItem);

                   this.Data.Clear();

                   var users = await this.repository.GetAllEntries();

                   RefreshDataSource(users);
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute command].
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteCommand()
        {
            string dbPath = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "users.db"));

            return IsolatedStorageFile.GetUserStoreForApplication().FileExists(dbPath) && !this.UserControlVisibility;
        }

        /// <summary>
        /// Determines whether this instance [can execute edit command].
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteEditCommand()
        {

            return this.CanExecuteCommand() && !this.userControlVisibility && (this.SelectedItem != null);
           
        }


        /// <summary>
        /// Determines whether this instance [can execute command dialog result].
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteCommandDialogResult()
        {
            
            return  this.UserControlVisibility;
        }
        
    }

  
}
