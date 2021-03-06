﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserDetailsClient.Core.Features.Home;
using UserDetailsClient.Core.Framework;
using Xamarin.Forms;

namespace UserDetailsClient.Core.Features.Shell
{
    public class AppShellViewModel : BaseViewModel
    {
        public const string AuthenticationRequestedMessage = nameof(AuthenticationRequestedMessage);

        public string AuthenticationActionText
        {
            get
            {
                return AuthenticationService.IsAnyOneLoggedOn ? "Log Off" : "Log On";
            }
        }

        public ICommand LoadCommand => new AsyncCommand(_ => LoadDataAsync());
        public ICommand AuthenticationActionCommand => new AsyncCommand(_ => OnAuthenticationAction());

        public AppShellViewModel()
        {
            MessagingCenter.Subscribe<HomeViewModel>(this, HomeViewModel.AuthenticationActionCompletedMessage, _ => LoadCommand.Execute(null));
        }

        private async Task LoadDataAsync()
        {
            // force the UI to update the data bindings
            this.SetAndRaisePropertyChanged("AuthenticationActionText");
        }

        private async Task OnAuthenticationAction()
        {
            // Home View Model does all the actual sign-in / sign-out and broadcasts the "AuthenticationCompleted" message
            MessagingCenter.Send(this, AuthenticationRequestedMessage);
        }
    }
}