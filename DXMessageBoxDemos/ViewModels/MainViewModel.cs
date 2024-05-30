using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DXMessageBoxDemos.View;
using System.Windows.Controls;
using System.ComponentModel;

namespace DXMessageBoxDemos.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        RegistrationViewModel registrationViewModel;

        IMessageBoxService AsyncMessageBoxService { get { return GetService<IMessageBoxService>(); } }
        IDialogService AsyncDialogService { get { return GetService<IDialogService>(); } }
        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: NotifyFullNameChanged); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: NotifyFullNameChanged); }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        private void NotifyFullNameChanged()
        {
            RaisePropertyChanged(() => FullName);
        }

        public ICommand ResetCommand { get; private set; }

        public MainViewModel()
        {
            ResetCommand = new DelegateCommand(() =>
            {
                FirstName = "";
                LastName = "";
            });

            ButtonText = "None";
            registrationViewModel = new RegistrationViewModel();
        }

        [Command]
        public void Save()
        {
            // Saved
        }
        [Command]
        public void DXDialog()
        {
            // 创建一个新的 DXDialog 对象
            var dialog = new DXDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Custom Dialog with UserControl",
            };

            // 创建并设置用户控件作为对话框的内容
            var myUserControl = new MyUserControl();
            dialog.Content = myUserControl;

            // 显示对话框并获取结果
            var result = dialog.ShowDialog();

            // 根据对话框结果进行处理
            if (result == true)
            {
                string name = myUserControl.UserName;
                int? age = myUserControl.UserAge;
                MessageBox.Show($"Name: {name}, Age: {age}");
            }
            else
            {
                MessageBox.Show("Dialog was cancelled.");
            }
        }
        [Command]
        public void BaseMsg()
        {
            MessageBox.Show("This is a standard .NET MessageBox", "Title", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        [Command]
        public void DxBaseMsg()
        {
            // 显示一个简单的消息框
            DXMessageBox.Show("This is a DevExpress DXMessageBox", "Title", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        [Command]
        public void BaseDXDialog()
        {
            var dialog = new DXDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Width = 500,
                Height = 300,
                Title = "Custom Dialog",
                Content = new TextBlock { Text = "This is a custom DXDialog", Margin = new Thickness(10) }
            };

            var result = dialog.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("OK clicked");
            }
            else
            {
                MessageBox.Show("Cancel clicked");
            }
        }
        [Command]
        public void CompDXDialog()
        {
            // 创建一个 StackPanel 作为对话框的内容
            var stackPanel = new StackPanel
            {
                Margin = new Thickness(10)
            };

            // 在 StackPanel 中添加控件
            stackPanel.Children.Add(new TextBlock { Text = "Enter your name:", Margin = new Thickness(0, 0, 0, 10) });
            var nameTextBox = new TextBox { Width = 200, Margin = new Thickness(0, 0, 0, 10) };
            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(new TextBlock { Text = "Select your age:", Margin = new Thickness(0, 0, 0, 10) });
            var ageComboBox = new ComboBox { Width = 200, Margin = new Thickness(0, 0, 0, 10) };
            ageComboBox.ItemsSource = new int[] { 18, 19, 20, 21, 22, 23, 24, 25 };
            stackPanel.Children.Add(ageComboBox);

            // 设置 StackPanel 作为对话框的内容
            var dialog = new DXDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Custom Dialog with Controls",
                Content = stackPanel
        };

            // 显示对话框并获取结果
            var result = dialog.ShowDialog();

            // 根据对话框结果进行处理
            if (result == true)
            {
                string name = nameTextBox.Text;
                int? age = ageComboBox.SelectedItem as int?;
                MessageBox.Show($"Name: {name}, Age: {age}");
            }
            else
            {
                MessageBox.Show("Dialog was cancelled.");
            }
        }
        [Command]
        public void UserControlDXDialog()
        {
            // 创建并设置用户控件作为对话框的内容
            var myUserControl = new MyUserControl();            
            var dialog = new DXDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Custom Dialog with UserControl",
                Content = myUserControl
            };            

            // 显示对话框并获取结果
            var result = dialog.ShowDialog();

            // 根据对话框结果进行处理
            if (result == true)
            {
                string name = myUserControl.UserName;
                int? age = myUserControl.UserAge;
                MessageBox.Show($"Name: {name}, Age: {age}");
            }
            else
            {
                MessageBox.Show("Dialog was cancelled.");
            }
        }
        public MessageResult Result
        {
            get { return GetProperty(() => Result); }
            set { SetProperty(() => Result, value); }
        }
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }

        [Command]
        public void ShowDialog()
        {
            Result = DialogService.ShowDialog(dialogButtons: MessageButton.YesNoCancel, title: "Simple Dialog", viewModel: new SimpleDialogViewModel());
        }
        public virtual string ButtonText
        {
            get { return GetProperty(() => ButtonText); }
            set { SetProperty(() => ButtonText, value); }
        }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(); } }
        [Command]
        public void SaveConfirmation()
        {
            MessageResult result;
            result = MessageBoxService.ShowMessage(
                messageBoxText: "Save Changes?",
                caption: "DXApplication",
                icon: MessageIcon.Question,
                button: MessageButton.YesNoCancel,
                defaultResult: MessageResult.Cancel
            );
            switch (result)
            {
                case MessageResult.Yes:
                    ButtonText = "Changes Saved";
                    break;
                case MessageResult.No:
                    ButtonText = "Changes Discarded";
                    break;
                case MessageResult.Cancel:
                    ButtonText = "Continue Editing";
                    break;
            }
        }
        public bool CanSave()
        {
            return !string.IsNullOrEmpty(FirstName);
        }
        //
        [Command]
        public void ShowRegistrationForm()
        {
            UICommand registerAsyncCommand = new UICommand(
               id: null,
               caption: "Register Async",
               command: new AsyncCommand<CancelEventArgs>(
                   async cancelArgs => {
                       try
                       {
                           await MyAsyncExecuteMethod();
                       }
                       catch (Exception e)
                       {
                           AsyncMessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
                           cancelArgs.Cancel = true;
                       }
                   },
                   cancelArgs => !string.IsNullOrEmpty(registrationViewModel.UserName)
               ),
               asyncDisplayMode: AsyncDisplayMode.Wait,
               isDefault: false,
               isCancel: false
           );

            UICommand registerCommand = new UICommand(
                id: null,
                caption: "Register",
                command: new DelegateCommand<CancelEventArgs>(
                    cancelArgs => {
                        try
                        {
                            MyExecuteMethod();
                        }
                        catch (Exception e)
                        {
                            AsyncMessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
                            cancelArgs.Cancel = true;
                        }
                    },
                    cancelArgs => !string.IsNullOrEmpty(registrationViewModel.UserName) && !((IAsyncCommand)registerAsyncCommand.Command).IsExecuting
                ),
                isDefault: true,
                isCancel: false
            );

            UICommand cancelCommand = new UICommand(
                id: MessageBoxResult.Cancel,
                caption: "Cancel",
                command: null,
                isDefault: false,
                isCancel: true
            );

            UICommand result = AsyncDialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { registerAsyncCommand, registerCommand, cancelCommand },
                title: "Registration Dialog",
                viewModel: registrationViewModel
            );

            if (result == registerCommand)
            {
                // ...
            }
        }

        void MyExecuteMethod()
        {
            // ...
        }

        async Task MyAsyncExecuteMethod()
        {
            await Task.Delay(2000);
            // ...
        }

    }
}
