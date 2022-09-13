using MainModule.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginModule.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        IMainShellInitializer _mainShellInitializer;
        IDialogService _dialogService;

        public ICommand UsernameChangeCommand { get; set; }
        public ICommand PasswordChangeCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username , value);

        }

        private Account _selectedAccount;

        public Account SelectedAccount
        {

            get { return _selectedAccount; }
            set { SetProperty(ref _selectedAccount, value); }
        }

        private string _password { get; set; }

        private List<Account> _listAccount;

        public List<Account> ListAccount
        {
            get => _listAccount;
            set => SetProperty(ref _listAccount, value);
        }

        public LoginWindowViewModel(IMainShellInitializer mainShellInitializer,IDialogService dialogService)
        {
            _mainShellInitializer = mainShellInitializer;
            _dialogService = dialogService;

            bindAccount();

            PasswordChangeCommand = new DelegateCommand<PasswordBox>(PasswordChanged);
            LoginCommand = new DelegateCommand<Window>(Login);
        }
        private void bindAccount()
        {
            var client = new RestClient(new Uri("http://gf-uat-service-lb-716780687.ap-southeast-1.elb.amazonaws.com:8888/api/accounts"));
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);

            try
            {
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    ListAccount = JsonConvert.DeserializeObject<List<Account>>(response.Content);
                    for (int i = 0; i < ListAccount.Count; i++)
                    {
                        ListAccount[i].fullname = "ชื่อ-" + ListAccount[i].name + " นามสกุล-" + ListAccount[i].surname;
                        ListAccount[i].fullinfo = "ID : " + ListAccount[i].id + "   /   BRN : " + ListAccount[i].branchCode;
                    }
                    ListAccount = ListAccount;
                    //ComboBoxAccount.ItemsSource = ListAccount;
                    //ComboBoxAccount.SelectedValuePath = "id";
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
        }



        void PasswordChanged(PasswordBox passwordBox)
        {
            _password = passwordBox.Password;
        }

        void Login(Window loginWindow)
        {
            var accId = SelectedAccount.id;
            var existingAccount = ListAccount.Where(a => a.id == accId).FirstOrDefault();

            if (existingAccount != null || _password != null)
            {
                AccountManager.GetInstance().Account = existingAccount;
                //open new window
                new MainBootstrapper(_mainShellInitializer).Run();
                //close login window
                loginWindow.Close();
            }
            else
            {
                //dialogService.ShowDialog("AlertDialog", new DialogParameters("message=Login Failed,username or password incorrect"), (r) => { System.Diagnostics.Debug.WriteLine("LoginWindowModel: dialog result = " + r.Result); });
                //return;
                MessageBox.Show("Login Failed,username or password incorrect", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
