using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ValidateForms
{
   public class MainPageViewModel: INotifyPropertyChanged
   {
      private string name;
      public string Name
      {
         get => name;
         set => SetProperty(ref name, value);
      }

      private string nameErrorMessage;
      public string NameErrorMessage
      {
         get => nameErrorMessage;
         set => SetProperty(ref nameErrorMessage, value);
      }      

      private bool showNameErrorMessage;
      public bool ShowNameErrorMessage
      {
         get => showNameErrorMessage;
         set => SetProperty(ref showNameErrorMessage, value);
      }

      private string email;
      public string Email
      {
         get => email;
         set => SetProperty(ref email, value);
      }

      private string emailErrorMessage;
      public string EmailErrorMessage
      {
         get => emailErrorMessage;
         set => SetProperty(ref emailErrorMessage, value);
      }      

      private bool showEmailErrorMessage;
      public bool ShowEmailErrorMessage
      {
         get => showEmailErrorMessage;
         set => SetProperty(ref showEmailErrorMessage, value);
      }

      private string password;
      public string Password
      {
         get => password;
         set => SetProperty(ref password, value);
      }

      private string passwordErrorMessage;
      public string PasswordErrorMessage
      {
         get => passwordErrorMessage;
         set => SetProperty(ref passwordErrorMessage, value);
      }      

      private bool showPasswordErrorMessage;
      public bool ShowPasswordErrorMessage
      {
         get => showPasswordErrorMessage;
         set => SetProperty(ref showPasswordErrorMessage, value);
      }

      private bool isValidName;
      public bool IsValidName
      {
         get => isValidName;
         set
         {
            if (SetProperty(ref isValidName, value))
            {
               SignUpCommand.RaiseCanExecuteChanged();
            }
         }
      }

      private bool isValidPassword;
      public bool IsValidPassword
      {
         get => isValidPassword;
         set
         {
            if (SetProperty(ref isValidPassword, value))
            {
               SignUpCommand.RaiseCanExecuteChanged();
            }
         }
      }

      private bool isValidEmail;
      public bool IsValidEmail
      {
         get => isValidEmail;
         set
         {
            if (SetProperty(ref isValidEmail, value))
            {
               SignUpCommand.RaiseCanExecuteChanged();
            }
         }
      }

      string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

      public AsyncCommand ValidateNameCommand { get; }
      public AsyncCommand ValidateEmailCommand { get; }
      public AsyncCommand ValidatePasswordCommand { get; }
      public AsyncCommand SignUpCommand { get; }

      bool EnableButton => IsValidName && IsValidEmail && IsValidPassword;

      public MainPageViewModel()
      {
         ValidateNameCommand = new AsyncCommand(HandleNameValidation);
         ValidateEmailCommand = new AsyncCommand(HandleEmailValidation);
         ValidatePasswordCommand = new AsyncCommand(HandlePasswordValidation);
         SignUpCommand = new AsyncCommand(HandleSignUp, () => EnableButton);
      }

      Task HandleNameValidation()
      {
         if (!string.IsNullOrEmpty(Name))
         {
            IsValidName = true;
            ShowNameErrorMessage = false;
         }
         else
         {
            NameErrorMessage = "*Please enter a name";
            IsValidName = false;
            ShowNameErrorMessage = true;
         }
         return Task.CompletedTask;
      }

      Task HandleEmailValidation()
      {
         if (!string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, emailPattern))
         {
            IsValidEmail = true;
            ShowEmailErrorMessage = false;
         }
         else
         {
            EmailErrorMessage = "*Invalid email";
            IsValidEmail = false;
            ShowEmailErrorMessage = true;
         }
         return Task.CompletedTask;
      }

      Task HandlePasswordValidation()
      {
         if (!string.IsNullOrEmpty(Password) && Password.Length >= 8)
         {
            IsValidPassword = true;
            ShowPasswordErrorMessage = false;
         }
         else
         {
            PasswordErrorMessage = "*Invalid password. Must be at least 8 characters";
            IsValidPassword = false;
            ShowPasswordErrorMessage = true;
         }
         return Task.CompletedTask;
      }

      async Task HandleSignUp()
      {
         await Application.Current.MainPage.DisplayAlert("Sign up completed", "Your user has been created successfully", "Ok");        
      }


      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
      {
         if (EqualityComparer<T>.Default.Equals(storage, value))
         {
            return false;
         }
         storage = value;
         OnPropertyChanged(propertyName);

         return true;
      }
   }
}
